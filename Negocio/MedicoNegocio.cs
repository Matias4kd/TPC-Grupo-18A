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
        public List<Medico> Listar()
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT m.IdMedico, u.IdUsuario, u.Nombres, u.Apellidos, m.Matricula, u.Mail FROM Medicos as m INNER JOIN Usuarios as u on  u.IdUsuario = m.IdUsuario ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["m.IdMedico"],
                        IdUsuario = (int)datos.Lector["u.IdUsuario"],
                        Nombres = (string)datos.Lector["u.Nombres"],
                        Apellidos = (string)datos.Lector["u.Apellidos"],
                        Matricula = (string)datos.Lector["m.Matricula"],
                        Email = (string)datos.Lector["u.Mail"]
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
            AccesoDatos datos = new AccesoDatos();

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

        public void Modificar(Medico medico) // REFORMULAR
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "UPDATE Medicos SET Nombre=@Nombre, Apellido=@Apellido, Matricula=@Matricula, Email=@Email WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", medico.Nombres);
                datos.setearParametro("@Apellido", medico.Apellidos);
                datos.setearParametro("@Matricula", medico.Matricula);
                datos.setearParametro("@Email", medico.Email);
                datos.setearParametro("@Id", medico.IdMedico);
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
                // Ajustar la consulta SQL para filtrar por nombre de prepaga y especialidad                      REFORMULAR
                string consulta = @"SELECT DISTINCT m.IdMedico, m.Nombres, m.Apellidos, m.Matricula, m.Mail 
                            FROM Medicos m
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
            {    //                     REFORMULAR
                string consulta = @"SELECT DISTINCT m.IdMedico, m.Nombres, m.Apellidos, m.Matricula, m.Mail 
                    FROM Medicos m
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
                // Consulta SQL para filtrar únicamente por prepaga                      REFORMULAR
                string consulta = @"SELECT DISTINCT m.IdMedico, m.Nombres, m.Apellidos, m.Matricula, m.Mail 
                            FROM Medicos m
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


        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "DELETE FROM Medicos WHERE Id = @Id";
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