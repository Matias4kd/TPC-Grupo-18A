<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tp_Cuatrimestral_18A.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-5 justify-content-center">
        <div class="col-md-6">
            <h2 class="text-center">Iniciar Sesión</h2>
            <div class="card p-4">
                <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
                <div class="form-group">
                    <label for="txtUsername">Usuario:</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
                </div>
                <div class="form-group mt-3">
                    <label for="txtPassword">Contraseña:</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña"></asp:TextBox>
                </div>
                <div class="form-group mt-4 text-center">
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                </div>
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="mt-3">
                    <div class="alert alert-danger">
                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
