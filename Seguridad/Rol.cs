﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } // Admin, Recepcionista, Medico
        public List<int> Permisos { get; set; }
    }
}
