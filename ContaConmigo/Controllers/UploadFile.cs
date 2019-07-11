using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContaConmigo.Controllers
{
    public class UploadFile
    {
        public String Confirmacion { get; set; }
        public Exception error { get; set; }
        public void SubirArchivo (String ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.Confirmacion = "Fichero guardado";

            }
            catch (Exception ex)
            {
                this.error = ex;
                
            }
        }
    }
}