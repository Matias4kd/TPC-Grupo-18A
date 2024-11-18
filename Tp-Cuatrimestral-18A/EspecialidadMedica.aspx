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
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h2>Agregar Nueva Especialidad</h2>
        <asp:TextBox ID="txtNombreEspecialidad" runat="server" CssClass="form-control" Placeholder="Nombre de la Especialidad"></asp:TextBox>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
    </div>
</asp:Content>
