using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class PacienteNegocio
    {
        public List<Paciente> Listar()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdPaciente, Nombres, Apellidos, DNI, FechaNacimiento, Mail, Telefono, Direccion FROM Pacientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente
                    {
                        IdPaciente = (long)datos.Lector["IdPaciente"],
                        Nombre = (string)datos.Lector["Nombres"],
                        Apellido = (string)datos.Lector["Apellidos"],
                        DNI = (string)datos.Lector["DNI"],
                        FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"],
                        Email = (string)datos.Lector["Mail"],
                        Telefono = (string)datos.Lector["Telefono"],
                        Direccion = (string)datos.Lector["Direccion"]
                    };

                    lista.Add(paciente);
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

        public void Agregar(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "INSERT INTO Pacientes (Nombres, Apellidos, DNI, FechaNacimiento, Mail, Telefono, Direccion, IdPrepaga) " +
                                  "VALUES (@Nombres, @Apellidos, @DNI, @FechaNacimiento, @Mail, @Telefono, @Direccion, @IdPrepaga)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombres", paciente.Nombre);
                datos.setearParametro("@Apellidos", paciente.Apellido);
                datos.setearParametro("@DNI", paciente.DNI);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@Mail", paciente.Email);
                datos.setearParametro("@Telefono", paciente.Telefono);
                datos.setearParametro("@Direccion", paciente.Direccion);
                datos.setearParametro("@IdPrepaga", paciente.prepaga != null ? paciente.prepaga.IdPrepaga : (object)DBNull.Value);
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

        public void Modificar(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "UPDATE Pacientes SET Nombres=@Nombres, Apellidos=@Apellidos, DNI=@DNI, FechaNacimiento=@FechaNacimiento, " +
                                  "Mail=@Mail, Telefono=@Telefono, Direccion=@Direccion, IdPrepaga=@IdPrepaga WHERE IdPaciente=@IdPaciente";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombres", paciente.Nombre);
                datos.setearParametro("@Apellidos", paciente.Apellido);
                datos.setearParametro("@DNI", paciente.DNI);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@Mail", paciente.Email);
                datos.setearParametro("@Telefono", paciente.Telefono);
                datos.setearParametro("@Direccion", paciente.Direccion);
                datos.setearParametro("@IdPrepaga", paciente.prepaga != null ? paciente.prepaga.IdPrepaga : (object)DBNull.Value);
                datos.setearParametro("@IdPaciente", paciente.IdPaciente);
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


        public Paciente ObtenerPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Paciente paciente = null;

            try
            {
                datos.setearConsulta("SELECT IdPaciente, Nombres, Apellidos, DNI, FechaNacimiento, Mail, Telefono, Direccion, IdPrepaga FROM Pacientes WHERE IdPaciente = @IdPaciente");
                datos.setearParametro("@IdPaciente", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    paciente = new Paciente
                    {
                        IdPaciente = (long)datos.Lector["IdPaciente"],
                        Nombre = (string)datos.Lector["Nombres"],
                        Apellido = (string)datos.Lector["Apellidos"],
                        DNI = (string)datos.Lector["DNI"],
                        FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"],
                        Email = (string)datos.Lector["Mail"],
                        Telefono = (string)datos.Lector["Telefono"],
                        Direccion = (string)datos.Lector["Direccion"]
                    };

                    // Verificar si el campo IdPrepaga no es nulo antes de asignarlo
                    if (datos.Lector["IdPrepaga"] != DBNull.Value)
                    {
                        paciente.prepaga = new Prepaga
                        {
                            IdPrepaga = (int)datos.Lector["IdPrepaga"]
                        };
                    }
                }

                return paciente;
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

        public void Eliminar(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "DELETE FROM Pacientes WHERE IdPaciente = @IdPaciente";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdPaciente", idPaciente);
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


        public void EliminarTurnosPorPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "DELETE FROM Turnos WHERE IdPaciente = @IdPaciente";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdPaciente", idPaciente);
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

        public void EliminarPacienteYTurnos(int idPaciente)
        {
            // Primero elimina los turnos asociados
            EliminarTurnosPorPaciente(idPaciente);

            // Luego elimina el paciente
            Eliminar(idPaciente);
        }


    }
}
    

