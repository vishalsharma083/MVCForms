<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MVCForms.Models.FormFieldViewModel>" %>
<%@ Import Namespace="MVCForms.Helpers"%>

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
                <%: Html.CheckBoxFor(formfield => formfield.IsRequired)%>Required?             
            </td>
            <td style="width:15%;">
                <% if (Model.ShowDelete) { %><a href="#" class="deleteItem">Delete</a><% } %> 
            </td>
        </tr>
        <tr>
            <td class="field-options">Prompt</td>
            <td colspan="3"><%: Html.TextAreaFor(formfield => formfield.FormFieldPrompt) %>  <%:Html.ValidationMessageFor(formfield => formfield.FormFieldPrompt)%></td>
        </tr>
        <tr>
            <td class="field-options">Rows</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.Rows) %>  <%:Html.ValidationMessageFor(formfield => formfield.Rows)%></td>
        </tr>
        <tr>
            <td class="field-options">Columns</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.Cols) %>  <%:Html.ValidationMessageFor(formfield => formfield.Cols)%></td>
        </tr>
    </table>
    <% } %>
</div>
