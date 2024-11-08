using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using ClinicaMedica;

namespace Tp_Cuatrimestral_18A
{
    public partial class TurnosMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string idMedicoAnterior = Request.QueryString["IdMedico"];
                if (int.TryParse(idMedicoAnterior, out int idMedico))
                {
                    // Lógica para cargar los detalles del médico usando el idMedico
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            int dni;
            
            if (int.TryParse(txtDni.Text, out dni))
            {

                txtDniFormulario.Text = txtDni.Text;
                PacienteNegocio negociopaciente = new PacienteNegocio();
                Paciente paciente = negociopaciente.ObtenerPorDNI(dni);

                // Llamada al método para buscar el paciente en la base de datos
                if (paciente != null)
                {
                    // Si el paciente existe, muestra el formulario con datos precargados
                    pnlFormularioPaciente.Visible = true;
                    txtNombre.Text = paciente.Nombre;
                    txtApellido.Text = paciente.Apellido;
                    txtEmail.Text = paciente.Email;
                    txtTelefono.Text = paciente.Telefono;
                    txtDireccion.Text = paciente.Direccion;
                    txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                }
                else
                {
                    // Si el paciente no existe, muestra el formulario vacío
                    pnlFormularioPaciente.Visible = true;
                    LimpiarCampos();
                }
            }
        }

        private bool BuscarPacientePorDni(int dni)
        {
            // Conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["TuConexion"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Nombre, Apellido, Telefono FROM Pacientes WHERE DNI = @DNI";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DNI", dni);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Si el paciente existe, cargar los datos en los campos
                        txtNombre.Text = reader["Nombre"].ToString();
                        txtApellido.Text = reader["Apellido"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        return true;
                    }
                }
            }
            return false;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            // Agrega cualquier otro campo que necesites limpiar
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Código para guardar o actualizar los datos del paciente en la base de datos
        }
    }
}


