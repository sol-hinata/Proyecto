using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    //[Authorize(Roles = "administrador")]
    public class ModeracionContenidoController : Controller
    {
        // GET: /ModeracionContenido/

        public ActionResult Index()
        {
            return View();
        }
        //ARTICULO
        public ActionResult VerArticulos() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listpendiente> pendientes = db.publicacions.Where(es => es.estado == false).Select(ess => new listpendiente() { idPublicacion = ess.idPublicacion, titulo = db.articulos.Where(f => f.idPublicacion == ess.idPublicacion).Select(g => new Articulo { titulo = g.titulo }).ToList(), nombre = db.publicacions.Where(pn => pn.idPublicacion == ess.idPublicacion).Select(pnn => pnn.aspnet_User).ToList() }).ToList();
            ViewBag.lista = pendientes;
            return View();
        }
        public ActionResult VerArticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<verarticulo> data = db.articulos.Where(d => d.idPublicacion == id).Select(d => new verarticulo() { idPublicacion = id,titulo=d.titulo,detalle=d.detalle,fecha=d.fecha}).ToList();
              ViewBag.articulo = data;
            return View();
        }
        public ActionResult PublicarArticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1


            System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
            if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
            {
                karma k = new karma() { UserId = idusu ,detalle = "puntaje  de karma", total = 10, fecha = DateTime.Now };
                db.karmas.InsertOnSubmit(k);
                db.SubmitChanges();
            }
            else {

                List<karma>   karma= db.karmas.Where(g => g.UserId == idusu).ToList();
                //kar.Sum = kar(double) + 10;
                double j = 0;
                foreach (var u in karma) {
                    double h = u.total ;
                     j = h + 10;
                }
                var l=db.karmas.Where(ll=>ll.UserId == idusu);
                karma ka = new karma() { UserId=idusu,detalle="puntaje",total = j, fecha=DateTime.Now };
                l.ToArray()[0].UserId = idusu;
                l.ToArray()[0].detalle ="puntaje";
                l.ToArray()[0].total = j;
                l.ToArray()[0].fecha = DateTime.Now;
                db.SubmitChanges();

            }
            
            //return View();
            return RedirectToAction("Index","ModeracionContenido");
        
        }
        //LIBRO
        public ActionResult VerLibros() {
            DataClasses1DataContext db = new DataClasses1DataContext();
           // List<listpendientelibros> librospendientes = db.publicacions.Where(l => l.estado == false).Select(ll => new listpendientelibros() { idPublicacion = ll.idPublicacion, list = db.publicacions.Where(li => li.idPublicacion == ll.idPublicacion).Select(puli => new SubirLibro { titulo = puli.titulo, rutafisica = puli.rutafisica, rutavirtual = puli.rutavirtual }).ToList(), nombre = db.publicacions.Where(nom => nom.idPublicacion == ll.idPublicacion).Select(no => no.aspnet_User).ToList(), listimli = db.archivos.Where(m => m.idPublicacion == ll.idPublicacion).Select(im => new archivo {rutafisica=im.rutafisica,rutavirtual=im.rutavirtual }).ToList() }).ToList();
            //List<listpendientelibros> librospendientes = db.publicacions.Where(p => p.estado == false).Select(t => new listpendientelibros() { idPublicacion = t.idPublicacion, nombre=db.publicacions.Where(u=>u.idPublicacion==t.idPublicacion).Select(h=>h.aspnet_User).ToList() }).ToList();
           
            // List<listalibro> librospendientes = db.publicacions.Where(a => a.estado == false).Select(b => new listalibro() { idPublicacion = b.idPublicacion, id = b.aspnet_User.UserId, nombre = b.aspnet_User.UserName, libro = b.libro.ToList(), archivo = b.archivos.ToList() }).ToList();
           // ViewBag.lista = librospendientes;
            archivo ar = db.archivos.ToArray()[0];
            List<listapendientelibro> librospendientes = db.publicacions.Where(es => es.estado == false).Select(ess => new listapendientelibro() { idPublicacion = ess.idPublicacion, libro = db.libros.ToList(), nombre = db.publicacions.Where(u => u.idPublicacion == ess.idPublicacion).Select(y => y.aspnet_User.UserName).ToArray()[0] , rutavirtual=ar.rutavirtual}).ToList();
            ViewBag.lista = librospendientes;
            return View();
        }
        public ActionResult VerLibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //List<VerLibro> verlibro = db.publicacions.Where(b => b.idPublicacion == id).Select(h => new VerLibro() { idPublicacion = id, nombre = h.aspnet_User.UserName, libro = h.libros.ToList(), archivo = h.archivos.ToList() }).ToList();
            //ViewBag.verlibro = verlibro;
            return View();
        }
        public ActionResult PublicarLibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();

             System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
             if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
             {
                 karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 100, fecha = DateTime.Now };
                 db.karmas.InsertOnSubmit(k);
                 db.SubmitChanges();
             }
             else
             {

                 List<karma> karma = db.karmas.Where(g => g.UserId == idusu).ToList();
                 //kar.Sum = kar(double) + 10;
                 double j = 0;
                 foreach (var u in karma)
                 {
                     double h = u.total;
                     j = h + 100;
                 }
                 var l = db.karmas.Where(ll => ll.UserId == idusu);
                 karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                 l.ToArray()[0].UserId = idusu;
                 l.ToArray()[0].detalle = "puntaje";
                 l.ToArray()[0].total = j;
                 l.ToArray()[0].fecha = DateTime.Now;
                 db.SubmitChanges();
             }
            return RedirectToAction("Index", "ModeracionContenido");
        }
        //CURSO
        public ActionResult VerCursos() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listpendientecurso> pendientes = db.publicacions.Where(es => es.estado == false).Select(ess => new listpendientecurso() { idPublicacion = ess.idPublicacion, titulo = db.cursos.Where(f => f.idPublicacion == ess.idPublicacion).Select(g => new Curso { titulo = g.titulo }).ToList(), nombre = db.publicacions.Where(pn => pn.idPublicacion == ess.idPublicacion).Select(pnn => pnn.aspnet_User).ToList() }).ToList();
            ViewBag.lista = pendientes;
            return View();
        }

        public ActionResult VerCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vercurso> data = db.cursos.Where(d => d.idPublicacion == id).Select(d => new vercurso() {idPublicacion=id, titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToList();
            ViewBag.curso = data;
            return View();
        }
        public ActionResult PublicarCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1
             System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
             if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
             {
                 karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 200, fecha = DateTime.Now };
                 db.karmas.InsertOnSubmit(k);
                 db.SubmitChanges();
             }
             else
             {

                 List<karma> karma = db.karmas.Where(g => g.UserId == idusu).ToList();
                 //kar.Sum = kar(double) + 10;
                 double j = 0;
                 foreach (var u in karma)
                 {
                     double h = u.total;
                     j = h + 200;
                 }
                 var l = db.karmas.Where(ll => ll.UserId == idusu);
                 karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                 l.ToArray()[0].UserId = idusu;
                 l.ToArray()[0].detalle = "puntaje";
                 l.ToArray()[0].total = j;
                 l.ToArray()[0].fecha = DateTime.Now;
                 db.SubmitChanges();
             }
            //return View();
            return RedirectToAction("Index", "ModeracionContenido");
        }
        public ActionResult Rechazar(int id)
        {
            ViewBag.ve = id;
            return View();
        }
        [HttpPost]
        public ActionResult Rechazar(observarcion model,int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { observaciones = model.observaciones };
            pub.ToArray()[0].observaciones = model.observaciones;
            db.SubmitChanges();
            return RedirectToAction("Index", "ModeracionContenido");
        }
        //TUTORIAL
        public ActionResult VerTutoriales() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listpendientetutorial> pendientes = db.publicacions.Where(es => es.estado == false).Select(ess => new listpendientetutorial() { idPublicacion = ess.idPublicacion, titulo = db.cursos.Where(f => f.idPublicacion == ess.idPublicacion).Select(g => new Tutorial { titulo = g.titulo }).ToList(), nombre = db.publicacions.Where(pn => pn.idPublicacion == ess.idPublicacion).Select(pnn => pnn.aspnet_User).ToList() }).ToList();
            ViewBag.lista = pendientes;
            return View();
        
        }
        public ActionResult Vertutorial(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vertutorial> data = db.tutorials.Where(d => d.idPublicacion == id).Select(d => new vertutorial() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha, puntuacion = d.puntuacion }).ToList();
            //Tutorial info = data.ToArray()[0];
            ViewBag.tutorial = data;
            return View();
        }
        public ActionResult PublicarTutorial(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1
            System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
            if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
            {
                karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 50, fecha = DateTime.Now };
                db.karmas.InsertOnSubmit(k);
                db.SubmitChanges();
            }
            else
            {

                List<karma> karma = db.karmas.Where(g => g.UserId == idusu).ToList();
                //kar.Sum = kar(double) + 10;
                double j = 0;
                foreach (var u in karma)
                {
                    double h = u.total;
                    j = h + 50;
                }
                var l = db.karmas.Where(ll => ll.UserId == idusu);
                karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                l.ToArray()[0].UserId = idusu;
                l.ToArray()[0].detalle = "puntaje";
                l.ToArray()[0].total = j;
                l.ToArray()[0].fecha = DateTime.Now;
                db.SubmitChanges();
            }
            return RedirectToAction("Index", "ModeracionContenido");
        
        }

        public ActionResult ReportesPublicados() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<Publicacion> p = db.publicacions.Where(s => s.estado == true).Select(f => new Publicacion(){ idPublicacion = f.idPublicacion,fecha=f.fecha}).ToList();
            ViewBag.listapublicados = p;
            return View();
        }
        
    }
}
