using ContaConmigo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaConmigo.Controllers
{
    public class DonantesController : Controller
    {
    ContaConmigoEntities db = new ContaConmigoEntities();
        // GET: Donantes
        public ActionResult ListadoDonantes()
        {
            List <Donor> donors = db.Donors.OrderByDescending(x => x.Last_Name_Don).ToList();
            int cityid = donors.Select(x=> x.CityId).First();
            List<City> cityList = db.Cities.Where(x => x.Id == cityid).ToList();
            var ProvinceId = cityList.Select(x => x.ProvinceId).First();
            List<Province> provinceList = db.Provinces.Where(x => x.ProvinceId == ProvinceId).ToList();
            ViewBag.ProvinceDescription = provinceList.Select(x => x.ProvinceDescription).First();


            return View(donors);
        }
    }
}