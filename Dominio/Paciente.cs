namespace Dominio;
    

    
        public class Paciente
        {
            public int IdPaciente { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DNI { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public Prepagas prepaga { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }

    

