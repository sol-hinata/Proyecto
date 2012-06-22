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
        public string observaciones { get; set; }
    }
    public class Articulo
    {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        [Required]
        [Display(Name = "detalle")]
        [UIHint("tinymce_jquery_full"),AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public System.DateTime fecha { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }
    }
    public class verarticulo {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        
        
    }
    public class vercontenido {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public System.DateTime fecha { get; set; }
        public System.Guid id { get; set; }
        public List<aspnet_User> nombre { get; set; }
        public List<articulo> idPerfil { get; set; }
    }
    public class listpendiente {
        public int idPublicacion { get; set; }
        public List<Articulo> titulo { get; set; }
        public List<aspnet_User> nombre { get; set; }
    }
    public class publicar {
        public int idPublicacion { get; set; }
        public int estado { get; set; }
        public int puntuacion { get; set; }
        public System.Guid id { get; set; }
        public String detalle { get; set; }
        public double total { get; set; }
        public System.DateTime fecha { get; set; }

    }
    //LIBRO
    public class Libro
    {
        public int idPublicacion { get; set; }
        public int idArchivo { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public string categoria { get; set; }
        [Required]
        [Display(Name = "detalle")]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        [Required]
        [Display(Name = "autor")]
        public string Autor { get; set; }
        [Required]
        [Display(Name = "año de publicacion")]
        public int Annio_pub { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }

    }
    public class SubirLibro
    {
        public int idPublicacion { get; set; }
        public int idArchivo { get; set; }
        public string titulo { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public int puntuacion { get; set; }
        public string Autor { get; set; }
        public int Annio_pub { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }

        //public System.Guid id { get; set; }
    }
    public class Archivo
    {
        public int idArchivo { get; set; }
        public int idPublicacion { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }
    }
    public class listalibro 
    {
        public int idPublicacion { get; set; }
        public System.Guid id { set; get; }
        public string nombre { set; get; }
        public List<libro> libro { set; get; }
        public List<archivo> archivo { set; get; }
    }
    public class listapendientelibro
    {
        public int idPublicacion { get; set; }
        public System.Guid id { set; get; }
        public string nombre { set; get; }
        public string rutavirtual { get; set; }
        public List<libro> libro { set; get; }
        public List<archivo> archivo { set; get; }
    }
    public class VerLibro {
        public int idPublicacion { get; set; }
        public System.Guid id { set; get; }
        public string nombre { set; get; }
        public List<libro> libro { set; get; }
        public List<archivo> archivo { set; get; }
    }

    public class Curso {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }
    }
    public class listpendientecurso
    {
        public int idPublicacion { get; set; }
        public List<Curso> titulo { get; set; }
        public List<aspnet_User> nombre { get; set; }
    }
    public class vercurso {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }
    
    }
    public class Tutorial {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }
    }
    public class listpendientetutorial
    {
        public int idPublicacion { get; set; }
        public List<Tutorial> titulo { get; set; }
        public List<aspnet_User> nombre { get; set; }
    }
    public class vertutorial
    {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }

    }
    public class observarcion {
        public int idPublicacion { get; set; }
        public string observaciones { get; set; }
    }
    public class mostrararticulos {
        public int idPublicacion { get; set; }
        public int idPerfil{get; set;}
        public System.Guid id { get; set; }
        public System.Guid idus { set; get; }
        public string titulo { set; get; }
        public string detalle { set; get; }
        public string nombre { set; get; }
        public List<articulo> arti { get; set; }
        public List<avatar> av { get; set; }
    
    }
    public class mostrarcursos
    {
        public int idPublicacion { get; set; }
        public System.Guid id { get; set; }
        public System.Guid idus { set; get; }
        public string nombre { set; get; }
        public List<curso> curs { get; set; }
        public List<avatar> av { get; set; }

    }
    public class mostrartutoriales
    {
        public int idPublicacion { get; set; }
        public int idPerfil { get; set; }
        public System.Guid id { get; set; }
        public System.Guid idus { set; get; }
        public string nombre { set; get; }
        public List<tutorial> tuto { get; set; }
        public List<avatar> av { get; set; }

    }
}