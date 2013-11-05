<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCForms.Models.UserAllocationsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit User Allocations
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Html.EnableClientValidation(); %>

    <h2>Edit User Allocations</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Model.UserName %>
                <%: Html.HiddenFor(model => model.UserName) %>
                <%: Html.HiddenFor(model => model.UserId) %>
            </div>
                       
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AvailableSubmissions) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AvailableSubmissions) %>
                <%: Html.ValidationMessageFor(model => model.AvailableSubmissions) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

