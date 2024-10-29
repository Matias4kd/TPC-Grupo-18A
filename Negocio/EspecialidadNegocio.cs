using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> ListarPorPrepaga(string nombrePrepaga)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT e.NombreEspecialidad, e.IdEspecialidad
                    FROM Especialidades e
                    INNER JOIN Especialidades_x_Medico em ON em.IdEspecialidad = e.IdEspecialidad
                    INNER JOIN Prepagas_x_Medico pm ON pm.IdMedico = em.IdMedico
                    INNER JOIN Prepagas p ON p.IdPrepaga = pm.IdPrepaga
                    WHERE p.NombrePrepaga = @nombrePrepaga";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nombrePrepaga", nombrePrepaga);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad
                    {
                        Nombre = (string)datos.Lector["NombreEspecialidad"],
                        IdEspecialidad = (int)datos.Lector["IdEspecialidad"]
                    };
                    lista.Add(especialidad);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar especialidades por prepaga", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public List<Especialidad> ListarTodas()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT NombreEspecialidad from Especialidades");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad
                    {
                        
                        Nombre = (string)datos.Lector["NombreEspecialidad"],
                        
                    };

                    lista.Add(especialidad);
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

    }

}
