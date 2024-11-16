<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ABMUsuarios.aspx.cs" Inherits="Tp_Cuatrimestral_18A.ABMUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
    <div class="col-12">
        <h2>Alta de Usuarios</h2>
    </div>
</div>

<div class="row mt-4">
    <div class="col-md-6">
        <asp:Label ID="lblRol" runat="server" Text="Seleccionar Rol:" AssociatedControlID="ddlRol" />
        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
            <asp:ListItem Text="Seleccionar" Value="0" />           
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" ErrorMessage="El rol es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre de Usuario:" AssociatedControlID="txtNombre" />
        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvNombreUsuario" runat="server" ControlToValidate="txtNombreUsuario" ErrorMessage="El nombre de usuario es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" AssociatedControlID="txtPassword" />
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="La contraseña es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />


        <asp:Label ID="lblConfirmacionPassword" runat="server" Text="Confirmar Contraseña:" AssociatedControlID="txtConfirmacionPassword" />
        <asp:TextBox ID="txtConfirmacionPassword" TextMode="Password" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmacionPassword" ErrorMessage="La contraseña es obligatorio.." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" AssociatedControlID="txtNombre" />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />
        
        <asp:Label ID="lblApellido" runat="server" Text="Apellido:" AssociatedControlID="txtApellido" />
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El Apellido es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Type="email" />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El mail es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" AssociatedControlID="txtTelefono" />
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="El telefono es obligatorio." CssClass="text-danger" Display="Dynamic" />
        <br />

        <asp:Label ID="lblMatricula" runat="server" Text="Matricula:" AssociatedControlID="txtMatricula" />
        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" SupportsDisabledAttribute="True" />
        <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula" ErrorMessage="La matricula es obligatoria." CssClass="text-danger" Display="Dynamic" />
        <br />

  
        <div class="form-group">
            <asp:Label ID="lblEspecialidades" runat="server" Text="Seleccione especialidad/es:" CssClass="form-label"></asp:Label>
            <div class="row">
                <asp:Repeater ID="rptEspecialidades" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-md-4 col-lg-3 mb-3">
                            <div class="form-check">
                                <input type="checkbox" class="form-check-input" id="chkEspecialidad<%# Container.ItemIndex %>" value='<%# Eval("IdEspecialidad") %>' />
                                <label class="form-check-label" for="chkEspecialidad<%# Container.ItemIndex %>"><%# Eval("Nombre") %></label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>
        <br />
        
        <div class="form-group">
            <asp:Label ID="lblPrepagas" runat="server" Text="Seleccione la/las prepaga/as con las que trabaja:" CssClass="form-label"></asp:Label>
            <div class="row">
                <asp:Repeater ID="rptPrepagas" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-md-4 col-lg-3 mb-3">
                            <div class="form-check" >
                                <input type="checkbox" class="form-check-input" id="chkPrepaga<%# Container.ItemIndex %>" value='<%# Eval("IdPrepaga") %>' />
                                <label class="form-check-label" for="chkPrepaga<%# Container.ItemIndex %>"><%# Eval("Nombre") %></label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />



        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary mt-3" OnClick="btnGuardar_Click" />

        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3" OnClick="btnVolver_Click" CausesValidation="false" />

    </div>
</div>
</asp:Content>
