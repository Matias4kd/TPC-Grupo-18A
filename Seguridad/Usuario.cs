using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre {  get; set; }
        public string Apellido {  get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public Rol Rol { get; set; }
    }
}
