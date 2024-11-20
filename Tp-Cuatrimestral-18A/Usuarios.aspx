<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Tp_Cuatrimestral_18A.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>
                <asp:Label ID="lblTitulo" runat="server" Text="Gestión de Usuarios"></asp:Label>
            </h2>
            <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-primary" OnClick="btnAgregarUsuario_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label> 
        </div>
    </div>

    <div class="row mt-3">
        <div class="col-md-4">
            <label for="txtBuscarDNI">Buscar por Nombre de usuario:</label>
            <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary mt-2" />
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-12">
            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                EmptyDataText="No se encontraron registros con el Usuario ingresado">
                <Columns>
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Mail" HeaderText="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModificar" runat="server" Text="Modificar" CssClass="btn btn-warning btn-sm" OnClick="lnkModificar_Click"
                                CommandName="Modificar" CommandArgument='<%# Eval("IdUsuario") %>' />
                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClick="lnkEliminar_Click"
                                OnClientClick="return confirm('¿Está seguro que desea eliminar este registro? Esta acción no se podrá deshacer.');"
                                CommandName="Eliminar" CommandArgument='<%# Eval("IdUsuario") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
