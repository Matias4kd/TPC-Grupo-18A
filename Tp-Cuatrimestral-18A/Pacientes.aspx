<%@ Page Title="Gestión de Pacientes" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs"  Inherits="ClinicaMedica.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>Gestión de Pacientes</h2>
            <asp:Button ID="btnAgregarPaciente" runat="server" Text="Agregar Paciente" CssClass="btn btn-primary" OnClick="btnAgregarPaciente_Click" />
        </div>
    </div>

    <div class="row mt-3">
        <div class="col-md-4">
            <label for="txtBuscarDNI">Buscar por DNI:</label>
            <asp:TextBox ID="txtBuscarDNI" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary mt-2" OnClick="btnBuscar_Click" />
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-12">
            <asp:GridView ID="gvPacientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                EmptyDataText="No se encontraron pacientes con el DNI ingresado">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModificar" runat="server" Text="Modificar" CssClass="btn btn-warning btn-sm" OnClick="btnModificar_Click"
                                CommandName="Modificar" CommandArgument='<%# Eval("IdPaciente") %>' />
                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm"
                                CommandName="Eliminar" CommandArgument='<%# Eval("IdPaciente") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

