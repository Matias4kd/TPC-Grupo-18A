using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad;
using Negocio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("Select IdUsuario, IdRol from Usuarios where NombreUsuario = @NombreUsuario and Contraseña = @Contraseña");
				datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
				datos.setearParametro("@Contraseña", usuario.Contraseña);

				datos.ejecutarLectura();
				while (datos.lector.Read())
				{
					usuario.IdUsuario = (int)datos.lector["IdUsuario"];
					// int aux = (int)datos.lector["IdRol"]; Discutir

                    return true;
                }
				return false;
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
