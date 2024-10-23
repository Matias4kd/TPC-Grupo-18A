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
        public List<Prepaga> Listar()
        {
            List<Prepaga> lista = new List<Prepaga>();
            AccesoDatos datos = new AccesoDatos();

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
    }

}
