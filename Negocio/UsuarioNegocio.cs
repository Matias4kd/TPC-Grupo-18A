using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad;
using Negocio;
using System.Reflection;
using Dominio;



namespace Negocio
{
    public class UsuarioNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public bool Loguear(Usuario usuario)
        {

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
		public Usuario cargarDatosUsuario(string Nombreusuario)
		{
			Usuario usuario = new Usuario();
			try
			{
				datos.setearConsulta("Select u.IdUsuario, u.NombreUsuario, u.Contraseña, u.Nombres, u.Apellidos, u.Mail, u.Telefono, u.IdRol, r.NombreRol from Usuarios as u inner join Roles as r on r.IdRol = u.IdRol WHERE u.NombreUsuario = @NombreUsuario");
				datos.setearParametro("@NombreUsuario", Nombreusuario);
				datos.ejecutarLectura();

				while (datos.lector.Read())
				{
					usuario.IdUsuario = (int)datos.lector["IdUsuario"];
					usuario.NombreUsuario = (string)datos.lector["NombreUsuario"];
					usuario.Contraseña = (string)datos.lector["Contraseña"];
					usuario.Nombre = (string)datos.lector["Nombres"];
					usuario.Apellido= (string)datos.lector["Apellidos"];
					usuario.Mail= (string)datos.lector["Mail"];
					usuario.Telefono= (string)datos.lector["Telefono"];
					usuario.Rol = new Rol();
					usuario.Rol.RolId = Convert.ToInt32(datos.lector["IdRol"]);
					usuario.Rol.Nombre= (string)datos.lector["NombreRol"];

					return usuario;
				}

				return null;
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
        public Usuario cargarDatosUsuario(int id)
        {
            Usuario usuario = new Usuario();
            try
            {
                datos.setearConsulta("Select u.IdUsuario, u.NombreUsuario, u.Contraseña, u.Nombres, u.Apellidos, u.Mail, u.Telefono, u.IdRol, r.NombreRol from Usuarios as u inner join Roles as r on r.IdRol = u.IdRol WHERE u.IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", id);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    usuario.IdUsuario = (int)datos.lector["IdUsuario"];
                    usuario.NombreUsuario = (string)datos.lector["NombreUsuario"];
                    usuario.Contraseña = (string)datos.lector["Contraseña"];
                    usuario.Nombre = (string)datos.lector["Nombres"];
                    usuario.Apellido = (string)datos.lector["Apellidos"];
                    usuario.Mail = (string)datos.lector["Mail"];
                    usuario.Telefono = (string)datos.lector["Telefono"];
                    usuario.Rol = new Rol();
                    usuario.Rol.RolId = Convert.ToInt32(datos.lector["IdRol"]);
                    usuario.Rol.Nombre = (string)datos.lector["NombreRol"];

                    return usuario;
                }

                return null;
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

		public void ModificarUsuario(Usuario usuario)
		{
            try
            {
                string consulta = "Update Usuarios set NombreUsuario=@NombreUsuario, Nombres=@Nombres, Apellidos=@Apellidos," +
                                    " Mail=@Mail, Telefono=@telefono, IdRol=@IdRol" +
                                    " where IdUsuario = @IdUsuario";
                datos.setearConsulta(consulta);
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);                
                datos.setearParametro("@Nombres", usuario.Nombre);
                datos.setearParametro("@Apellidos", usuario.Apellido);
                datos.setearParametro("@Mail", usuario.Mail);
                datos.setearParametro("@telefono", usuario.Telefono);
                datos.setearParametro("@IdRol", usuario.Rol.RolId);
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
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

        public void AgregarUsuario(Usuario usuario)
		{
			try
			{
				string consulta = "INSERT INTO Usuarios(NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol)" +
									"Values(@NombreUsuario, @Contraseña, @Nombres, @Apellidos, @Mail, @telefono,@IdRol)";
				datos.setearConsulta(consulta);
				datos.setearParametro("@NombreUsuario",usuario.NombreUsuario);
				datos.setearParametro("@Contraseña",usuario.Contraseña);
				datos.setearParametro("@Nombres",usuario.Nombre);
				datos.setearParametro("@Apellidos",usuario.Apellido);
				datos.setearParametro("@Mail",usuario.Mail);
				datos.setearParametro("@telefono",usuario.Telefono);
				datos.setearParametro("@IdRol",usuario.Rol.RolId);
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

		public int buscarID(string nombreUsuario)
		{
			int id = new int();
			try
			{
                datos.setearConsulta("SELECT IdUsuario FROM Usuarios WHERE NombreUsuario = @nombreUsuario");
                datos.setearParametro("@nombreUsuario", nombreUsuario);
                datos.ejecutarLectura();

                while (datos.lector.Read())
                {
                    id = (int)datos.lector["IdUsuario"];

                    return id;
                }

                return -1;
            }
			catch (Exception)
			{

				throw;
			}
			finally
			{
				datos.cerrarConexion();
			}
            
		}

		public List<Usuario> Listar(Usuario usuarioLogueado)
		{
            List<Usuario> lista = new List<Usuario>();

            try
            {
				if(usuarioLogueado.Rol.RolId == 2)
				{
                    datos.setearConsulta("SELECT u.IdUsuario, u.NombreUsuario, u.Contraseña,u.Nombres, u.Apellidos, u.Mail, u.Telefono, r.IdRol, r.NombreRol FROM Usuarios as u inner join Roles as r on u.IdRol = r.IdRol where u.IdRol = 3");
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("SELECT u.IdUsuario, u.NombreUsuario, u.Contraseña,u.Nombres, u.Apellidos, u.Mail, u.Telefono, r.IdRol, r.NombreRol FROM Usuarios as u inner join Roles as r on u.IdRol = r.IdRol ");
                    datos.ejecutarLectura();
                }
                

                while (datos.Lector.Read())
                {
					Usuario usuario = new Usuario();


					usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
					usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
					usuario.Nombre = (string)datos.Lector["Nombres"];
					usuario.Apellido = (string)datos.Lector["Apellidos"];
					usuario.Mail = (string)datos.Lector["Mail"];
					usuario.Telefono = (string)datos.Lector["Telefono"];
                    
					usuario.Rol = new Rol();
                    usuario.Rol.RolId = Convert.ToInt32(datos.lector["IdRol"]);
                    usuario.Rol.Nombre = (string)datos.lector["NombreRol"];


                    lista.Add(usuario);
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
