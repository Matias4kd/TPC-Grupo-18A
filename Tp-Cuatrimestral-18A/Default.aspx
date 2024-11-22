<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tp_Cuatrimestral_18A.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-background"></div>
    <div class="login-overlay"></div>
    
    <div class="row min-vh-100 justify-content-center align-items-center mx-0">
        <div class="col-11 col-md-8 col-lg-6 col-xl-5">
            <div class="login-card p-4 p-md-5">
                <h2 class="login-title">Bienvenido a Clínica Médica ZN</h2>
                
                <asp:Label ID="lblError" runat="server" CssClass="login-error" Visible="false"></asp:Label>
                
                <div class="login-form-group">
                    <label for="txtUsername" class="form-label">Usuario</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control login-input" 
                        placeholder="Ingrese su usuario"></asp:TextBox>
                </div>
                
                <div class="login-form-group">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                        CssClass="form-control login-input" placeholder="Ingrese su contraseña"></asp:TextBox>
                </div>
                
                <div class="text-center mt-4">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" 
                        CssClass="login-btn" OnClick="btnIngresar_Click" />
                </div>
                
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="login-error mt-3">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        // Agregar clase al body para ocultar navbar y footer
        document.body.classList.add('login-page');

        if (performance.navigation.type == 2) {
            window.location.href = "Default.aspx";
        }

        window.history.pushState(null, null, window.location.href);
        window.onpopstate = function () {
            window.history.go(1);
        };
    </script>
</asp:Content>