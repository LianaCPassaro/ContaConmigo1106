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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Data;
using ContaConmigo.ViewModel;

namespace ContaConmigo.Controllers
{
    public class DonanteController : Controller
    {
        ContaConmigoEntities db = new ContaConmigoEntities();
        // GET: Donantes
        public ActionResult ListadoDonantes()
        {
            using (ContaConmigoEntities db = new ContaConmigoEntities())
            {
                List<Donor> donors = db.Donors.ToList();
                List<City> cities = db.Cities.ToList();
                List<Province> provinces = db.Provinces.ToList();
                List<GroupFactorBlood> groupFactors = db.GroupFactorBloods.ToList();

                var donorcitype = from d in donors
                                  join c in cities on d.CityId equals c.Id into table1
                                  from c in table1.ToList()
                                  join p in provinces on c.ProvinceId equals p.ProvinceId into table2
                                  from p in table2.ToList()
                                  join b in groupFactors on d.BloodGroupFactorId equals b.GroupFactorBloodId into table3
                                  from b in table3.ToList()
                                  select new CityViewModel
                                  {
                                      DonorsVM = d,
                                      CitiesVM = c,
                                      ProvincesVM = p,
                                      GroupFactorBloodVM =b
                                  };
                return View(donorcitype);
            }
        }

        [HttpGet]
        public ActionResult AgregarDonante()
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

        private void PopulateCityDropDownList(int pcia, object selectedCity = null)
        {
            var cityQuery = from d in db.Cities
                            orderby d.CityName
                            where d.ProvinceId == pcia
                            select d;
            ViewBag.CityIdSelected = new SelectList(cityQuery, "Id", "CityName", selectedCity);
        }

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
                        don.Last_Date_Blood_Extract = donor.Last_Date_Blood_Extract;
                        don.CityId = donor.CityId;
                        don.BloodGroupFactorId = donor.BloodGroupFactorId;
                        don.CreatedDate = DateTime.Now;
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
        public ActionResult EditarDonante(int id)
        {
            var donor = db.Donors.Where(x => x.DonorId == id).FirstOrDefault();

            List<Province> ProvinceList = db.Provinces.ToList();
            ViewBag.ProvinceList = new SelectList(ProvinceList, "ProvinceId", "ProvinceDescription");
            PopulateGroupFactorDropDownList(donor.BloodGroupFactorId);
            //busco la provincia relacionada a la ciudad y la agrego al modelo
            int pcia = db.Cities.Find(donor.CityId).ProvinceId;
            Province province = db.Provinces.Find(pcia);
            donor.ProvinceId = province.ProvinceId;
            ViewBag.CityDetail = db.Cities.Find(donor.CityId);
            //PopulateCityDropDownList(pcia, donor.CityId);


            return View(donor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDonante([Bind(Include = "DonorID,Name_Don,Last_Name_Don,CityId,Last_Date_Blood_Extract,BloodGroupFactorId,UserId=1")]Donor donor)
        {
            //var groupFactorsQuery = from d in db.GroupFactorBloods
            //                        //orderby d.GroupFactorDescription
            //                        select d;
            //ViewBag.GroupFactorBloodId = new SelectList(groupFactorsQuery, "GroupFactorBloodId", "GroupFactorDescription", "BloodGroupFactorId");
            if (ModelState.IsValid)
            {
                db.Entry(donor).State = EntityState.Modified;
                donor.UpdatedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("ListadoDonantes");
            }
            return View(donor);
        }

        public ActionResult Eliminar(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id)
        {
            try
            {
                Donor donor = db.Donors.Find(id);
                db.Donors.Remove(donor);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("ListadoDonantes");
        }
        public ActionResult Detail(int id)
        {
            //Donor donor = db.Donors.Find(id);
            //int pcia = db.Cities.Find(donor.CityId).ProvinceId;
            //donor.ProvinceDescription = db.Provinces.Find(pcia).ProvinceDescription;
            //donor.CityName = db.Cities.Find(donor.CityId).CityName;
            //return View(donor);
            using (ContaConmigoEntities db = new ContaConmigoEntities())
            {
                List<Donor> donors = db.Donors.ToList();
                List<City> cities = db.Cities.ToList();
                List<Province> provinces = db.Provinces.ToList();
                List<GroupFactorBlood> groupFactors = db.GroupFactorBloods.ToList();

                var donorcitype = from d in donors 
                                  join c in cities on d.CityId equals c.Id into table1
                                  where d.DonorId == id
                                  from c in table1.ToList()
                                  join p in provinces on c.ProvinceId equals p.ProvinceId into table2
                                  from p in table2.ToList()
                                  join b in groupFactors on d.BloodGroupFactorId equals b.GroupFactorBloodId into table3
                                  from b in table3.ToList()
                                  
                                  select new CityViewModel
                                  {
                                        
                                      DonorsVM = d,
                                      CitiesVM = c,
                                      ProvincesVM = p,
                                      GroupFactorBloodVM = b
                                  };
                return View(donorcitype);
            }

        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
}