using Dominio;
using Negocio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    contenedorInfoMedico.Visible = false;
                    
                }
                if (usuarioLogueado.Rol.RolId == 2)
                {
                    lblTitulo.Text = "Alta de Medicos: ";
                    ddlRol.Enabled = false;
                    ddlRol.SelectedIndex = 3;
                    contenedorInfoMedico.Visible = true;                    
                }

                if (Request.QueryString["Id"] != null)
                {
                    int IdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                    usuario = usuarioNegocio.cargarDatosUsuario(IdUsuario);
                    txtMatricula.Enabled = false;
                    cargarCampos(usuario);

                    contenedorPasswords.Visible = false;
                                                            
                    if(usuarioLogueado.Rol.RolId == 1)
                    {
                        contenedorPasswords.Visible = true;                        
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
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtEmail.Text = usuario.Mail;
                txtTelefono.Text = usuario.Telefono;
                ddlRol.SelectedValue = usuario.Rol.RolId.ToString();

                if (usuario.Rol.RolId == 3)
                {
                    contenedorInfoMedico.Visible = true;                    

                    MedicoNegocio Mnegocio = new MedicoNegocio();

                    Medico medico = Mnegocio.BuscarPorIDUsuario(usuario.IdUsuario);

                    txtMatricula.Text = medico.Matricula;

                    foreach(ListItem item in cblEspecialidades.Items)
                    {
                        if (medico.Especialidades.Any(e => e.IdEspecialidad == int.Parse(item.Value)))
                        {
                            item.Selected = true;
                        }
                    }

                    foreach (ListItem item in cblPrepagas.Items)
                    {
                        if (medico.Prepagas.Any(p => p.IdPrepaga == int.Parse(item.Value)))
                        {
                            item.Selected = true;
                        }
                    }


                }

            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            rfvNombreUsuario.Enabled = true;
            rfvPassword.Enabled = true;
            rfvNombre.Enabled = true;
            rfvApellido.Enabled = true;
            rfvEmail.Enabled = true;
            rfvTelefono.Enabled = true;
            ddlRol.Enabled = true;

            if(Request.QueryString["Id"] != null)
            {
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario"];

                Rol rolSeleccionado = new Rol();

                Usuario usuarioModificado =  new Usuario();

                usuarioModificado.IdUsuario = usuario.IdUsuario; //no lo esta levantando
                usuarioModificado.NombreUsuario = txtNombreUsuario.Text;
                usuarioModificado.Nombre = txtNombre.Text;
                usuarioModificado.Apellido = txtApellido.Text;
                usuarioModificado.Mail = txtEmail.Text;
                usuarioModificado.Telefono = txtTelefono.Text;

                if(usuarioLogueado.Rol.RolId == 1)
                {
                    usuarioModificado.Contraseña = txtPassword.Text;
                }

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

                    // hacer metodo para modificar medico

                }
            }
            else
            {
                
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
                    EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
                    PrepagaNegocio prepagaNegocio = new PrepagaNegocio();
                    TurnoNegocio turnoNegocio = new TurnoNegocio();

                    Medico nuevoMedico = new Medico();


                    nuevoMedico.IdUsuario = usuarioNegocio.buscarID(nuevoUsuario.NombreUsuario);
                    nuevoMedico.Matricula = txtMatricula.Text;
                    nuevoMedico.Prepagas = obtenerPrepagasSeleccionadas();
                    nuevoMedico.Especialidades = ObtenerEspecialidadesSeleccionadas();
                    nuevoMedico.TurnosTrabajo = ObtenerTurnosTrabajoSeleccionados();

                    medicoNegocio.Agregar(nuevoMedico);

                    nuevoMedico.IdMedico = medicoNegocio.ObtenerID(nuevoMedico);

                    especialidadNegocio.GuardarEspecialidadesMedico(nuevoMedico);
                    prepagaNegocio.GuardarPrepagasMedico(nuevoMedico);
                    turnoNegocio.GuardarTurnosTrabajoMedico(nuevoMedico);
                }
            }
            
            Response.Redirect("Usuarios.aspx");
        }

        private List<TurnoTrabajo> ObtenerTurnosTrabajoSeleccionados()
        {
            List<TurnoTrabajo> turnosSeleccionados = new List<TurnoTrabajo>();
                        
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Lunes",
                HoraInicio = TimeSpan.Parse(ddlInicioLunes.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinLunes.SelectedItem.Text)
            });
                        
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Martes",
                HoraInicio = TimeSpan.Parse(ddlInicioMartes.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinMartes.SelectedItem.Text)
            });
            
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Miércoles",
                HoraInicio = TimeSpan.Parse(ddlInicioMiercoles.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinMiercoles.SelectedItem.Text)
            });
            
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Jueves",
                HoraInicio = TimeSpan.Parse(ddlInicioJueves.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinJueves.SelectedItem.Text)
            });
            
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Viernes",
                HoraInicio = TimeSpan.Parse(ddlInicioViernes.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinViernes.SelectedItem.Text)
            });
            
            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Sábado",
                HoraInicio = TimeSpan.Parse(ddlInicioSabado.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinSabado.SelectedItem.Text)
            });

            turnosSeleccionados.Add(new TurnoTrabajo
            {
                DiaDeLaSemana = "Domingo",
                HoraInicio = TimeSpan.Parse(ddlInicioDomingo.SelectedItem.Text),
                HoraFin = TimeSpan.Parse(ddlFinDomingo.SelectedItem.Text)
            });

            return turnosSeleccionados;
        }

        private List<Prepaga> obtenerPrepagasSeleccionadas()
        {
            List<Prepaga> prepagasSeleccionadas = new List<Prepaga>();

            foreach (ListItem item in cblPrepagas.Items)
            {
                if (item.Selected)
                {
                    Prepaga seleccionPrepaga = new Prepaga();
                    seleccionPrepaga.IdPrepaga = int.Parse(item.Value);
                    seleccionPrepaga.Nombre = item.Text;
                    prepagasSeleccionadas.Add(seleccionPrepaga);
                }
            }
            return prepagasSeleccionadas;
        }

        private List<Especialidad> ObtenerEspecialidadesSeleccionadas()
        {
            List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();


            foreach (ListItem item in cblEspecialidades.Items)
            {
                if (item.Selected)
                {
                    Especialidad seleccionEspecialidad = new Especialidad();
                    seleccionEspecialidad.IdEspecialidad = int.Parse(item.Value);
                    seleccionEspecialidad.Nombre = item.Text;
                    especialidadesSeleccionadas.Add(seleccionEspecialidad);
                }
            }
            return especialidadesSeleccionadas;
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
                contenedorInfoMedico.Visible = true;
            }
            else
            {
                contenedorInfoMedico.Visible = false;
            }
        }


    }
}