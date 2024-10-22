using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class Usuario
    {        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public Rol Rol { get; set; }
    }
}
