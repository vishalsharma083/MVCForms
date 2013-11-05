<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MVCForms Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        This front-end lets you build your own forms using an MVC interface, and then they are rendered using the MVC Dynamic Forms project. This system 
        keeps a database copy of the form you've built and allows you to collect responses with your form and export them. There is an embeddable tag option
        which lets you host your form on any website.
    </p>
</asp:Content>
