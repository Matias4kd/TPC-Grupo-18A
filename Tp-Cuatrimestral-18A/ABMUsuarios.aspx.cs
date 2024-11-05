﻿using Dominio;
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

            if(ddlRol.SelectedIndex == 3)
            {
                Medico nuevoMedico = new Medico();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}