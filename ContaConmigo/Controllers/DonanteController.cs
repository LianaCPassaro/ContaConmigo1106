﻿using ContaConmigo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace ContaConmigo.Controllers
{
    public class DonanteController : Controller
    {
        ContaConmigoEntities db = new ContaConmigoEntities();
        // GET: Donantes
        public ActionResult ListadoDonantes()
        {            
            var donors = db.Donors.Include(x => x.City)
                .Include(x => x.GroupFactorBlood)
                .OrderBy(x => x.Last_Name_Don)
                .ToList();
            return View(donors);
        }
        [HttpGet]
        public ActionResult AgregarDonante ()
        {
            List<Province> ProvinceList = db.Provinces.ToList();
            ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");
            PopulateGroupFactorDropDownList();
            return View();
        }

        private void PopulateGroupFactorDropDownList(object selectedGroupFactor = null)
        {
            var groupFactorsQuery = from d in db.GroupFactorBloods
                                   orderby d.GroupFactorDescription
                                   select d;
            ViewBag.GroupFactorBloodId = new SelectList(groupFactorsQuery, "GroupFactorBloodId", "GroupFactorDescription", selectedGroupFactor);
        }

        //private void PopulateCityDropDownList(int pcia, object selectedCity = null)
        //{
        //    var cityQuery = from d in db.Cities
        //                            orderby d.CityName
        //                            where d.ProvinceId == pcia
        //                            select d;
        //    ViewBag.CityIdSelected = new SelectList(cityQuery, "Id", "CityName", selectedCity);
        //}

        public JsonResult GetCityList(int ProvinceId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<City> CityList = db.Cities.Where(x => x.ProvinceId == ProvinceId).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarDonante(Donor donor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new ContaConmigoEntities())
                    {
                        Donor don = new Donor();
                        don.UserId = 1;
                        don.Name_Don = donor.Name_Don;
                        don.Last_Name_Don = donor.Last_Name_Don;
                        //don.Last_Date_Blood_Extract = donor.Last_Date_Blood_Extract;
                        don.CityId = donor.CityId;
                        don.BloodGroupFactorId = donor.GroupFactorBloodId;
                        db.Donors.Add(don);
                        db.SaveChanges();
                    }
                    return RedirectToAction("ListadoDonantes");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "Imposible guardar los cambios. Intente nuevamente, y si el problema persiste, por favor comuníquese con el Administrador.");
                    return View();
                }
            }
            else
            { return View(); }
        }

        [HttpGet]
        public ActionResult EditarDonante(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            
            List<Province> ProvinceList = db.Provinces.ToList();
            ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");
            PopulateGroupFactorDropDownList(donor.BloodGroupFactorId);
            //busco la provincia relacionada a la ciudad y la agrego al modelo
            int pcia = db.Cities.Find(donor.CityId).ProvinceId;
            Province province = db.Provinces.Find(pcia);
            donor.ProvinceId = province.ProvinceId;
            
            //PopulateCityDropDownList(pcia, donor.CityId);


            return View(donor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDonante(int id, Donor donor)
        {
            if (ModelState.IsValid)
            {

                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListadoDonantes");
            }
            return View(donor);
        }
    }
}