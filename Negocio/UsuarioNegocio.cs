﻿using System;
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
				datos.setearConsulta("Select u.IdUsuario, u.NombreUsuario, u.Contraseña, u.Nombres, u.Apellidos, u.Mail, u.Telefono, u.IdRol, r.NombreRol from Usuarios as u " +
                                        "inner join Roles as r on r.IdRol = u.IdRol WHERE u.NombreUsuario = @NombreUsuario");
				datos.setearParametro("@NombreUsuario", Nombreusuario);
				datos.ejecutarLectura();

				while (datos.lector.Read())
				{
					usuario.IdUsuario = (int)datos.lector["u.IdUsuario"];
					usuario.NombreUsuario = (string)datos.lector["u.NombreUsuario"];
					usuario.Contraseña = (string)datos.lector["u.Contraseña"];
					usuario.Nombre = (string)datos.lector["u.Nombres"];
					usuario.Apellido= (string)datos.lector["u.Apellidos"];
					usuario.Mail= (string)datos.lector["u.Mail"];
					usuario.Telefono= (string)datos.lector["u.Telefono"];
					usuario.Rol.RolId = (int)datos.lector["r.IdRol"];
					usuario.Rol.Nombre= (string)datos.lector["r.NombreRol"];

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
    }
}
