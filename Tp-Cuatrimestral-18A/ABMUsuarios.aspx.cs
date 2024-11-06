using Dominio;
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
    public partial class ABMUsuarios : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio= new UsuarioNegocio();
        private MedicoNegocio medicoNegocio= new MedicoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                lblMatricula.Visible = false;
                txtMatricula.Visible = false;
            }
        }

        private void CargarRoles()
        {
            RolNegocio rolNegocio = new RolNegocio();

            try
            {
                List<Rol> listaRoles = rolNegocio.Listar();

                ddlRol.DataSource = listaRoles;
                ddlRol.DataTextField = "Nombre"; // Campo a mostrar
                ddlRol.DataValueField = "RolId"; // Valor asociado
                ddlRol.DataBind();

                ddlRol.Items.Insert(0, new ListItem("Seleccionar Rol", "0"));
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtPassword != txtConfirmacionPassword)
            {

            }

            Usuario nuevoUsuario = new Usuario();
            Rol rolSeleccionado = new Rol();

            nuevoUsuario.NombreUsuario = txtNombreUsuario.Text;
            nuevoUsuario.Contraseña = txtPassword.Text;
            nuevoUsuario.Nombre = txtNombre.Text;
            nuevoUsuario.Apellido = txtApellido.Text;
            nuevoUsuario.Mail = txtEmail.Text;
            nuevoUsuario.Telefono = txtTelefono.Text;

            rolSeleccionado.RolId = int.Parse(ddlRol.SelectedValue);
            rolSeleccionado.Nombre = ddlRol.SelectedItem.Text;

            nuevoUsuario.Rol = rolSeleccionado;

            usuarioNegocio.AgregarUsuario(nuevoUsuario);

            if(rolSeleccionado.RolId == 3)
            {
                Medico nuevoMedico = new Medico();

                nuevoMedico.IdUsuario = usuarioNegocio.buscarID(nuevoUsuario.NombreUsuario);
                nuevoMedico.Matricula = txtMatricula.Text;

                medicoNegocio.Agregar(nuevoMedico);

            }

            Response.Redirect("Medicos.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos.aspx");
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e) // REVISAR
        {
            Rol rolSeleccionado = new Rol();

            rolSeleccionado.RolId = int.Parse(ddlRol.SelectedValue);
            rolSeleccionado.Nombre = ddlRol.SelectedItem.Text;

            if (rolSeleccionado.RolId == 3)
            {
                lblMatricula.Visible = true;
                txtMatricula.Visible = true;

            }
            else
            {
                lblMatricula.Visible = false;
                txtMatricula.Visible = false;
            }
        }

    }
}