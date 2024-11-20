<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ABMUsuarios.aspx.cs" Inherits="Tp_Cuatrimestral_18A.ABMUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
    <div class="col-12">
        <h2><asp:Label ID="lblTitulo" runat="server" Text="Alta de Usuarios"></asp:Label></h2>
    </div>
</div>

<div class="row mt-4">
    <div class="col-md-6">
        <div class="container" id="contenedorInfoUsuario" runat="server">
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
            <div class="container" id="contenedorPasswords" runat="server" >
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" AssociatedControlID="txtPassword" />
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="La contraseña es obligatorio." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>

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
            
        </div>
        <div class="container" id="contenedorInfoMedico" runat="server" >
            <div class="row mt-3">
               <div class="col-12">
                    <h5>Informacion Medico: </h5>
                </div>
            </div>
            <asp:Label ID="lblMatricula" runat="server" Text="Matricula:" AssociatedControlID="txtMatricula" />
            <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" SupportsDisabledAttribute="True" />
            <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula" ErrorMessage="La matricula es obligatoria." CssClass="text-danger" Display="Dynamic" />
            <br />

            <div class="form-group"> 
                <asp:Label ID="lblEspecialidades" runat="server" Text="Seleccione especialidad/es:" CssClass="form-label"></asp:Label> 
                <div class="row"> 
                    <asp:CheckBoxList ID="cblEspecialidades" runat="server" CssClass="form-check-list"></asp:CheckBoxList>
                    <asp:Label ID="lblErrorEspecialidades" runat="server" Text="Seleccione al menos una especialidad." Visible="false" CssClass="text-danger"></asp:Label> 

                </div>
            </div>
            <br />
            
            <div class="form-group"> 
                <asp:Label ID="lblPrepagas" runat="server" Text="Seleccione la/las prepaga/as con las que trabaja:" CssClass="form-label"></asp:Label> 
                <div class="row"> 
                    <asp:CheckBoxList ID="cblPrepagas" runat="server" CssClass="form-check-list"></asp:CheckBoxList>
                    <asp:Label ID="lblErrorPrepagas" runat="server" Text="Seleccione al menos una prepaga." Visible="false" CssClass="text-danger"></asp:Label> 

                </div>
            </div>
            <br />

            <div class="container" id="contenedorHorarios" runat="server" >
                <div class="row">
                    <asp:Label ID="lblHorarios" runat="server" Text="Defina el horario de trabajo:" CssClass="form-label"></asp:Label>
                </div>
                <div class="row">
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label id="lblLunes" for="ddlInicioLunes" class="form-label">Lunes: </label>
                                <br />
                                <asp:Label ID="lblInicioLunes" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioLunes" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFinLunes" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinLunes" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioMartes" class="form-label">Martes: </label>
                                <br />
                                <asp:Label ID="lblInicioMartes" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioMartes" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFinMartes" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinMartes" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioMiercoles" class="form-label">Miercoles: </label>
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioMiercoles" runat="server" CssClass="form-select" />
                                <asp:Label ID="Label2" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinMiercoles" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioJueves" class="form-label">Jueves: </label>
                                <br />
                                <asp:Label ID="lblInicioJueves" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioJueves" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFinJueves" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinJueves" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioViernes" class="form-label">Viernes: </label>
                                <br />
                                <asp:Label ID="lblInicioViernes" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioViernes" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFInViernes" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinViernes" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioSabado" class="form-label">Sabado: </label>
                                <br />
                                <asp:Label ID="lblInicioSabado" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioSabado" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFinSabado" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinSabado" runat="server" CssClass="form-select mt-2" />
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="ddlInicioDomingo" class="form-label">Domingo: </label>
                                <br />
                                <asp:Label ID="lblInicioDomingo" runat="server" Text="Inicio: "></asp:Label>
                                <asp:DropDownList ID="ddlInicioDomingo" runat="server" CssClass="form-select" />
                                <asp:Label ID="lblFinDomingo" runat="server" Text="Fin: "></asp:Label>
                                <asp:DropDownList ID="ddlFinDomingo" runat="server" CssClass="form-select mt-2" /> 
                            </div>
                        </div>                    
                </div>
            </div>
        </div>


        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary mt-3" OnClick="btnGuardar_Click" />

        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3" OnClick="btnVolver_Click" CausesValidation="false" />

    </div>

</div>

</asp:Content>
