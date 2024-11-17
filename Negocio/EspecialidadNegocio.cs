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
        AccesoDatos datos = new AccesoDatos();

        public List<Especialidad> ListarPorPrepaga(string nombrePrepaga)
        {
            List<Especialidad> lista = new List<Especialidad>();

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

            try
            {
                datos.setearConsulta("SELECT IdEspecialidad, NombreEspecialidad from Especialidades");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad
                    {
                        IdEspecialidad = (int)datos.lector["IdEspecialidad"],   
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

        public void GuardarEspecialidadesMedico(Medico medico)
        {
            foreach (Especialidad esp in medico.Especialidades)
            {
                try
                {
                    datos.setearConsulta("Insert into Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (@IdEspecialidad, @IdMedico)" );
                    datos.setearParametro("@IdEspecialidad", esp.IdEspecialidad);
                    datos.setearParametro("@IdMedico", medico.IdMedico);
                    datos.ejecutarAccion();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public List<Especialidad> BuscarEspecialidadesMedico(Medico medico)
        {
            List<Especialidad> especialidades = new List<Especialidad>();

            try
            {
                datos.setearConsulta("Select e.IdEspecialidad, e.NombreEspecialidad from Especialidades as e inner join Especialidades_x_Medico em ON em.IdEspecialidad = e.IdEspecialidad where em.IdMedico = @IdMedico");
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    Especialidad especialidad = new Especialidad();

                    especialidad.IdEspecialidad = (int)datos.lector["IdEspecialidad"];
                    especialidad.Nombre = (string)datos.lector["NombreEspecialidad"];
                    especialidades.Add(especialidad);
                }

                return especialidades;
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
