using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using ClinicaMedica;
using System.Data.SqlClient;

namespace Tp_Cuatrimestral_18A
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnIngresar_Click(object sender, EventArgs e) 
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            try
            {
                usuario.NombreUsuario = txtUsername.Text;
                usuario.Contraseña = txtPassword.Text;
                if (usuarioNegocio.Loguear(usuario))
                {
                    usuario = usuarioNegocio.cargarDatosUsuario(usuario.NombreUsuario);
                    Session.Add("Usuario", usuario);
                    Response.Redirect("Pacientes.aspx");
                }
                else 
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error: " + ex.Message; // Mostrar el mensaje de error
                lblError.Visible = true;
            }
        }
    }


}