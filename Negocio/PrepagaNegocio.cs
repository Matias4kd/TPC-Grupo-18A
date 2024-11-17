using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PrepagaNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Prepaga> Listar()
        {
            List<Prepaga> lista = new List<Prepaga>();

            try
            {
                datos.setearConsulta("SELECT IdPrepaga, NombrePrepaga FROM Prepagas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prepaga prepaga = new Prepaga
                    {
                        IdPrepaga = (int)datos.Lector["IdPrepaga"],
                        Nombre = (string)datos.Lector["NombrePrepaga"]
                    };

                    lista.Add(prepaga);
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

        public void GuardarPrepagasMedico(Medico medico)
        {
            foreach (Prepaga prep in medico.Prepagas)
            {
                try
                {
                    datos.setearConsulta("Insert into Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (@IdPrepaga, @IdMedico)");
                    datos.setearParametro("@IdPrepaga", prep.IdPrepaga);
                    datos.setearParametro("@IdMedico", medico.IdMedico);
                    datos.ejecutarAccion();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public List<Prepaga> BuscarPrepagasMedico(Medico medico)
        {
            List<Prepaga> prepagas = new List<Prepaga>();

            try
            {
                datos.setearConsulta("Select p.IdPrepaga, p.NombrePrepaga from Prepagas as p inner join Prepagas_x_Medico pm ON pm.IdPrepaga = p.IdPrepaga where pm.IdMedico = @IdMedico");
                datos.setearParametro("@IdMedico", medico.IdMedico);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    Prepaga prepaga = new Prepaga();

                    prepaga.IdPrepaga = (int)datos.lector["IdPrepaga"];
                    prepaga.Nombre = (string)datos.lector["NombrePrepaga"];
                    prepagas.Add(prepaga);
                }

                return prepagas;
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
