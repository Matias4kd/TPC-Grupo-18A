<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TurnosMedico.aspx.cs" Inherits="Tp_Cuatrimestral_18A.TurnosMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-6">
        <div class="col-6">
            <label for="txtDni" class="form-label" style="margin-top: 100px; margin-left: 100px">Ingrese su DNI: </label>
            <asp:TextBox runat="server" ID="txtDni" Style="margin-left: 20px" />
            <asp:Button Text="Aceptar" runat="server" OnClick="btnAceptar_Click" />
        </div>
    </div>

    <asp:Panel ID="pnlFormularioPaciente" runat="server" Visible="false" CssClass="container border rounded p-4 shadow-sm">
        <h4 class="text-center mb-4">Formulario de Paciente</h4>

        <div class="row mb-3">
            <label for="txtDniFormulario" class="col-sm-4 col-form-label">DNI:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtDniFormulario" CssClass="form-control" Enabled="false" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtNombre" class="col-sm-4 col-form-label">Nombre:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtApellido" class="col-sm-4 col-form-label">Apellido:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtEmail" class="col-sm-4 col-form-label">Email:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtTelefono" class="col-sm-4 col-form-label">Teléfono:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtDireccion" class="col-sm-4 col-form-label">Dirección:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtPrepaga" class="col-sm-4 col-form-label">Prepaga:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtPrepaga" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtFechaNacimiento" class="col-sm-4 col-form-label">Fecha de Nacimiento:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="form-control" />
            </div>
        </div>

        <div class="text-center">
            <asp:Button Text="Agendar Turno" runat="server" OnClick="btnGuardar_Click" CssClass="btn btn-primary w-100" />
        </div>
    </asp:Panel>
</asp:Content>


