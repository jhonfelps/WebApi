using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set;}
        public string Apellido { get; set; }
        [Required]
        public string Tipo_Documento { get; set; }
        [Required]
        public string Documento { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}