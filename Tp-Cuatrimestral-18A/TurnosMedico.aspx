<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TurnosMedico.aspx.cs" Inherits="Tp_Cuatrimestral_18A.TurnosMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlFormularioMedico" runat="server">
        <div class="container mt-5" id="CardContainer">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header bg-primary text-white">
                            <h5 class="card-title mb-0">Información del Médico</h5>
                        </div>
                        <div class="card-body">
                            <p>
                                <strong>Nombre:</strong>
                                <asp:Label ID="lblNombreMedico" runat="server" CssClass="form-control-plaintext" />
                            </p>
                            <p>
                                <strong>Apellido:</strong>
                                <asp:Label ID="lblApellidoMedico" runat="server" CssClass="form-control-plaintext" />
                            </p>
                            <p>
                                <strong>Matricula:</strong>
                                <asp:Label ID="lblMatriculaMedico" runat="server" CssClass="form-control-plaintext" />
                            </p>
                        </div>
                    </div>
                    <asp:label runat="server"><strong>Seleccione el día del turno: </strong></asp:label>
                    <asp:Calendar ID="calendarioTurnos" runat="server" OnSelectionChanged="calendarioTurnos_SelectionChanged" style="margin-left: 200px; margin-top: 20px; margin-bottom: 20px" />
                    <asp:label runat="server" style="margin-top: 20px" Visible="false" ID="lblSeleccioneHorario"><strong>Seleccione un horario: </strong></asp:label>
                    <asp:DropDownList ID="ddlTurnosDisponibles" runat="server" class="form-select" visible="false"></asp:DropDownList>
                    <asp:Button ID="btnAceptarHorario" runat="server" Text="Aceptar" class="btn btn-primary btn-sm" Style="margin-top: 20px" OnClick="btnAceptarHorario_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDni" runat="server" Visible="false">
        <div class="row-6">
            <div class="col-6">
                <asp:Label runat="server" ID="lblIngresarDni" class="form-label" style="margin-top: 100px; margin-left: 100px">Ingrese el DNI del paciente: </asp:Label>
                <asp:TextBox runat="server" ID="txtDni" Style="margin-left: 20px; margin-top: 100px; margin-left: 20px; margin-right: 20px; margin-bottom: 40px" />
                <asp:Button Text="Aceptar" ID="txtAceptar" runat="server" OnClick="btnAceptar_Click" />
            </div>
       </div>
    </asp:Panel>

    <asp:Panel ID="pnlFormularioPaciente" runat="server" Visible="false" CssClass="container border rounded p-4 shadow-sm">
        <h4 class="text-center mb-4">Formulario de Paciente</h4>

        <div class="row mb-3">
            <label for="txtDniFormulario" class="col-sm-4 col-form-label">DNI:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtDniFormulario" CssClass="form-control" Enabled="false" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtNombre" class="col-sm-4 col-form-label">Nombre:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtApellido" class="col-sm-4 col-form-label">Apellido:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtEmail" class="col-sm-4 col-form-label">Email:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtTelefono" class="col-sm-4 col-form-label">Teléfono:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtDireccion" class="col-sm-4 col-form-label">Dirección:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtPrepaga" class="col-sm-4 col-form-label">Prepaga:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtPrepaga" CssClass="form-control" />
            </div>
        </div>

        <div class="row mb-3">
            <label for="txtFechaNacimiento" class="col-sm-4 col-form-label">Fecha de Nacimiento:</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="form-control" />
            </div>
        </div>

        <div class="text-center">
            <asp:Button Text="Agendar Turno" runat="server" OnClick="btnAgendar_Click" CssClass="btn btn-primary w-100" />
        </div>
    </asp:Panel>
</asp:Content>


