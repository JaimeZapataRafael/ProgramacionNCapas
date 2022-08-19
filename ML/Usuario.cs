using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        //[RegularExpression("/^[a-zA-ZÀ-ÿ\u00f1\u00d1]*$/", ErrorMessage = "Solo puede ingresar letras")]
        public string? Nombre { get; set; }

        //[Required(ErrorMessage = "Ingrese el Apellido Paterno")]
        [Display(Name = "Apellido Paterno")]
        //[RegularExpression("/^[a-zA-ZÀ-ÿ\u00f1\u00d1]*$/", ErrorMessage = "Solo puede ingresar letras")]
        public string? ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        //[RegularExpression("/^[a-zA-ZÀ-ÿ\u00f1\u00d1]*$/", ErrorMessage = "Solo puede ingresar letras")]
        public string? ApellidoMaterno { get; set; }

        //[Required(ErrorMessage = "Ingrese un email valido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }


        //[RegularExpression("/^[0-9]*$/", ErrorMessage = "Solo puede ingresar numeros")]
        public string? Telefono { get; set; }
        public ML.Rol Rol { get; set; }
        public string? UserName { get; set; }

        [Display(Name = "Contraseña")]
        public string? Password { get; set; }
        public string? Sexo { get; set; }


        //[RegularExpression("/^[0-9]*$/", ErrorMessage = "Solo puede ingresar numeros")]
        public string? Celular { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string? FechaNacimiento { get; set; }

        [StringLength(18, ErrorMessage = "El CURP no puede exceder los 18 caracteres. ")]
        public string? CURP { get; set; }
        public string? Imagen { get; set; }
        public ML.Direccion Direccion { get; set; }
        public bool Status { get; set; }
        public List<object>? Usuarios { get; set; }
    }
    
}