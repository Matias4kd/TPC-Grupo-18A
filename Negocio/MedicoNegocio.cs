using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class MedicoNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Medico> Listar()
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT m.IdMedico, u.IdUsuario, u.Nombres, u.Apellidos, m.Matricula, u.Mail 
                               FROM Medicos m
                               INNER JOIN Usuarios u ON u.IdUsuario = m.IdUsuario");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombres = (string)datos.Lector["Nombres"],
                        Apellidos = (string)datos.Lector["Apellidos"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Mail"]
                    };

                    lista.Add(medico);
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


        public void Agregar(Medico medico)
        {
                        
            try
            {
                string consulta = "INSERT INTO Medicos (IdUsuario, Matricula) VALUES (@IdUsuario, @Matricula)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdUsuario", medico.IdUsuario);
                datos.setearParametro("@Matricula", medico.Matricula);
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

        public int ObtenerID(Medico Medico)
        {
            try
            {
                int id;                

                datos.setearConsulta("Select IdMedico from Medicos where Matricula = @Matricula");
                datos.setearParametro("@Matricula", Medico.Matricula);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    id = (int)datos.Lector["IdMedico"];
                    return id;
                }
                return -1;
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

        public List<Medico> ListarPorPrepagaYEspecialidad(string nombrePrepaga, string nombreEspecialidad)
        {
            if (string.IsNullOrEmpty(nombrePrepaga) || string.IsNullOrEmpty(nombreEspecialidad))
            {
                throw new ArgumentException("Nombre de prepaga o especialidad no válido");
            }

            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT DISTINCT m.IdMedico, u.Nombres, u.Apellidos, m.Matricula, u.Mail 
                            FROM Medicos m
                            INNER JOIN Usuarios u ON u.IdUsuario = m.IdUsuario
                            INNER JOIN Prepagas_x_Medico pm ON m.IdMedico = pm.IdMedico
                            INNER JOIN Especialidades_x_Medico em ON m.IdMedico = em.IdMedico
                            INNER JOIN Prepagas p ON pm.IdPrepaga = p.IdPrepaga
                            INNER JOIN Especialidades e ON em.IdEspecialidad = e.IdEspecialidad
                            WHERE p.NombrePrepaga = @nombrePrepaga
                            AND e.NombreEspecialidad = @nombreEspecialidad";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nombrePrepaga", nombrePrepaga);
                datos.setearParametro("@nombreEspecialidad", nombreEspecialidad);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        Nombres = (string)datos.Lector["Nombres"],
                        Apellidos = (string)datos.Lector["Apellidos"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Mail"]
                    };
                    lista.Add(medico);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener médicos por prepaga y especialidad", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Medico> ListarPorEspecialidad(string nombreEspecialidad)
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT DISTINCT m.IdMedico, u.Nombres, u.Apellidos, m.Matricula, u.Mail 
                            FROM Medicos m
                            INNER JOIN Usuarios u ON u.IdUsuario = m.IdUsuario
                            INNER JOIN Especialidades_x_Medico em ON m.IdMedico = em.IdMedico
                            INNER JOIN Especialidades e ON em.IdEspecialidad = e.IdEspecialidad
                            WHERE e.NombreEspecialidad = @nombreEspecialidad";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nombreEspecialidad", nombreEspecialidad);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        Nombres = (string)datos.Lector["Nombres"],
                        Apellidos = (string)datos.Lector["Apellidos"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Mail"]
                    };
                    lista.Add(medico);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener médicos por especialidad", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Medico> ListarPorPrepaga(string nombrePrepaga)
        {
            if (string.IsNullOrEmpty(nombrePrepaga))
            {
                throw new ArgumentException("Nombre de prepaga no válido");
            }

            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT DISTINCT m.IdMedico, u.Nombres, u.Apellidos, m.Matricula, u.Mail 
                            FROM Medicos m
                            INNER JOIN Usuarios u ON u.IdUsuario = m.IdUsuario
                            INNER JOIN Prepagas_x_Medico pm ON m.IdMedico = pm.IdMedico
                            INNER JOIN Prepagas p ON pm.IdPrepaga = p.IdPrepaga
                            WHERE p.NombrePrepaga = @nombrePrepaga";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nombrePrepaga", nombrePrepaga);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        Nombres = (string)datos.Lector["Nombres"],
                        Apellidos = (string)datos.Lector["Apellidos"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Mail"]
                    };
                    lista.Add(medico);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener médicos por prepaga", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Medico ObtenerPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Medico medico = null;

            try
            {
                datos.setearConsulta("SELECT m.IdMedico, m.IdUsuario, m.Matricula, u.Nombres, u.Apellidos, u.Mail " +
                                     "FROM Medicos AS m " +
                                     "JOIN Usuarios AS u ON m.IdUsuario = u.IdUsuario " +
                                     "WHERE m.IdMedico = @IdMedico");
                datos.setearParametro("@IdMedico", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        Nombres = (string)datos.Lector["Nombres"],
                        Apellidos = (string)datos.Lector["Apellidos"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Mail"],
                        Especialidades = new List<Especialidad>()
                    };
                }

                datos.cerrarConexion();

                if (medico != null)
                {
                    datos.setearConsulta("SELECT e.IdEspecialidad, e.NombreEspecialidad " +
                                         "FROM Especialidades AS e " +
                                         "JOIN Especialidades_x_Medico AS em ON e.IdEspecialidad = em.IdEspecialidad " +
                                         "WHERE em.IdMedico = @IdMedico");
                    datos.setearParametro("@IdMedico", id);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Especialidad especialidad = new Especialidad
                        {
                            IdEspecialidad = (int)datos.Lector["IdEspecialidad"],
                            Nombre = (string)datos.Lector["NombreEspecialidad"]
                        };
                        medico.Especialidades.Add(especialidad);
                    }
                }

                return medico;
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
            AccesoDatos datos = new AccesoDatos();
            DateTime fechabaja = DateTime.Now;

            try
            {
                string consulta = "Update Medicos set FechaDeBaja = @Fechadebaja WHERE Id = @Id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Id", id);
                datos.setearParametro("@Fechadebaja", fechabaja);
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

        public Medico BuscarPorIDUsuario(int idUsuario) 
        {

            Medico medico = new Medico();
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            PrepagaNegocio prepagaNegocio = new PrepagaNegocio();

            try
            {
                datos.setearConsulta("SELECT m.IdMedico, m.IdUsuario, m.Matricula, u.Nombres, u.Apellidos, u.Mail FROM Medicos as m JOIN Usuarios as u ON m.IdUsuario = u.IdUsuario WHERE m.IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    medico.IdMedico = (int)datos.Lector["IdMedico"];
                    medico.Nombres = (string)datos.Lector["Nombres"];
                    medico.Apellidos = (string)datos.Lector["Apellidos"];
                    medico.Matricula = (string)datos.Lector["Matricula"];
                    medico.Email = (string)datos.Lector["Mail"];

                    medico.Especialidades = especialidadNegocio.BuscarEspecialidadesMedico(medico);
                    medico.Prepagas = prepagaNegocio.BuscarPrepagasMedico(medico);
                    medico.TurnosTrabajo = turnoNegocio.BuscarTurnosTrabajoMedico(medico);
                    
                }

                return medico;
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

        public void Modificar(Medico medico)
        {
            cambiarHorariosMedico(medico);
            cambiarEspecialidadesPorMedico(medico);
            cambiarPrepagasPorMedico(medico);
        }

        private void cambiarEspecialidadesPorMedico(Medico medico)
        {
            try
            {
                datos.setearConsulta("DELETE FROM Especialidades_x_Medico WHERE IdMedico = @IdMedico;");
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarAccion();

                foreach (Especialidad item in medico.Especialidades)
                {
                    datos.setearConsulta("INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (@IdEspecialidad, @IdMedico)");
                    datos.setearParametro("@IdMedico", medico.IdMedico);
                    datos.setearParametro("@IdEspecialidad", item.IdEspecialidad);
                    datos.ejecutarAccion();
                }
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

        private void cambiarHorariosMedico(Medico medico)
        {
            try
            {
                foreach (TurnoTrabajo item in medico.TurnosTrabajo)
                {
                    datos.setearConsulta("UPDATE TurnosTrabajo SET HoraInicio = @HoraInicio, HoraFin = @HoraFin where IdTurnoTrabajo = @IdTurnoTrabajo");
                    datos.setearParametro("@HoraInicio", item.HoraInicio);
                    datos.setearParametro("@HoraFin", item.HoraFin);
                    datos.setearParametro("@IdTurnoTrabajo", item.IdTurnoTrabajo);
                    datos.ejecutarAccion();
                }

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

        private void cambiarPrepagasPorMedico(Medico medico)
        {
            try
            {
                datos.setearConsulta("DELETE FROM Prepagas_x_Medico  WHERE IdMedico = @IdMedico;");
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarAccion();

                foreach (Prepaga item in medico.Prepagas)
                {
                    datos.setearConsulta("INSERT INTO Prepagas_x_Medico  (IdPrepaga, IdMedico) VALUES (@IdPrepaga, @IdMedico)");
                    datos.setearParametro("@IdMedico", medico.IdMedico);
                    datos.setearParametro("@IdPrepaga", item.IdPrepaga);
                    datos.ejecutarAccion();

                }
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