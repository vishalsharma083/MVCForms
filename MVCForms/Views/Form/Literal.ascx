<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MVCForms.Models.FormFieldViewModel>" %>
<%@ Import Namespace="MVCForms.Helpers"%>
<script type="text/javascript">
    tinyMCE.init({
        mode: "exact",
        elements: "WYSIWYG_<%: Model.Uid %>",
        width: "400",
        height: "150",
        theme: "advanced",
        theme_advanced_buttons1: "bold,italic,underline,link,unlink,bullist,blockquote,undo",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: ""
    });
</script>

<div class="FormField">
    <% using(Html.BeginCollectionItem("formfields"))
       {%>   
    <table>
        <tr>
            <th>Form Field</th>
            <th>Field Type</th>
            <th>Required</th>
            <th>Actions</th>
        </tr>
        <tr>
            <td style="width:45%;">
                <%:Html.HiddenFor(formfield => formfield.Uid)%>
                <%:Html.TextBoxFor(formfield => formfield.FormFieldName)%> <%:Html.ValidationMessageFor(formfield => formfield.FormFieldName)%>
            </td>
            <td style="width:25%;">
                      <%:Html.DropDownListFor(formfield => formfield.SelectedFormFieldType,
                                                               new SelectList(Model.FormFieldTypes, "Value", "Text",
                                                                              Model.SelectedFormFieldType),
                                                               new {disabled = "disabled"})%> 
                      <%:Html.HiddenFor(formfield => formfield.SelectedFormFieldType) %>             
                <%: Html.ValidationMessageFor(formfield => formfield.SelectedFormFieldType) %>
            </td>
            <td style="width:15%;">
                <%: Html.CheckBoxFor(formfield => formfield.IsRequired, new { disabled = "disabled" })%>Required?             
            </td>
            <td style="width:15%;">
                <% if (Model.ShowDelete) { %><a href="#" class="deleteItem">Delete</a><% } %> 
            </td>
        </tr>
        <tr>
            <td class="field-options">HTML to Display</td>
            <td colspan="3"><%: Html.TextAreaFor(formfield => formfield.LiteralText, new { id = "WYSIWYG_" + Model.Uid }) %>  <%:Html.ValidationMessageFor(formfield => formfield.LiteralText)%></td>
        </tr>
    </table>
    <% } %>
</div>
