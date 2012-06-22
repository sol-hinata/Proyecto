using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    public class ReedicionContenidoController : Controller
    {
        //
        // GET: /ReedicionContenido/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<Publicacion> publi = db.publicacions.Where( p=>p.UserId==(Guid)Session["ids"]&& p.estado == false).Select(pp => new Publicacion { idPublicacion = pp.idPublicacion, fecha = pp.fecha, observaciones = pp.observaciones }).ToList();
            ViewBag.lista = publi;
            return View();
        }
        public ActionResult Reeditar(int id) {
            int i = id;
            DataClasses1DataContext db = new DataClasses1DataContext();
            if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("ReeditarCurso","ReedicionContenido", new{id = id});    
            }
            if (db.articulos.Where(v => v.idPublicacion == i) != null)
            {   
                return RedirectToAction("ReeditarArticulo", "ReedicionContenido", new { id = id });
                //return View(info);
            }
            if (db.libros.Where(v => v.idPublicacion == i) != null) {
                return RedirectToAction("ReeditarLibro", "ReedicionContenido", new { id = id }); 
            }
            
            if (db.tutorials.Where(v => v.idPublicacion == i) != null)
            {
                return RedirectToAction("ReeditarTutorial", "ReedicionContenido", new { id = id });
                //return View(info);
            }
            //ViewBag.detalle = info.detalle;
            return View();
        }
        public ActionResult ReeditarCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //publicacion p =db.publicacions.Where(t=>t.UserId==(Guid)Session["ids"]).ToArray()[0];
            Curso data = db.cursos.Where(d => d.idPublicacion == id).Select(d => new Curso() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            //List<Publicacion> p = db.publicacions.Where(d => d.UserId == (Guid)Session["ids"] && d.idPublicacion == id).Select(g=>db.articulo).ToList();
            Curso info = data;
            return View(info);
        }
        [HttpPost]
        public ActionResult ReeditarCurso(int id,Curso model)
        {
            return View();
        }
        public ActionResult ReeditarArticulo(int id) {
            DataClasses1DataContext db= new DataClasses1DataContext();
            Articulo data = db.articulos.Where(p => p.idPublicacion == id).Select(d => new Articulo() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToArray()[0];
             Articulo info = data;
            return View(info);
        }
        [HttpPost]
        public ActionResult ReeditarArticulo(int id,Articulo model)
        {
            return View();
        }
        public ActionResult ReeditarTutorial(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Tutorial data = db.tutorials.Where(p => p.idPublicacion == id).Select(d => new Tutorial() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToArray()[0];
            Tutorial info = data;
            return View(info);
        }
        [HttpPost]
        public ActionResult ReeditarTutorial(int id, Tutorial model) {
            return View();
        }
        public ActionResult ReeditarLibro(int id) {
            
            DataClasses1DataContext db = new DataClasses1DataContext();
            Libro data = db.libros.Where(p => p.idPublicacion == id).Select(d => new Libro() { titulo = d.titulo, Autor = d.Autor, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            Libro info = data;
            ViewBag.im = data;
            return View(info);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReeditarLibro(int id, Libro model, HttpPostedFileBase uploadFile, HttpPostedFileBase libroFile)
        {
            if (uploadFile.ContentLength > 0 && libroFile.ContentLength > 0)
            {  //Pendiente
                DataClasses1DataContext db = new DataClasses1DataContext();
                //inserta la imagen en archivo
                string filePath = Path.Combine(HttpContext.Server.MapPath("../ImagenesLib"), Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
                publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                archivo img = new archivo() { rutafisica = filePath, rutavirtual = "/ImagenesLib/" + uploadFile.FileName, idPublicacion = ip.idPublicacion, fecha = DateTime.Now };
                db.archivos.InsertOnSubmit(img);
                db.SubmitChanges();
                //inserta el libro
                string librofilePath = Path.Combine(HttpContext.Server.MapPath("../Libro"), Path.GetFileName(libroFile.FileName));
                uploadFile.SaveAs(librofilePath);
                //llena en libro
                libro lib = new libro() { idPublicacion = ip.idPublicacion, titulo = model.titulo, fecha = DateTime.Now, puntuacion = 100, Autor = model.Autor, Annio_pub = model.Annio_pub, rutafisica = librofilePath, rutavirtual = "/Libro/" + libroFile.FileName, detalle = model.detalle };
                db.libros.InsertOnSubmit(lib);
                db.SubmitChanges();
                //
                var l = db.libros.Where(li => li.idPublicacion == id);
                libro libr = new libro() { titulo = model.titulo, fecha = DateTime.Now, Autor = model.Autor, Annio_pub = model.Annio_pub, detalle = model.detalle};
                l.ToArray()[0].titulo = model.titulo;
                l.ToArray()[0].fecha = DateTime.Now;
                l.ToArray()[0].Autor = model.Autor;
                l.ToArray()[0].Annio_pub = model.Annio_pub;
                l.ToArray()[0].titulo = model.detalle;
                db.SubmitChanges();
            }
            return View();
        }
        public ActionResult Borrar(int id) {

            return View();
        }
        
    }
}
