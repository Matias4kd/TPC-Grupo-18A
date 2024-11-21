<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="DetalleTurno.aspx.cs" Inherits="Tp_Cuatrimestral_18A.DetalleTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row mt-5 justify-content-center">
        <div class="col-md-6">
            <h2 class="text-center"><asp:Label ID="lblTitulo" runat="server" Text="Detalle: "></asp:Label></h2>
            <div class="card p-4">                    
                <div class="form-group">
                    <asp:Label ID="lblNombrePaciente" runat="server" Text="Nombre:" AssociatedControlID="txtNombrePaciente" />
                    <asp:TextBox ID="txtNombrePaciente" runat="server" CssClass="form-control"/>
                </div>
                <div class="form-group mt-3">
                    <asp:Label ID="lblApellidoPaciente" runat="server" Text="Apellido:" AssociatedControlID="txtApellidoPaciente" />
                    <asp:TextBox ID="txtApellidoPaciente" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group mt-3">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha:" AssociatedControlID="txtFecha" />
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control"/>
                </div>
                <div class="form-group mt-3">
                    <asp:Label ID="lblHorario" runat="server" Text="Horario:" AssociatedControlID="txtHorario" />
                    <asp:TextBox ID="txtHorario" runat="server" CssClass="form-control"/>
                </div>                
                <div class="form-group mt-3">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:" AssociatedControlID="ddlEstado" />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado" ErrorMessage="El estado es obligatorio." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="form-group mt-3">
                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones:" AssociatedControlID="txtObservaciones" />
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine"/>
                    <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="txtObservaciones" ErrorMessage="La observacion es obligatoria." CssClass="text-danger" Display="Dynamic" />

                </div>  
                <div class="form-group mt-4 text-center">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click"/>
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click"/>
                </div>
            </div>
        </div>
    </div>
    
    
    
    
    <br />

</asp:Content>
