<%@ Page Title="Gestión de Médicos" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Medicos.aspx.cs" Inherits="ClinicaMedica.Medicos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-3">
        <div class="col-12">
            <h2>Búsqueda de Médicos</h2>
        </div>
    </div>

    <div class="row mt-3">
        <div class="col-md-4">
            <div class="form-group">
                <label for="ddlPrepagas">Prepaga:</label>
                <asp:DropDownList ID="ddlPrepagas" runat="server" CssClass="form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPrepagas_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label for="ddlEspecialidades">Especialidad:</label>
                <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lblEspecialidadError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-12">
            <asp:GridView ID="gvMedicos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                EmptyDataText="No se encontraron médicos con los criterios seleccionados">
                <Columns>
                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                    <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSeleccionar" runat="server" CssClass="btn btn-primary btn-sm"
                                CommandName="Seleccionar" CommandArgument='<%# Eval("IdMedico") %>'
                                OnCommand="lnkSeleccionar_Command">
                                Turnos
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkAgenda" runat="server" CssClass="btn btn-secondary btn-sm"
                                CommandName="Agenda" CommandArgument='<%# Eval("IdMedico") %>'
                                OnCommand="lnkAgenda_Command">
                                Agenda
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Panel ID="pnlMensajes" runat="server" Visible="false" CssClass="mt-3">
        <div class="alert alert-info">
            <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
        </div>
    </asp:Panel>
</asp:Content>
