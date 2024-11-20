using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public Especialidad Especialidad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; } // Nuevo, Reprogramado, Cancelado, etc.
    }
}
