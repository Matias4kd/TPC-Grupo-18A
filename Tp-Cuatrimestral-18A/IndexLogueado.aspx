<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="IndexLogueado.aspx.cs" Inherits="Tp_Cuatrimestral_18A.IndexLogueado" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Contenido adicional en el head si lo necesitas -->
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center" style="height: 80vh;">
        <a href="Pacientes.aspx" class="btn btn-primary mx-2">Pacientes</a>
        <a href="Medicos.aspx" class="btn btn-primary mx-2">Turnos Médicos</a>
    </div>
</asp:Content>
