using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /Publicacion/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Articulo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Articulo(Articulo model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = true, fecha = model.fecha };
            db.publicacions.InsertOnSubmit(p);
            db.SubmitChanges();
            publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).ToArray()[0];
            articulo a = new articulo() { titulo = model.titulo, fecha = model.fecha, puntuacion = model.puntuacion, detalle = model.detalle, idPublicacion = ip.idPublicacion };
            db.articulos.InsertOnSubmit(a);
            db.SubmitChanges();
            return RedirectToAction("Index", "Publicacion");

 
        }

    }
}
