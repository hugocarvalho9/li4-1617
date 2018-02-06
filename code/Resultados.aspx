<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Resultados.aspx.cs" Inherits="Resultados" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
    <div = "wrapper" style="height:100%">
        <div id="menu" style="float:left; width:30%; height:100%">
                <ul>
                    <li>Inicio</li>
                    <li>Histórico</li>
                    <li>Adicionar Etiquetas</li>
                    <li>Ver Histórico</li>
                    <li>Histórico de Avaliações</li>
                    <li>Autenticar</li>
                </ul>
        </div>

        <div id="interact" ClientIDMode="Static" runat="server">

        </div>
    </div>

</asp:Content>
