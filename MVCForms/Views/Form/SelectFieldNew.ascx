<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MVCForms.Models.FormFieldViewModel>" %>

 <script type="text/javascript">
     $(document).ready(function () {
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
     });
    </script>

<div id="FieldType"> 
    <table>
        <tr>
            <th colspan="4">Select a Field Type</th>
        </tr>
        <tr>
            <td colspan="4">
                <%: Html.DropDownListFor(formfield => formfield.SelectedFormFieldType, new SelectList(Model.FormFieldTypes, "Value", "Text", Model.SelectedFormFieldType), new { id = "SelectedFieldUid" })%>
                <%: Html.ValidationMessageFor(formfield => formfield.SelectedFormFieldType) %>
                <%: Html.ActionLink("Add Field", "NewFormField", new { formId = ViewContext.FormContext.FormId, selectedFieldType = "SelectedFieldUid" }, new { id = "newFormField" })%>
            </td>
        </tr>
    </table>
</div>