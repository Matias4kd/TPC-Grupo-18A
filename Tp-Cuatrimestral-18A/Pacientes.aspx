﻿<%@ Page Title="Gestión de Pacientes" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="ClinicaMedica.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="row mt-3">
        <div class="col-md-4">
                    <div class="row mt-3">
            <div class="col-12">
                <h2>Gestión de Pacientes</h2>
                <asp:Button ID="btnAgregarPaciente" runat="server" Text="Agregar Paciente" CssClass="btn btn-primary" OnClick="btnAgregarPaciente_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label> 
            </div>
        </div>
            <label for="txtBuscarDNI">Buscar por DNI:</label>
            <asp:TextBox ID="txtBuscarDNI" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="lblBuscar" runat="server" CssClass="d-block mt-3" Visible="false"></asp:Label>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary mt-2" OnClick="btnBuscar_Click" />
                 <asp:Button ID="Limpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary mt-2" OnClick="btnLimpiar_Click" />
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
                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClick="btnEliminar_Click"
                                OnClientClick="return confirm('¿Está seguro que desea eliminar este registro? Esta acción no se podrá deshacer.');"
                                CommandName="Eliminar" CommandArgument='<%# Eval("IdPaciente") %>' />
                            <asp:LinkButton ID="linkSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary" OnClick ="linkSeleccionar_Click"
                            CommandName="Seleccionar" CommandArgument='<%# Eval("IdPaciente") %>' />
                            <asp:LinkButton ID="lnkAgenda" runat="server" CssClass="btn btn-secondary btn-sm" CommandName="Agenda" CommandArgument='<%# Eval("IdPaciente") %>' OnCommand="lnkAgenda_Command">Agenda</asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="pnlMensajes" runat="server" Visible="false" CssClass="mt-3">
        <div class="alert alert-info">
            <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
        </div>
    </asp:Panel>
</asp:Content>
