using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public Empleado()
        {
            Dependientes = new HashSet<Dependiente>();
        }

        public string NumeroEmpleado { get; set; } = null!;
        public string? Rfc { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Nss { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? IdEmpresa { get; set; }
        public string? Foto { get; set; }

        public virtual Empresa? IdEmpresaNavigation { get; set; }
        public virtual ICollection<Dependiente> Dependientes { get; set; }

        //Alias de Empleado

        public string NombreEmpresa { get; set; }
    }
}
