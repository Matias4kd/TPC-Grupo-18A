using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{
    public partial class ModificarPaciente : System.Web.UI.Page
    {
        private PacienteNegocio negocio = new PacienteNegocio();
        private PrepagaNegocio prepagaNegocio = new PrepagaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPrepagas();
                int idPaciente = Convert.ToInt32(Request.QueryString["Id"]);
                CargarDatosPaciente(idPaciente);
            }
        }

        private void CargarPrepagas()
        {
            try
            {
                ddlPrepaga.DataSource = prepagaNegocio.Listar();
                ddlPrepaga.DataTextField = "Nombre";
                ddlPrepaga.DataValueField = "IdPrepaga";
                ddlPrepaga.DataBind();
                ddlPrepaga.Items.Insert(0, new ListItem("Seleccionar Prepaga", "0"));
            }
            catch (Exception ex)
            {
                // Manejo de errores (opcional: mostrar mensaje al usuario)
                throw ex;
            }
        }

        private void CargarDatosPaciente(int idPaciente)
        {
            try
            {
                Paciente paciente = negocio.ObtenerPorID(idPaciente);
                if (paciente != null)
                {
                    txtNombre.Text = paciente.Nombre;
                    txtApellido.Text = paciente.Apellido;
                    txtDNI.Text = paciente.DNI;
                    txtEmail.Text = paciente.Email;
                    txtTelefono.Text = paciente.Telefono;
                    txtDireccion.Text = paciente.Direccion;
                    ddlPrepaga.SelectedValue = paciente.prepaga.IdPrepaga.ToString();
                    txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idPaciente = Convert.ToInt32(Request.QueryString["Id"]);

            Paciente pacienteModificado = new Paciente
            {
                IdPaciente = idPaciente,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                DNI = txtDNI.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                prepaga = new Prepaga { IdPrepaga = int.Parse(ddlPrepaga.SelectedValue) },
                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text)
            };

            negocio.Modificar(pacienteModificado);
            Response.Redirect("Pacientes.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }
    }
}