using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Producto
    {
        public int Codigo { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public int Precio { get; set; }
        [Required]
        public string Imagen { get; set; }
    }
}