using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security;

namespace proyect.Models
{
    public class Publicacion
    {
        public int idPublicacion { get; set; }
        public System.Guid id { get; set; }
        public int estado { get; set; }
        public System.DateTime fecha { get; set; }
    }
    public class Articulo
    {
        public int idArticulo { get; set; }
        public string titulo { get; set; }
        [UIHint("tinymce_jquery_full"),AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public System.DateTime fecha { get; set; }
        public int idPublicacion { get; set; }

    }
    public class verarticulo {
        public int idArticulo { get; set; }
        public string titulo { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public System.DateTime fecha { get; set; }
        public int idPublicacion { get; set; }
        public System.Guid id { get; set; }
        public List<aspnet_User> nombre { get; set; }
 
    }
}