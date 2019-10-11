using ContaConmigo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
                //.Include(x => x.Province)
                .ToList();


            return View(donors);
        }
    }
}