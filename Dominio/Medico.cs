using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio;
    

    public class Medico
    {

        public int IdMedico { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        
        
        public string Email { get; set; }
        public string Matricula { get; set; }

        public List<Prepagas> Prepagas { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public List<TurnoTrabajo> TurnosTrabajo { get; set; }


    }

