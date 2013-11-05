<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MVCForms.Models.UserAllocationsViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	User Management
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>User Management</h2>

    <table>
        <tr>
            <th>User List</th>
        </tr>

    <% foreach (var userAllocation in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink(userAllocation.UserName, "Edit", new { id = userAllocation.UserId} )%> (<%: userAllocation.AvailableSubmissions %> remaining form submissions, last submission <%: userAllocation.LastSubmission %>)
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

