using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /Publicacion/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //List<verarticulo> listarti = db.articulos.Select(a => new verarticulo() { idArticulo = a.idArticulo, titulo = a.titulo, detalle = a.detalle, fecha = a.fecha, idPublicacion = a.idPublicacion, nombre = db.publicacions.Where(p => p.idPublicacion == a.idPublicacion).Select(x => x.aspnet_User).ToList() }).ToList();
           // List<verarticulo> listarti = db.articulos.Select(a => new verarticulo() { idArticulo = a.idArticulo, titulo = a.titulo, detalle = a.detalle, fecha = a.fecha }).ToList();
            //ViewBag.lista = listarti;

            List<mostrararticulos> listarti = db.publicacions.Where(a => a.estado == true ).Select(aa => new mostrararticulos() { idPublicacion = aa.idPublicacion, idus = aa.aspnet_User.UserId, nombre = aa.aspnet_User.UserName, av = db.avatars.ToList(),titulo=aa.articulo.titulo,detalle=aa.articulo.detalle }).ToList();
            ViewBag.lista = listarti;
            
            List<mostrarcursos> listcur = db.publicacions.Where(a => a.estado == true ).Select(aa => new mostrarcursos() { idPublicacion = aa.idPublicacion, idus = aa.aspnet_User.UserId, nombre = aa.aspnet_User.UserName, av = db.avatars.ToList(),curs=db.cursos.ToList() }).ToList();
            ViewBag.listacurso = listcur;
            List<mostrartutoriales> listuto = db.publicacions.Where(a => a.estado == true).Select(aa => new mostrartutoriales() { idPublicacion = aa.idPublicacion, idus = aa.aspnet_User.UserId, nombre = aa.aspnet_User.UserName, av = db.avatars.ToList() }).ToList();
            ViewBag.listatutorial = listuto;
            return View();
        }
        public ActionResult Enviado()
        {

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
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                db.publicacions.InsertOnSubmit(p);
                db.SubmitChanges();
                // publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).ToArray()[0];
                publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                articulo a = new articulo() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 10, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                db.articulos.InsertOnSubmit(a);
                db.SubmitChanges();


                string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                List<categoria> listacategoria = new List<categoria>();
                foreach (var items in arraycategorias)
                {
                    string item = items.Trim();
                    if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                    {
                        listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                    }

                }
                if (listacategoria.ToList().Count() > 0)
                {
                    db.categorias.InsertAllOnSubmit(listacategoria);
                    db.SubmitChanges();
                    @ViewBag.mensaje = "las categorias se crearon con exito";

                }

                categoria idcate = db.categorias.ToArray()[0];
                publicacion_categoria c = new publicacion_categoria() { idCategoria = idcate.idCategoria, idPublicacion = ip.idPublicacion };
                db.publicacion_categorias.InsertOnSubmit(c);
                db.SubmitChanges();



                return RedirectToAction("Enviado", "Publicacion"); 
            }
            return View(model);
            
        }
        //DESDE AQUI PARA EL LIBRO
        public ActionResult Libro()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Libro(HttpPostedFileBase uploadFile,HttpPostedFileBase libroFile, Libro model)
        {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                if (uploadFile.ContentLength > 0 && libroFile.ContentLength > 0)
                {   //llena en publicacion   
                    publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                    db.publicacions.InsertOnSubmit(p);
                    db.SubmitChanges();
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

                    string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                    List<categoria> listacategoria = new List<categoria>();
                    foreach (var items in arraycategorias)
                    {
                        string item = items.Trim();
                        if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                        {
                            listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                        }

                    }
                    if (listacategoria.ToList().Count() > 0)
                    {
                        db.categorias.InsertAllOnSubmit(listacategoria);
                        db.SubmitChanges();
                        @ViewBag.mensaje = "las categorias se crearon con exito";

                    }

                    categoria idcate = db.categorias.ToArray()[0];
                    publicacion_categoria c = new publicacion_categoria() { idCategoria = idcate.idCategoria, idPublicacion = ip.idPublicacion };
                    db.publicacion_categorias.InsertOnSubmit(c);
                    db.SubmitChanges();

                }
                return RedirectToAction("Enviado", "Publicacion");
            }
            return View(model);
            }
        
        
        //CURSOS
        public ActionResult Curso(){
        
        return View();
        }
        
        [HttpPost]
        public ActionResult Curso(Curso model){
            if (ModelState.IsValid)
            {

                DataClasses1DataContext db = new DataClasses1DataContext();
                publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                db.publicacions.InsertOnSubmit(p);
                db.SubmitChanges();
                publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                curso a = new curso() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 200, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                db.cursos.InsertOnSubmit(a);
                db.SubmitChanges();

                string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                List<categoria> listacategoria = new List<categoria>();
                foreach (var items in arraycategorias)
                {
                    string item = items.Trim();
                    if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                    {
                        listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                    }

                }
                if (listacategoria.ToList().Count() > 0)
                {
                    db.categorias.InsertAllOnSubmit(listacategoria);
                    db.SubmitChanges();
                    @ViewBag.mensaje = "las categorias se crearon con exito";

                }

                categoria idcate = db.categorias.ToArray()[0];
                publicacion_categoria c = new publicacion_categoria() { idCategoria = idcate.idCategoria, idPublicacion = ip.idPublicacion };
                db.publicacion_categorias.InsertOnSubmit(c);
                db.SubmitChanges();
                return RedirectToAction("Enviado", "Publicacion");
            }
            return View(model);
            }
            

        //TUTORIALES 
        public ActionResult Tutorial() {
            return View();
        }
        [HttpPost]
        public ActionResult Tutorial(Tutorial model) {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                db.publicacions.InsertOnSubmit(p);
                db.SubmitChanges();
                publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                tutorial a = new tutorial() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 50, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                db.tutorials.InsertOnSubmit(a);
                db.SubmitChanges();

                string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                List<categoria> listacategoria = new List<categoria>();
                foreach (var items in arraycategorias)
                {
                    string item = items.Trim();
                    if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                    {
                        listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                    }

                }
                if (listacategoria.ToList().Count() > 0)
                {
                    db.categorias.InsertAllOnSubmit(listacategoria);
                    db.SubmitChanges();
                    @ViewBag.mensaje = "las categorias se crearon con exito";

                }

                categoria idcate = db.categorias.ToArray()[0];
                publicacion_categoria c = new publicacion_categoria() { idCategoria = idcate.idCategoria, idPublicacion = ip.idPublicacion };
                db.publicacion_categorias.InsertOnSubmit(c);
                db.SubmitChanges();
                return RedirectToAction("Enviado", "Publicacion");
            }
            return View(model);
            }
    }
}
