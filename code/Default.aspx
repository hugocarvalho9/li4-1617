<%@ Page Title="EatUp - Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

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

        <div id="interact" >
            <div id="text" style="height:550px; width:100%;">
                <img src="logo.png"/>

                <div id = "search">
                    <p>Etiquetas</p><asp:TextBox style="width:100%" CssClass="textB" ID="TextBox1" runat="server"></asp:TextBox>
                    <p>Com</p><asp:TextBox style="width:100%"  CssClass="textB" ID="TextBox2" runat="server"></asp:TextBox> 
                    <p>Sem</p><asp:TextBox style="width:100%"  ID="TextBox3" runat="server"></asp:TextBox>
                    <br />
                    <p>
                    KM:
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList> 
                        <asp:Button ID="searchb" runat="server" OnClick="Button1_Click" Text="Pesquisar" /> 
                    </p>
                </div>
               

            </div>
            <div id="voice">
                 <img src="mic.png"/>
            </div>
        </div>
    </div>

</asp:Content>
