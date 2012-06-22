using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Web.Security;

namespace proyect.Models
{
    public class Articulo
    {
        // public string nombre { get; set; }
        public int idArticulo { get; set; }
        public string titulo { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public System.DateTime fecha { get; set; }
        public int idPublicacion { get; set; }
    }
}