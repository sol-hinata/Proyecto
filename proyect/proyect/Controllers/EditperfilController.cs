﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class EditperfilController : Controller
    {
        //
        // GET: /Editperfil/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Perfil()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            System.Guid ddd = (Guid)Session["ids"];
            if (db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 1)
            {
                List<PerfilEdit> data = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).Select(d => new PerfilEdit() { nombre = d.nombre, apellido = d.apellido, fecha = d.fecha, apellidom = d.apellidom, interes = d.intereses, idPerfil = d.idPerfil, ubicacion = d.ubicacion }).ToList();

                PerfilEdit info = data.ToArray()[0];
                return View(info);
            }
            return View();
            
        }
        [HttpPost]
        public ActionResult Perfil(PerfilEdit model)
        {

            DataClasses1DataContext per = new DataClasses1DataContext();

            if (per.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 0)
            {

                string sqlTimeAsString = model.fecha.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                System.Guid IdUs = per.aspnet_Users.Where(a => a.UserName == model.nombre).Select(a => a.UserId).ToArray()[0];
                perfil p = new perfil() { nombre = model.nombre, apellido = model.apellido, apellidom = model.apellidom, ubicacion = model.ubicacion, intereses = model.interes, fecha = model.fecha, UserId = IdUs, };
                per.perfils.InsertOnSubmit(p);
                per.SubmitChanges();
                return RedirectToAction("Index", "EditPerfil");

            }
            else
            {
                var o = per.perfils.Where(a => a.UserId == (Guid)Session["ids"]);

                perfil p = new perfil() { nombre = model.nombre, apellido = model.apellido, apellidom = model.apellidom, ubicacion = model.ubicacion, intereses = model.interes, fecha = model.fecha, UserId = (Guid)Session["ids"] };
                o.ToArray()[0].nombre = model.nombre;
                o.ToArray()[0].apellido = model.apellido;
                o.ToArray()[0].apellidom = model.apellidom;
                o.ToArray()[0].intereses = model.interes;
                o.ToArray()[0].fecha = model.fecha;
                o.ToArray()[0].ubicacion = model.ubicacion;

                per.SubmitChanges();
                return RedirectToAction("Index", "EditPerfil");

            }

        }
        
    }
}