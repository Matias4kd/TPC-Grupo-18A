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
                                <strong>Especialidad:</strong>
                                <asp:Label ID="lblEspecialidadesMedico" runat="server" CssClass="form-control-plaintext" />
                            </p>
                        </div>

                    </div>
                    <asp:Label runat="server"><strong>Seleccione el día del turno: </strong></asp:Label>
                    <asp:Calendar ID="calendarioTurnos" runat="server" OnDayRender="calendarioTurnos_DayRender" OnSelectionChanged="calendarioTurnos_SelectionChanged" Style="margin-left: 130px; margin-top: 20px; margin-bottom: 20px" />
                    <asp:Label runat="server" Style="margin-top: 20px" Visible="false" ID="lblSeleccioneHorario"><strong>Seleccione un horario: </strong></asp:Label>
                    <asp:DropDownList ID="ddlTurnosDisponibles" runat="server" class="form-select" Visible="false"></asp:DropDownList>
                    <br />
                    <asp:Label runat="server" Style="margin-top: 20px" Visible="true" ID="lblObservaciones"><strong>Observaciones: </strong></asp:Label>
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine"/>
                    <asp:RequiredFieldValidator ID="rfvTxtObservaciones" runat="server" ControlToValidate="txtObservaciones" ErrorMessage="Por favor, ingrese una observación para el turno"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button ID="btnAgendarTurno" runat="server" Text="Agendar Turno" class="btn btn-primary btn-sm" Style="margin-top: 20px" OnClick="btnAgendarTurno_Click" Visible="false" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTurnoExitoso" runat="server" Visible="false" style="margin-top: 40px">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 class="card-title mb-0">Asignación de Turno</h5>
            </div>
            <div class="card-body">
                <h1 style="margin-bottom: 20px">
                    <strong>¡El turno se agendo exitosamente!</strong>
                </h1>
                <p>
                   Haga click aquí para regresar al menú principal
                </p>
                <asp:Button ID="btnRegresar" Text="Regresar" runat="server" OnClick="btnRegresar_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>


