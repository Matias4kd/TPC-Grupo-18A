<%@ Page Title="Alta de Paciente" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="AltaPaciente.aspx.cs" Inherits="ClinicaMedica.AltaPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>Alta de Paciente</h2>
        </div>
       
    </div>

   

   

    <div class="row mt-4">
        <div class="col-md-6">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" AssociatedControlID="txtNombre" />
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblApellido" runat="server" Text="Apellido:" AssociatedControlID="txtApellido" />
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblDNI" runat="server" Text="DNI:" AssociatedControlID="txtDNI" />
            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI" ErrorMessage="El DNI es obligatorio." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
            <asp:TextBox ID="txtEmail" type="email" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" AssociatedControlID="txtTelefono" />
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono es obligatorio." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" AssociatedControlID="txtDireccion" />
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="La dirección es obligatoria." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblPrepaga" runat="server" Text="Prepaga:" AssociatedControlID="ddlPrepaga" />
            <asp:DropDownList ID="ddlPrepaga" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccionar" Value="0" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPrepaga" runat="server" ControlToValidate="ddlPrepaga" InitialValue="0" ErrorMessage="La selección de prepaga es obligatoria." CssClass="text-danger" Display="Dynamic" />
            <br />
            <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" AssociatedControlID="txtFechaNacimiento" />
            <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
            <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="La fecha de nacimiento es obligatoria." CssClass="text-danger" Display="Dynamic" />
            <br />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary mt-3" OnClick="btnGuardar_Click" />

             <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3" OnClick="btnVolver_Click" CausesValidation="false" />

        </div>
    </div>
</asp:Content>
