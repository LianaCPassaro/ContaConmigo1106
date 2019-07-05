﻿using ContaConmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaConmigo.Controllers
{
    public class ListadoSolicitudDonantesController : System.Web.Mvc.Controller
    {
        // GET: SolicitudDonante
        public ActionResult ListadoSolicitudDonante()
        {
            ContaConmigoEntities db = new ContaConmigoEntities();
            //db.RequestDonors.ToList();
            return View(db.RequestDonors.ToList());
        }
        [HttpGet]
        public ActionResult AgregarSolicitud()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarSolicitud(RequestDonor a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (ContaConmigoEntities db = new ContaConmigoEntities())
                {
                    db.RequestDonors.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("ListadoSolicitudDonante");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Error al agregar una solicitud " + ex.Message);
                return View();
            }
        }
        public ActionResult Detalle(int id)
        {
            try
            {
                using (var db = new ContaConmigoEntities())
                {
                    RequestDonor soldon = db.RequestDonors.Find(id);
                    return View(soldon);

                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al ver el detalle de una solicitud " + ex.Message);
                return View();
            }
           
        }

        public ActionResult EditarSolicitud(int id)
        {

            try
            {
                using (var db = new ContaConmigoEntities())
                {
                    RequestDonor soldon = db.RequestDonors.Where(a=>a.RequestDonorId==id).FirstOrDefault();
                    //RequestDonor soldon1 = db.RequestDonors.Find(id);
                    return View(soldon);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar la información de la solicitud " + ex.Message);
                return View();
            }
                
                
        }
        public ActionResult ProvincesList()
        {
            using (var db = new ContaConmigoEntities())
            {
                return PartialView(db.Provinces.ToList());
            }
        }
        public ActionResult CitiesList()
        {
            using (var db = new ContaConmigoEntities())
            {
                return PartialView(db.Cities.ToList());
            }
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarSolicitud(RequestDonor rd)
        {
            try
            {
                using (var db = new ContaConmigoEntities())
                {
                    RequestDonor reqdon = db.RequestDonors.Find(rd.RequestDonorId);
                    if (reqdon != null)
                    {
                        reqdon.Name_Request_Don = rd.Name_Request_Don;
                        reqdon.Last_Name_Request_Don = rd.Last_Name_Request_Don;
                        reqdon.Phone_Number = rd.Phone_Number;
                        reqdon.ProvinceId = rd.ProvinceId;
                        reqdon.CityId = rd.CityId;
                        reqdon.Last_Date_Replacement = rd.Last_Date_Replacement;
                        reqdon.AmountDonor = rd.AmountDonor;
                        reqdon.InstitutionId = rd.InstitutionId;
                        reqdon.GroupId = rd.GroupId;
                        reqdon.FactorId = rd.FactorId;
                        reqdon.Comment = rd.Comment;
                        reqdon.Phone_Number = rd.Phone_Number;
                        reqdon.Birthday = rd.Birthday;
                        reqdon.Completed = rd.Completed;
                        reqdon.Photo = rd.Photo;
                        db.SaveChanges();
                        return RedirectToAction("ListadoSolicitudDonante");
                    }
                    else
                    {
                        return RedirectToAction("ListadoSolicitudDonante");
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar la edición de la información de la solicitud " + ex.Message);
                return RedirectToAction("ListadoSolicitudDonante");
            }
        }
        public ActionResult EliminarSolicitud(int id)
        {
            using (var db = new ContaConmigoEntities())
            {
                RequestDonor reqdon = db.RequestDonors.Find(id);
                db.RequestDonors.Remove(reqdon);
                db.SaveChanges();
                return RedirectToAction("ListadoSolicitudDonante");
            }
        }
    }
}