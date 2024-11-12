using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TurnoNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();

            try
            {
                datos.setearConsulta(@"SELECT t.Id, t.Fecha, t.Hora, t.PacienteId, t.MedicoId, t.EspecialidadId, t.Estado, p.Nombre as PacienteNombre, 
                                       m.Nombre as MedicoNombre, e.Nombre as EspecialidadNombre 
                                       FROM Turnos t
                                       JOIN Pacientes p ON t.PacienteId = p.Id
                                       JOIN Medicos m ON t.MedicoId = m.Id
                                       JOIN Especialidades e ON t.EspecialidadId = e.Id");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno
                    {
                        IdTurno = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Hora = (DateTime)datos.Lector["Hora"],
                        Paciente = new Paciente
                        {
                            IdPaciente = (int)datos.Lector["PacienteId"],
                            Nombre = (string)datos.Lector["PacienteNombre"]
                        },
                        Medico = new Medico
                        {
                            IdMedico = (int)datos.Lector["MedicoId"],
                            Nombres = (string)datos.Lector["MedicoNombre"]
                        },
                        Especialidad = new Especialidad
                        {
                            IdEspecialidad = (int)datos.Lector["EspecialidadId"],
                            Nombre = (string)datos.Lector["EspecialidadNombre"]
                        },
                        Estado = (string)datos.Lector["Estado"]
                    };

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
    }
}

