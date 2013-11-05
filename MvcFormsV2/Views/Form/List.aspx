<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MVCForms.Models.Form>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Your Forms
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Forms</h2>

            <h3>
        <%: ViewData["Message"] %></h3>

    <h3><%:Html.ActionLink("Create a New Form", "Create")%></h3>

    <% if (Model.Count() > 0)
       {%>
    <table>
        <tr>
            <th>Title</th>
            <th>Actions</th>
        </tr>

    <%
           foreach (var form in Model)
           {%>
    
        <tr>
            <td>
                <%:form.FormName%>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id = form.Uid }) %>
                |
                <a href="<%: "javascript:copyToClipboard('<iframe height=\"320\" allowTransparency=\"true\" frameborder=\"0\" scrolling=\"no\" style=\"width:100%;border:none\" src=\"http://" + ViewData["Domain"] + "/" + form.ShortPath + "\"><a href=\"http://" + ViewData["Domain"] + "/" + form.ShortPath + "\" title=\"" + form.FormName +  "\" rel=\"nofollow\">Click here to fill-out my form: \"" + form.FormName + "\"</a></iframe>');alert('Form tag copied! Now, paste it into a webpage.');" %>">Embed</a>
                |
                <a href="/<%:form.ShortPath %>" target="_blank">View Form</a>
            </td>
        </tr>
    
    <%
           }%>

    </table>
    <%
       }
       else
       {
           %>
           <p>You don't have any forms. Create one!</p>
           <%
       }%>

</asp:Content>

