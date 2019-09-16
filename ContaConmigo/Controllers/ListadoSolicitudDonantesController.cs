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

namespace ContaConmigo.Controllers
{
    public class ListadoSolicitudDonantesController : System.Web.Mvc.Controller
    {

        // GET: SolicitudDonante
        public ActionResult ListadoSolicitudDonante()
        {
            ContaConmigoEntities1 db = new ContaConmigoEntities1();
            List<RequestDonor> requestDonors = db.RequestDonors.ToList();

            return View(requestDonors);
        }
        [HttpGet]
        public ActionResult AgregarSolicitud()
        {
            ContaConmigoEntities1 db = new ContaConmigoEntities1();
            List<Province> ProvinceList = db.Provinces.ToList();
            ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");

            List<BloodGroup> BloodGroupList = db.BloodGroups.ToList();
            ViewBag.BloodGroupList = new SelectList(BloodGroupList, "BloodGroupId", "Blood_Group");


            List<BloodFactor> BloodFactorList = db.BloodFactors.ToList();
            ViewBag.BloodFactorList = new SelectList(BloodFactorList, "BloodFactorId", "Blood_Factor");
            //otra forma de cargar el combo grupo
            //var bloodGrupo = db.BloodGroups.ToList();

            //List<SelectListItem> BloodGroupList = new List<SelectListItem>();
            //foreach (var m in bloodGrupo)
            //{
            //    BloodGroupList.Add(new SelectListItem { Text = m.Blood_Group, Value = m.BloodGroupId.ToString() });
            //    ViewBag.bloodGrupo = BloodGroupList;
            //}

            //otra forma de cargar el combo factor
            //var bloodFact = db.BloodFactors.ToList();

            //List<SelectListItem> BloodFactorList = new List<SelectListItem>();
            //foreach (var m in bloodFact)
            //{
            //    BloodFactorList.Add(new SelectListItem { Text = m.Blood_Factor, Value = m.BloodFactorId.ToString() });
            //    ViewBag.bloodFact = BloodFactorList;
            //}



            //List<BloodGroup> BloodGroupList = db.BloodGroups.ToList();
            //ViewBag.BloodGroupList = new SelectList(BloodGroupList, "BloodGroupId", "Blood_Group");

            //List<Institution> InstitutionList = db.Institutions.ToList();
            //ViewBag.InstitutionList = new SelectList(InstitutionList, "InstitutionId", "InstitutionDescription");

            return View();
        }
        public JsonResult GetCityList(int ProvinceId)
        {
            ContaConmigoEntities1 db = new ContaConmigoEntities1();
            db.Configuration.ProxyCreationEnabled = false;
            List<City> CityList = db.Cities.Where(x => x.ProvinceId == ProvinceId).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInstitutionList(int CityId)
        {
            ContaConmigoEntities1 db = new ContaConmigoEntities1();
            db.Configuration.ProxyCreationEnabled = false;
            List<Institution> InstitutionList = db.Institutions.Where(x => x.CityId == CityId).ToList();
            return Json(InstitutionList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarSolicitud(RequestDonor a)
        {
                    if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new ContaConmigoEntities1())
                    {
                        
                        a.Completed = "false";
                        a.UserId = 1;
                        db.RequestDonors.Add(a);
                        db.SaveChanges();
                        return RedirectToAction("ListadoSolicitudDonante");
                    }
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


        //public JsonResult GetCityList(int pcia)
        //{
        //    ContaConmigoEntities1 db = new ContaConmigoEntities1();
        //    //db.Configuration.ProxyCreationEnabled = false;
        //    var city = db.Cities.Where(x => x.ProvinceId == pcia).ToList();

        //    List<SelectListItem> licity = new List<SelectListItem>();
        //    licity.Add(new SelectListItem { Text = "--Ciudad--", Value = "0" });
        //    if (city != null)
        //    {
        //        foreach (var x in city)
        //        {
        //            licity.Add(new SelectListItem { Text = x.CityName, Value = x.Id.ToString() });
        //        }
        //    }
        //    return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));
        //}
        //public JsonResult GetInstitutionList(int id)
        //{
        //    ContaConmigoEntities1 db = new ContaConmigoEntities1();
        //    //db.Configuration.ProxyCreationEnabled = false;
        //    var inst = db.Institutions.Where(x => x.CityId == id).ToList();
        //    List<SelectListItem> liInst = new List<SelectListItem>();

        //    liInst.Add(new SelectListItem { Text = "--Institución--", Value = "0" });
        //    if (inst != null)
        //    {
        //        foreach (var l in inst)
        //        {
        //            liInst.Add(new SelectListItem { Text = l.InstitutionDescription, Value = l.InstitutionId.ToString() });
        //        }
        //    }
        //    return Json(new SelectList(liInst, "Value", "Text", JsonRequestBehavior.AllowGet));
        //}



        public ActionResult Detalle(int id)
        {
            try
            {
                using (var db = new ContaConmigoEntities1())
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
                using (ContaConmigoEntities1 db = new ContaConmigoEntities1())
                {
                    RequestDonor soldon = db.RequestDonors.Where(a => a.RequestDonorId == id).FirstOrDefault();

                    List<Province> ProvinceList = db.Provinces.ToList();
                    ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");


                    //List<City> CityList = db.Cities.ToList();
                    //ViewBag.CityList = new SelectList(CityList, "CityId", "CityName");
                    //ViewBag.CityList= soldon.CityId;
                    //var cityid = soldon.CityId;
                    //City provinceidObject = db.Cities.Where(a => a.CityId == cityid).FirstOrDefault();
                    //ViewBag.ProvinceIdSelected = provinceidObject.ProvinceId;
                    List<BloodFactor> BloodFactorList = db.BloodFactors.ToList();
                    ViewBag.BloodFactorList = new SelectList(BloodFactorList, "BloodFactorId", "Blood_Factor");

                    List<BloodGroup> BloodGroupList = db.BloodGroups.ToList();
                    ViewBag.BloodGroupList = new SelectList(BloodGroupList, "BloodGroupId", "Blood_Group");

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
                using (var db = new ContaConmigoEntities1())
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
                        reqdon.BloodGroupId = rd.BloodGroupId;
                        reqdon.BloodFactorId = rd.BloodFactorId;
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
            using (var db = new ContaConmigoEntities1())
            {
                RequestDonor reqdon = db.RequestDonors.Find(id);
                db.RequestDonors.Remove(reqdon);
                db.SaveChanges();
                return RedirectToAction("ListadoSolicitudDonante");
            }
        }
    }
}