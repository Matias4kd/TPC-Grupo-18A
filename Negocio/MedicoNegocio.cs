using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


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
                datos.setearConsulta("SELECT Id, Nombre, Apellido, Matricula, Email FROM Medicos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["Id"],
                        Nombres = (string)datos.Lector["Nombre"],
                        Apellidos = (string)datos.Lector["Apellido"],
                        Matricula = (string)datos.Lector["Matricula"],
                        Email = (string)datos.Lector["Email"]
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
                string consulta = "INSERT INTO Medicos (Nombre, Apellido, Matricula, Email) VALUES (@Nombre, @Apellido, @Matricula, @Email)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", medico.Nombres);
                datos.setearParametro("@Apellido", medico.Apellidos);
                datos.setearParametro("@Matricula", medico.Matricula);
                datos.setearParametro("@Email", medico.Email);
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

        public void Modificar(Medico medico)
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
