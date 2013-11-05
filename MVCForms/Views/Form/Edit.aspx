<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCForms.Models.FormViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit a Form
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>" type="text/javascript"></script>

    <% Html.EnableClientValidation(); %>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#newFieldSelect").click(function () {
                $("#FieldType").fadeIn('slow');
                return false;
            });
            $("#newFormField").click(function () {
                $.ajax({
                    url: this.href.replace("SelectedFieldUid", document.getElementById("SelectedFieldUid").value),
                    cache: false,
                    success: function (html) {
                        $("#FormFieldList").append(html);
                        $("#FieldType").fadeOut('slow');
                    }
                });
                return false;
            });
            $("a.deleteItem").live("click", function () {
                $(this).parents("div.FormField:first").remove();
                return false;
            });
            $("#FormFieldList").sortable({ axis: "y" });
        });
    </script>

    <h2>Edit a Form</h2>

        <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>Time to Tweak!</legend>
            <%: Html.HiddenFor(form => form.Form.Uid) %>
            <%: Html.Hidden("ListFields", ViewData["ListFields"]) %>
            <p>
                <%: Html.LabelFor(form => form.Form.FormName) %><br />
                <%: Html.TextBoxFor(form => form.Form.FormName) %>
                <%: Html.ValidationMessageFor(form => form.Form.FormName) %>
            </p>
            <div id="FormFieldList">
            <% foreach (var formfield in Model.FormFields)
            {
                   switch (formfield.ControlType)
                   {
                       case ("Generic"):
                           Html.RenderPartial("Generic", formfield);                           
                           break;
                       case ("ChoiceList"):
                           Html.RenderPartial("ChoiceList", formfield);
                           break;
                       case ("SelectList"):
                           Html.RenderPartial("SelectList", formfield);
                           break;
                       case ("TextArea"):
                           Html.RenderPartial("TextArea", formfield);
                           break;                                                      
                       case ("CheckBox"):
                           Html.RenderPartial("CheckBox", formfield);
                           break;
                       case ("Literal"):
                           Html.RenderPartial("Literal", formfield);
                           break;
                       case ("FileUpload"):
                           Html.RenderPartial("FileUpload", formfield);
                           break;                             
                   }
            }%>
            </div>
            <h4>
                <%--<%: Html.ActionLink("[+] Add Another Form Field", "NewFormField", new { ViewContext.FormContext.FormId }, new { id = "newFormField" }) %>--%>
                <a href="javascript:;" id="newFieldSelect">[+] Add a Field</a>
            </h4>
            <div id="FieldType" style="display:none;"> 
                <table>
                    <tr>
                        <th>Select a Field Type</th>
                    </tr>
                    <tr>
                        <td>
                            <%: Html.DropDownList("FieldTypes", new SelectList(Model.FormFields[0].FormFieldTypes, "Value", "Text"), new { id = "SelectedFieldUid" })%>
                            <%: Html.ActionLink("Add Field", "NewFormField", new { formId = ViewContext.FormContext.FormId, selectedFieldType = "SelectedFieldUid" }, new { id = "newFormField" })%>
                        </td>
                    </tr>
                </table>
            </div>
            <p>
                <input type="submit" value="Save" />
                <input type="button" value="Cancel" onclick="confirmation('Are you sure you wish to cancel, and abandon all changes?', '<%: Url.Action("List") %>');" />
            </p>
        </fieldset>     
    <% } %>

</asp:Content>