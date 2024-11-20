<%@ Page Title="Especialidades Médicas" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="EspecialidadMedica.aspx.cs" Inherits="ClinicaMedica.EspecialidadMedica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Especialidades Médicas</h1>
        <asp:GridView ID="gvEspecialidades" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="IdEspecialidad" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnReactivar" runat="server" CommandName="Reactivar" OnClick="btnReactivar_Click" CommandArgument='<%# Eval("IdEspecialidad") %>' Text="Reactivar" CssClass="btn btn-success btn-sm" Enabled='<%# Eval("Estado").ToString() == "Inactivo" %>' />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdEspecialidad") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h2>Agregar Nueva Especialidad</h2>
        <asp:TextBox ID="txtNombreEspecialidad" runat="server" CssClass="form-control" Placeholder="Nombre de la Especialidad"></asp:TextBox>
        <br />
        <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
        <br />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-2" OnClick="btnVolver_Click" />
        <br />
    </div>
    <!-- Footer -->
<footer class="bg-dark text-white text-center py-3 mt-5">
    <div class="container">
        <p class="mb-0">Clínica Médica ZN - Todos los derechos reservados © 2024</p>
        <p class="mb-0">Dirección: Calle Falsa 123, Ciudad Autónoma de Buenos Aires</p>
        <p class="mb-0">Teléfono: +54 11 1234-5678 | Email: contacto@clinicaZN.com</p>
    </div>
</footer>
</asp:Content>
