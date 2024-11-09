using Dominio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RolNegocio
    {
        public List<Rol> Listar(Usuario usuarioSesion)
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {   
                if(usuarioSesion.Rol.RolId == 2) 
                {

                    datos.setearConsulta("SELECT IdRol, NombreRol FROM Roles where IdRol = 3");
                    datos.ejecutarLectura();

                } 
                else { 

                    datos.setearConsulta("SELECT IdRol, NombreRol FROM Roles");
                    datos.ejecutarLectura();
                }

                while (datos.Lector.Read())
                {
                    Rol rol = new Rol();
                    
                    rol.RolId = Convert.ToInt32(datos.Lector["IdRol"]);
                    rol.Nombre = (string)datos.Lector["NombreRol"];
                    

                    lista.Add(rol);
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
