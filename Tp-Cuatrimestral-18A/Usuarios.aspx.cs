using ClinicaMedica;
using Negocio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_18A
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private UsuarioNegocio negocio = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario"];
                if (usuarioLogueado.Rol.RolId == 2)
                {
                    btnAgregarUsuario.Text = "Agregar Medico";
                    lblTitulo.Text = "Gestión de Medicos";
                }
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            Usuario usuarioLogueado = new Usuario();
            usuarioLogueado = (Usuario)Session["Usuario"];
            gvUsuarios.DataSource = negocio.Listar(usuarioLogueado);
            gvUsuarios.DataBind();
        }

        protected void lnkModificar_Click(object sender, EventArgs e)
        {
            LinkButton btnModificar = (LinkButton)sender;
            string idUsuario = btnModificar.CommandArgument; // Obtiene el ID del paciente desde el CommandArgument
            Response.Redirect("ABMUsuarios.aspx?Id=" + idUsuario); // Pasa el ID en la URL
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMUsuarios.aspx");
        }
    }
}