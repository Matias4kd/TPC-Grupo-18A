using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Seguridad;

namespace Negocio
{
    public class TurnoNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Turno> Listar(Medico medico, DateTime Fecha)
        {
            List<Turno> lista = new List<Turno>();

            try
            {
                datos.setearConsulta(@"SELECT t.IdTurno, t.Fecha, t.Horario, t.IdPaciente, t.IdMedico, t.IdEspecialidad, t.Estado, t.Observaciones, p.Nombres as PacienteNombres, p.Apellidos AS PacienteApellidos,
                                     u.Nombres as MedicoNombre, u.Apellidos as MedicoApellidos, e.NombreEspecialidad as EspecialidadNombre
                                     FROM Turnos t
                                     JOIN Pacientes p ON t.IdPaciente = p.IdPaciente
                                     JOIN Medicos m ON t.IdMedico = m.IdMedico
                                     JOIN Usuarios u ON m.IdUsuario = u.IdUsuario
                                     JOIN Especialidades e ON t.IdEspecialidad = e.IdEspecialidad
                                     WHERE t.Fecha = @Fecha AND t.IdMedico = @IdMedico");

                datos.setearParametro("@Fecha", Fecha);
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();

                    turno.IdTurno = Convert.ToInt32(datos.Lector["IdTurno"]);
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Hora = (TimeSpan)datos.Lector["Horario"];
                    turno.Observaciones = (string)datos.Lector["Observaciones"];
                    Paciente paciente = new Paciente();

                    paciente.IdPaciente = Convert.ToInt32(datos.Lector["IdPaciente"]);
                    paciente.Nombre = (string)datos.Lector["PacienteNombres"];
                    paciente.Apellido = (string)datos.Lector["PacienteApellidos"];

                    turno.Paciente = paciente;

                    Medico medicoturno = new Medico();

                    medicoturno = medico;

                    Especialidad especialidad = new Especialidad();

                    especialidad.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    especialidad.Nombre = (string)datos.Lector["EspecialidadNombre"];

                    turno.Estado = (string)datos.Lector["Estado"];

                    lista.Add(turno);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Turno turno)
        {

            try
            {
                string consulta = @"INSERT INTO Turnos (Fecha, Hora, PacienteId, MedicoId, EspecialidadId, Estado) 
                                    VALUES (@Fecha, @Hora, @PacienteId, @MedicoId, @EspecialidadId, @Estado)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Fecha", turno.Fecha);
                datos.setearParametro("@Hora", turno.Hora);
                datos.setearParametro("@PacienteId", turno.Paciente.IdPaciente);
                datos.setearParametro("@MedicoId", turno.Medico.IdMedico);
                datos.setearParametro("@EspecialidadId", turno.Especialidad.IdEspecialidad);
                datos.setearParametro("@Estado", turno.Estado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<TimeSpan> turnosDisponibles(int idMedico, DateTime fecha)
        {
            List<TimeSpan> turnosDisponibles = new List<TimeSpan>();

            try
            {
                datos.SetearSPTurnos("SP_ObtenerTurnosDisponibles", idMedico, fecha);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {

                    turnosDisponibles.Add(datos.lector.GetTimeSpan(0));

                }

                return turnosDisponibles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Turno turno)
        {

            try
            {
                string consulta = @"UPDATE Turnos SET Fecha=@Fecha, Hora=@Hora, PacienteId=@PacienteId, MedicoId=@MedicoId, EspecialidadId=@EspecialidadId, Estado=@Estado 
                                    WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Fecha", turno.Fecha);
                datos.setearParametro("@Hora", turno.Hora);
                datos.setearParametro("@PacienteId", turno.Paciente.IdPaciente);
                datos.setearParametro("@MedicoId", turno.Medico.IdMedico);
                datos.setearParametro("@EspecialidadId", turno.Especialidad.IdEspecialidad);
                datos.setearParametro("@Estado", turno.Estado);
                datos.setearParametro("@Id", turno.IdTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Eliminar(int id)
        {
            try
            {
                string consulta = "DELETE FROM Turnos WHERE Id = @Id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void GuardarTurnosTrabajoMedico(Medico medico)
        {
            foreach (TurnoTrabajo item in medico.TurnosTrabajo)
            {
                try
                {

                    datos.setearConsulta("Insert into TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (@IdMedico, @HoraInicio, @HoraFin, @DiaTrabajo)");
                    datos.setearParametro("@IdMedico", medico.IdMedico);
                    datos.setearParametro("@HoraInicio", item.HoraInicio);
                    datos.setearParametro("@HoraFin", item.HoraFin);
                    datos.setearParametro("@DiaTrabajo", item.DiaDeLaSemana);
                    datos.ejecutarAccion();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }

        }

        public List<TurnoTrabajo> BuscarTurnosTrabajoMedico(Medico medico)
        {
            List<TurnoTrabajo> turnosTrabajo = new List<TurnoTrabajo>();

            try
            {
                datos.setearConsulta("Select IdTurnoTrabajo, HoraInicio, HoraFin, DiaTrabajo from TurnosTrabajo where IdMedico = @IdMedico");
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    TurnoTrabajo turnoTrabajo = new TurnoTrabajo();

                    turnoTrabajo.IdTurnoTrabajo = (int)datos.lector["IdTurnoTrabajo"];
                    turnoTrabajo.HoraInicio = (TimeSpan)datos.lector["HoraInicio"];
                    turnoTrabajo.HoraFin = (TimeSpan)datos.lector["HoraFin"];
                    turnoTrabajo.DiaDeLaSemana = (string)datos.lector["DiaTrabajo"];
                    turnosTrabajo.Add(turnoTrabajo);
                }

                return turnosTrabajo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool AgendarTurno(Turno turno)
        {
            try
            {
                datos.setearConsulta("Insert into Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones) VALUES (@IdPaciente, @IdMedico, @IdEspecialidad, @Fecha, @Horario, @Observaciones)");
                datos.setearParametro("@IdPaciente", turno.Paciente.IdPaciente);
                datos.setearParametro("@IdMedico", turno.Medico.IdMedico);
                datos.setearParametro("@IdEspecialidad", turno.Especialidad.IdEspecialidad);
                datos.setearParametro("@Fecha", turno.Fecha);
                datos.setearParametro("@Horario", turno.Hora);
                datos.setearParametro("@Observaciones", turno.Observaciones);
                datos.ejecutarAccion();

                return true;
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

