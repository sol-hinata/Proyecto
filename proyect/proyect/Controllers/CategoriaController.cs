using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
namespace proyect.Controllers
{
    public class CategoriaController : Controller
    {
        //
        // GET: /Categoria/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getCategoria()
        {
            int p=3;
            DataClasses1DataContext db = new DataClasses1DataContext();
            Articulo info = db.articulos.Select(a => new Articulo() { categorialista = db.categorias.Select(b => new categoriapublicacion() {id=b.idCategoria,nombre=b.nombre}).ToList() }).ToArray()[0];

            return View(info);
        }
    }
}
