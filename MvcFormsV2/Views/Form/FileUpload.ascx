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
            <td class="field-options">Maximum Size (in Bytes, 1KB = 1000 Bytes)</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.MaxSizeBytes) %>  <%:Html.ValidationMessageFor(formfield => formfield.MaxSizeBytes)%></td>
        </tr>
        <tr>
            <td class="field-options">Valid File Extensions (1 per Line, ex: jpeg)</td>
            <td colspan="3"><%: Html.TextAreaFor(formfield => formfield.ValidExtensions) %>  <%:Html.ValidationMessageFor(formfield => formfield.ValidExtensions)%></td>
        </tr>
        <tr>
            <td class="field-options">Error for Invalid Extension</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.ErrorExtensions) %>  <%:Html.ValidationMessageFor(formfield => formfield.ErrorExtensions)%></td>
        </tr>
    </table>
    <% } %>
</div>
