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
        private Usuario usuario = new Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                lblMatricula.Visible = false;
                txtMatricula.Visible = false;

                if (Request.QueryString["Id"] != null)
                {
                    int IdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                    usuario = usuarioNegocio.cargarDatosUsuario(IdUsuario);
                    cargarCampos(usuario);

                    lblPassword.Visible = false;
                    txtPassword.Visible = false;
                    lblConfirmacionPassword.Visible = false;
                    txtConfirmacionPassword.Visible = false;

                    Usuario usuarioLogueado = new Usuario();
                    usuarioLogueado = (Usuario)Session["Usuario"];
                    if(usuarioLogueado.Rol.RolId == 1)
                    {
                   
                        lblPassword.Visible = true;
                        txtPassword.Visible = true;
                        lblConfirmacionPassword.Visible = true;
                        txtConfirmacionPassword.Visible = true;
                    }

                    if (usuarioLogueado.Rol.RolId == 2)
                    {
                        ddlRol.SelectedIndex= 1;

                    }
                    
                }
            }
        }

        private void CargarRoles()
        {
            RolNegocio rolNegocio = new RolNegocio();

            try
            {
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario"];

                List<Rol> listaRoles = rolNegocio.Listar(usuarioLogueado);

                ddlRol.DataSource = listaRoles;
                ddlRol.DataTextField = "Nombre"; // Campo a mostrar
                ddlRol.DataValueField = "RolId"; // Valor asociado
                ddlRol.DataBind();

                if (usuarioLogueado.Rol.RolId != 2)
                {

                    ddlRol.Items.Insert(0, new ListItem("Seleccionar Rol", "0"));

                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        private void cargarCampos(Usuario usuario)
        {
            if(usuario != null)
            {
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtPassword.Text = usuario.Contraseña;
                txtConfirmacionPassword.Text = usuario.Contraseña;
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtEmail.Text = usuario.Mail;
                txtTelefono.Text = usuario.Telefono;
                ddlRol.SelectedValue = usuario.Rol.RolId.ToString();

                if (usuario.Rol.RolId == 3)
                {
                    lblMatricula.Visible = true;
                    txtMatricula.Visible = true;

                    MedicoNegocio Mnegocio = new MedicoNegocio();

                    Medico medico = Mnegocio.BuscarMatricula(usuario.IdUsuario);

                    txtMatricula.Text = medico.Matricula;
                }

            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            rfvNombreUsuario.Enabled = true;
            rfvPassword.Enabled = true;
            rfvConfirmPassword.Enabled = true;
            rfvNombre.Enabled = true;
            rfvApellido.Enabled = true;
            rfvEmail.Enabled = true;
            rfvTelefono.Enabled = true;
            ddlRol.Enabled = true;

            if(Request.QueryString["Id"] != null)
            {
                
                Rol rolSeleccionado = new Rol();

                Usuario usuarioModificado =  new Usuario();

                usuarioModificado.IdUsuario = usuario.IdUsuario; //no lo esta levantando
                usuarioModificado.NombreUsuario = txtNombreUsuario.Text;
                usuarioModificado.Nombre = txtNombre.Text;
                usuarioModificado.Apellido = txtApellido.Text;
                usuarioModificado.Mail = txtEmail.Text;
                usuarioModificado.Telefono = txtTelefono.Text;

                rolSeleccionado.RolId = int.Parse(ddlRol.SelectedValue);
                rolSeleccionado.Nombre = ddlRol.SelectedItem.Text;

                usuarioModificado.Rol = rolSeleccionado;

                usuarioNegocio.ModificarUsuario(usuarioModificado);

                if (rolSeleccionado.RolId == 3)
                {
                    Medico medico = new Medico();

                    medico.IdUsuario = usuarioNegocio.buscarID(usuarioModificado.NombreUsuario);
                    medico.Matricula = txtMatricula.Text;
                    
                    txtMatricula.Enabled = false; //buscar como deshabilitar el control

                }
            }
            else
            {
                if (txtPassword != txtConfirmacionPassword)
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
            }
            
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("Usuarios.aspx");
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