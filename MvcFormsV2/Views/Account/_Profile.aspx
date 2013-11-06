<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCForms.Models.UserProfileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit My Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit My Profile</h2>
         <% using (Html.BeginForm()) { %>
              <div>
            <fieldset>
            <legend>It's all about you...</legend>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.FirstName)%>
                </div>
                <div class="editor-field">            
                    <%: Html.TextBoxFor(m => m.FirstName ) %>
                    <%: Html.ValidationMessageFor(m => m.FirstName ) %>
                </div>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.LastName)%>
                </div>
                <div class="editor-field">
             
                    <%: Html.TextBoxFor(m => m.LastName ) %>
                    <%: Html.ValidationMessageFor(m => m.LastName ) %>
                </div>

                <p>
                    <input type="submit" value="Update Profile" />
                </p>
            </fieldset>
        </div>
    <% } %>

</asp:Content>
