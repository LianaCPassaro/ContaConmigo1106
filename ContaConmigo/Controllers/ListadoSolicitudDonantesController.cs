using ContaConmigo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
//using ContaConmigo.DataAccess.Managers;
//using ContaConmigo.DataAccess.Managers;

namespace ContaConmigo.Controllers
{
    public class ListadoSolicitudDonantesController : System.Web.Mvc.Controller
    {
        ContaConmigoEntities db = new ContaConmigoEntities();

        // GET: SolicitudDonante
        public ActionResult ListadoSolicitudDonante()
        { 
            List<RequestDonor> requestDonors = db.RequestDonors.OrderByDescending(x=>x.Last_Name_Request_Don).ToList();
            return View(requestDonors);
        }
        

        [HttpGet]
        public ActionResult AgregarSolicitud()
        {
            List<Province> ProvinceList = db.Provinces.ToList();
            ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");

            RequestDonor model = new RequestDonor();
            List<GroupFactorBlood> allGroupFactorItems = db.GroupFactorBloods.ToList();
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var factor in allGroupFactorItems)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = factor.GroupFactorBloodId,
                    Display = factor.GroupFactorDescription,
                    IsChecked = false //On the add view, no genres are selected by default
                });
            }
            model.RequestDonorBloods = checkBoxListItems;

            return View(model);
        }
        public JsonResult GetCityList(int ProvinceId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<City> CityList = db.Cities.Where(x => x.ProvinceId == ProvinceId).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInstitutionList(int CityId)
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<Institution> InstitutionList = db.Institutions.Where(x => x.CityId == CityId).ToList();
            return Json(InstitutionList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarSolicitud(RequestDonor a)
        {
            //Error al agregar una solicitud The changes to the database were committed successfully, but an error occurred while updating the object context. The ObjectContext might be in an inconsistent state. Inner exception message: The navigation property 'RequestDonorBloods' on entity of type 'ContaConmigo.Model.RequestDonor' must implement ICollection<T> in order for Entity Framework to be able to track changes in collections.
            if (ModelState.IsValid)
            {
                try
                {
                    string result = "Error! Order Is Not Complete!";
                    using (var db = new ContaConmigoEntities())
                    {
                        a.Completed = "false";
                        a.UserId = 1;
                        var selectedGroupFactor = a.RequestDonorBloods.Where(x => x.IsChecked).Select(x => x.ID).ToList();
                        RequestDonor requestDonor = new RequestDonor();
                        requestDonor.Name_Request_Don = a.Name_Request_Don;
                        requestDonor.Last_Name_Request_Don = a.Last_Name_Request_Don;
                        requestDonor.CityId = a.CityId;
                        requestDonor.Last_Date_Replacement = a.Last_Date_Replacement;
                        requestDonor.AmountDonor = a.AmountDonor;
                        requestDonor.InstitutionId = a.InstitutionId;
                        requestDonor.Comment = a.Comment;
                        requestDonor.Phone_Number = a.Phone_Number;
                        requestDonor.Birthday = a.Birthday;
                        requestDonor.Completed = "false";
                        requestDonor.UserId = 1;
                        db.RequestDonors.Add(requestDonor);

                        foreach (var groupfactorID in selectedGroupFactor)
                        {
                            RequestDonorBlood requestDonorBlood = new RequestDonorBlood();
                            requestDonorBlood.RequestDonorId = requestDonor.RequestDonorId;
                            var factor = db.GroupFactorBloods.Find(groupfactorID);
                            requestDonorBlood.GroupFactorBlood = factor;
                            db.RequestDonorBloods.Add(requestDonorBlood);
                        }
                        db.SaveChanges();
                        result = "Success! Order Is Complete!";
                    }
                    return RedirectToAction("ListadoSolicitudDonante");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al agregar una solicitud " + ex.Message);
                    return View();
                }
            }
            else
            { return View(); }
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

        public ActionResult SubirArchivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Temp/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpGet]
        public ActionResult EditarSolicitud(int id)
        {
            try
            {
                using (ContaConmigoEntities db = new ContaConmigoEntities())
                {
                    RequestDonor soldon = db.RequestDonors.Where(a => a.RequestDonorId == id).FirstOrDefault();

                    List<Province> ProvinceList = db.Provinces.ToList();
                    ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");

                    List<GroupFactorBlood> BloodGroupFactorList = db.GroupFactorBloods.ToList();
                    ViewBag.BloodFactorList = new SelectList(BloodGroupFactorList, "GroupFactorBloodId", "GroupFactorDescription");

                    List<Institution> InstitutionList = db.Institutions.ToList();
                    ViewBag.InstitutionList = new SelectList(InstitutionList, "InstitutionId", "InstitutionDescription");

                    return View(soldon);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar la información de la solicitud " + ex.Message);
                return View();
            }


        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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
                        //reqdon.ProvinceId = rd.ProvinceId;
                        reqdon.CityId = rd.CityId;
                        reqdon.Last_Date_Replacement = rd.Last_Date_Replacement;
                        reqdon.AmountDonor = rd.AmountDonor;
                        reqdon.InstitutionId = rd.InstitutionId;
                        //reqdon.BloodGroupId = rd.BloodGroupId;
                        //reqdon.BloodFactorId = rd.BloodFactorId;
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