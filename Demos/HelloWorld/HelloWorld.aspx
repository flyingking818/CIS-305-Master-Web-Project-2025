<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="CIS_305_Master_Web_Project.Demos.HelloWorld.HelloWorld" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Hello world</h1>
   <br />
    <asp:Button ID="Submit" runat="server" Text="Hello World!" OnClick="Submit_Click" />
          
</asp:Content>
