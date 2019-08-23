using ContaConmigo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;


namespace ContaConmigo.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {

        public ActionResult Index()
        {

            return View();

        }
        public ActionResult ListadoSolicitudDonantes()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Logueo de Usuario.";
            ContaConmigoEntities1 db = new ContaConmigoEntities1();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserContaConmigo a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new ContaConmigoEntities1())
                {

                    db.UserContaConmigoes.Add(a);
                    db.SaveChanges();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar el usuario " + ex.Message);
                return View();
            }
        }
    }
}
