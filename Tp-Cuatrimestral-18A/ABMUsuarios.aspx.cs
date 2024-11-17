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
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario"];
                CargarRoles();
                CargarEspecialidades();
                CargarPrepagas();
                PopulateTimeDropDown(ddlInicioLunes);
                PopulateTimeDropDown(ddlFinLunes);
                PopulateTimeDropDown(ddlInicioMartes);
                PopulateTimeDropDown(ddlFinMartes);
                PopulateTimeDropDown(ddlInicioMiercoles);
                PopulateTimeDropDown(ddlFinMiercoles);
                PopulateTimeDropDown(ddlInicioJueves);
                PopulateTimeDropDown(ddlFinJueves);
                PopulateTimeDropDown(ddlInicioViernes);
                PopulateTimeDropDown(ddlFinViernes);
                PopulateTimeDropDown(ddlInicioSabado);
                PopulateTimeDropDown(ddlFinSabado);
                PopulateTimeDropDown(ddlInicioDomingo);
                PopulateTimeDropDown(ddlFinDomingo);

                foreach (ListItem item in cblPrepagas.Items)
                {
                    item.Attributes.CssStyle.Add("class", "form-check-input");
                }

                if (usuarioLogueado.Rol.RolId == 1)
                {
                    lblMatricula.Visible = false;
                    txtMatricula.Visible = false;
                    lblEspecialidades.Visible = false;
                    cblEspecialidades.Visible = false;
                    lblPrepagas.Visible = false;
                    cblPrepagas.Visible = false;
                    contenedorHorarios.Visible = false;
                }
                if (usuarioLogueado.Rol.RolId == 2)
                {
                    ddlRol.SelectedIndex = 3;
                    ddlRol.Enabled = false;
                    lblMatricula.Visible = true;
                    txtMatricula.Visible = true;
                    lblEspecialidades.Visible = true;
                    cblEspecialidades.Visible = true;
                    lblPrepagas.Visible = true;
                    cblPrepagas.Visible = true;

                }

                if (Request.QueryString["Id"] != null)
                {
                    int IdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                    usuario = usuarioNegocio.cargarDatosUsuario(IdUsuario);
                    cargarCampos(usuario);

                    lblPassword.Visible = false;
                    txtPassword.Visible = false;
                    lblConfirmacionPassword.Visible = false;
                    txtConfirmacionPassword.Visible = false;

                    
                    if(usuarioLogueado.Rol.RolId == 1)
                    {                   
                        lblPassword.Visible = true;
                        txtPassword.Visible = true;
                        lblConfirmacionPassword.Visible = true;
                        txtConfirmacionPassword.Visible = true;
                    }                   
                    
                }
            }
        }
        private void PopulateTimeDropDown(DropDownList ddl)
        {
            for(int hora = 0; hora < 24; hora++)
            {
                string time = $"{hora:D2}:00";
                ddl.Items.Add(new ListItem(time, time));
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

        private void CargarPrepagas()
        {
            PrepagaNegocio prepagaNegocio = new PrepagaNegocio();

            try
            {
               
                List<Prepaga> listaRoles = prepagaNegocio.Listar();

                cblPrepagas.DataSource = listaRoles;
                cblPrepagas.DataTextField = "Nombre"; // Campo a mostrar
                cblPrepagas.DataValueField = "IdPrepaga"; // Valor asociado
                cblPrepagas.DataBind();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEspecialidades()
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            try
            {                
                List<Especialidad> listaEspecialidades = especialidadNegocio.ListarTodas();

                cblEspecialidades.DataSource = listaEspecialidades;
                cblEspecialidades.DataTextField = "Nombre"; // Campo a mostrar
                cblEspecialidades.DataValueField = "IdEspecialidad"; // Valor asociado
                cblEspecialidades.DataBind();    
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
                    List<Prepaga> prepagasSeleccionadas = new List<Prepaga>();
                    List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();
                    List<TurnoTrabajo> turnosSeleccionados = new List<TurnoTrabajo>();

                    medico.IdUsuario = usuarioNegocio.buscarID(usuarioModificado.NombreUsuario);
                    medico.Matricula = txtMatricula.Text;
                    medico.Prepagas = prepagasSeleccionadas;
                    medico.Especialidades = especialidadesSeleccionadas;
                    medico.TurnosTrabajo = turnosSeleccionados;

                    txtMatricula.Enabled = false; //buscar como deshabilitar el control

                    // hacer metodo para modificar medico

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

                //usuarioNegocio.AgregarUsuario(nuevoUsuario);

                if(rolSeleccionado.RolId == 3)
                {
                    Medico nuevoMedico = new Medico();

                    List<Prepaga> prepagasSeleccionadas = new List<Prepaga>();

                    foreach (ListItem item in cblPrepagas.Items)
                    {
                        if (item.Selected)
                        {
                            Prepaga seleccionPrepaga= new Prepaga();
                            seleccionPrepaga.IdPrepaga = int.Parse(item.Value);
                            seleccionPrepaga.Nombre = item.Text;
                            prepagasSeleccionadas.Add(seleccionPrepaga);
                        }
                    }    

                    List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();


                    foreach (ListItem item in cblPrepagas.Items)
                    {
                        if (item.Selected)
                        {
                            Especialidad seleccionEspecialidad= new Especialidad();
                            seleccionEspecialidad.IdEspecialidad = int.Parse(item.Value);
                            seleccionEspecialidad.Nombre = item.Text;
                            especialidadesSeleccionadas.Add(seleccionEspecialidad);
                        }
                    }

                    List<TurnoTrabajo> turnosSeleccionados = new List<TurnoTrabajo>();

                    nuevoMedico.IdUsuario = usuarioNegocio.buscarID(nuevoUsuario.NombreUsuario);
                    nuevoMedico.Matricula = txtMatricula.Text;
                    nuevoMedico.Prepagas = prepagasSeleccionadas;
                    nuevoMedico.Especialidades = especialidadesSeleccionadas;
                    nuevoMedico.TurnosTrabajo = turnosSeleccionados;

                    medicoNegocio.Agregar(nuevoMedico);

                    // agregar a especialidades por medico, turnostrabajo y prepagas por medico

                }
            }
            
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("Usuarios.aspx");
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rol rolSeleccionado = new Rol();

            rolSeleccionado.RolId = int.Parse(ddlRol.SelectedValue);
            rolSeleccionado.Nombre = ddlRol.SelectedItem.Text;

            if (rolSeleccionado.RolId == 3)
            {
                lblMatricula.Visible = true;
                txtMatricula.Visible = true;
                lblEspecialidades.Visible = true;
                cblEspecialidades.Visible = true;
                lblPrepagas.Visible = true;
                cblPrepagas.Visible = true;
                contenedorHorarios.Visible = true;
                contenedorHorarios.Visible = true;


            }
            else
            {
                lblMatricula.Visible = false;
                txtMatricula.Visible = false;
                lblEspecialidades.Visible = false;
                cblEspecialidades.Visible = false;
                lblPrepagas.Visible = false;
                cblPrepagas.Visible = false; 
                contenedorHorarios.Visible = false;
            }
        }


    }
}