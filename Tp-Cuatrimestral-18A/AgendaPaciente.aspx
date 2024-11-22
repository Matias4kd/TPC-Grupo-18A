<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AgendaPaciente.aspx.cs" Inherits="Tp_Cuatrimestral_18A.AgendaPaciente1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>
                <asp:Label ID="lblAgendaPaciente" runat="server" CssClass="welcome-label"></asp:Label>
            </h2>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-md-6">
            <div class="row mt-4">
                <div class="col-12">
                    <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                        EmptyDataText="No se encontraron turnos agendados">
                        <Columns>
                            <asp:BoundField DataField="IdTurno" HeaderText="ID" Visible="False" />    
                            <asp:BoundField DataField="NombreMedico" HeaderText="Nombre del Médico" />
                            <asp:BoundField DataField="ApellidoMedico" HeaderText="Apellido del Médico" />
                            <asp:BoundField DataField="HorarioTurno" HeaderText="Horario del Turno" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado de consulta" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkVer" runat="server" Text="Ver Turno" OnClick="lnkVer_Click" CssClass="btn btn-warning btn-sm"
                                        CommandName="Ver" CommandArgument='<%# Eval("IdTurno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
    
        </div>
    </div>
</asp:Content>
