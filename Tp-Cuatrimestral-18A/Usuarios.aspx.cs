﻿using ClinicaMedica;
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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

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
            string idUsuario = btnModificar.CommandArgument;
            Response.Redirect("ABMUsuarios.aspx?Id=" + idUsuario);
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEliminar = (LinkButton)sender;
                int idUsuario = Convert.ToInt32(btnEliminar.CommandArgument);

                UsuarioNegocio uNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario();
                usuario = uNegocio.cargarDatosUsuario(idUsuario);
                uNegocio.EliminarUsuario(usuario);

                lblMensaje.Text = "Usuario eliminado correctamente.";
                lblMensaje.CssClass = "text-success";
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el Usuario: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }

        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMUsuarios.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtBuscarUsuario.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                lblBuscar.Visible = true;
                lblBuscar.Text = "Debe ingresar un nombre de usuario para buscar";
            }
            else
            {
                try
                {
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    int idUsuarioBuscado = usuarioNegocio.buscarID(nombreUsuario);

                    if (idUsuarioBuscado > 0)
                    {
                        Usuario usuarioBuscado = usuarioNegocio.cargarDatosUsuario(idUsuarioBuscado);

                        List<Usuario> listaAuxiliar = new List<Usuario>();
                        listaAuxiliar.Add(usuarioBuscado);

                        gvUsuarios.DataSource = listaAuxiliar;
                        gvUsuarios.DataBind();
                    }
                    else
                    {
                        lblBuscar.Visible = true;
                        lblBuscar.Text = "No se encontró un usuario con ese nombre de usuario.";
                    }
                }
                catch (Exception ex)
                {
                    lblBuscar.Visible = true;
                    lblBuscar.Text = "Error al realizar la búsqueda: " + ex.Message;
                }
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");

        }
    }
}