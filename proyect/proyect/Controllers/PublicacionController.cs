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
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<verarticulo> listarti = db.articulos.Select(a => new verarticulo() { idArticulo = a.idArticulo, titulo = a.titulo, detalle = a.detalle, fecha = a.fecha, idPublicacion = a.idPublicacion, nombre = db.publicacions.Where(p => p.idPublicacion == a.idPublicacion).Select(x => x.aspnet_User).ToList() }).ToList();

            ViewBag.lista = listarti;
            return View();
        }
        public ActionResult mostrar()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
           
                //publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).ToArray()[0];
               
            List<Articulo> data = db.articulos.Select(d => new Articulo() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToList();


                Articulo info = data.ToArray()[0];
                ViewBag.detalle = info.detalle;
                return View(info);
            
        }

        public ActionResult Publicacion()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            if (db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 1)
            {
                publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).ToArray()[0];
                List<Articulo> data = db.articulos.Where(a => a.idPublicacion == ip.idPublicacion).Select(d => new Articulo() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToList();

                
                Articulo info = data.ToArray()[0];
                ViewBag.detalle=info.detalle;
                return View(info);
            }
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
            articulo a = new articulo() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = model.puntuacion, detalle = model.detalle, idPublicacion = ip.idPublicacion };
            db.articulos.InsertOnSubmit(a);
            db.SubmitChanges();
            return RedirectToAction("Index", "Publicacion");

 
        }

    }
}
