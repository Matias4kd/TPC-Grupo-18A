using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{

    public enum TipoUsuario
    {
        Administrador= 1,
        Recepcionista = 2,
        Medico = 3
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

    }
}
