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
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, FechaNacimiento, Email, Telefono FROM Pacientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente
                    {
                        IdPaciente = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        DNI = (string)datos.Lector["DNI"],
                        FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = (string)datos.Lector["Telefono"]
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
                string consulta = "INSERT INTO Pacientes (Nombre, Apellido, DNI, FechaNacimiento, Email, Telefono) VALUES (@Nombre, @Apellido, @DNI, @FechaNacimiento, @Email, @Telefono)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", paciente.Nombre);
                datos.setearParametro("@Apellido", paciente.Apellido);
                datos.setearParametro("@DNI", paciente.DNI);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@Email", paciente.Email);
                datos.setearParametro("@Telefono", paciente.Telefono);
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
                string consulta = "UPDATE Pacientes SET Nombre=@Nombre, Apellido=@Apellido, DNI=@DNI, FechaNacimiento=@FechaNacimiento, Email=@Email, Telefono=@Telefono WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", paciente.Nombre);
                datos.setearParametro("@Apellido", paciente.Apellido);
                datos.setearParametro("@DNI", paciente.DNI);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@Email", paciente.Email);
                datos.setearParametro("@Telefono", paciente.Telefono);
                datos.setearParametro("@Id", paciente.IdPaciente);
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
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "DELETE FROM Pacientes WHERE Id = @Id";
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

