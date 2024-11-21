<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AgendaMedico.aspx.cs" Inherits="Tp_Cuatrimestral_18A.AgendaMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>
                <asp:Label ID="lblAgenda" runat="server" CssClass="welcome-label"></asp:Label>
            </h2>
        </div>

    </div>

    <div class="row mt-4">
        <div class="col-md-6">

            <strong>
                <asp:Label ID="lblFechaTurno" runat="server" Text="Fecha del turno:" AssociatedControlID="txtFechaTurno" /></strong>
            <asp:TextBox ID="txtFechaTurno" runat="server" CssClass="form-control" TextMode="Date" OnTextChanged="txtFechaTurno_TextChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvFechaTurno" runat="server" ControlToValidate="txtFechaTurno" ErrorMessage="La fecha de nacimiento es obligatoria." CssClass="text-danger" Display="Dynamic" />
            <br />

            <div class="row mt-4">
                <div class="col-12">
                    <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                        EmptyDataText="No se encontraron turnos en el día de la fecha">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkVer" runat="server" Text="Ver Turno" CssClass="btn btn-warning btn-sm"
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

