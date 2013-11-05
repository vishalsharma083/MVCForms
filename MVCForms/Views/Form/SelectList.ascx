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
            <td class="field-options">Show Empty Option?</td>
            <td colspan="3"><%: Html.RadioButtonFor(formfield => formfield.IsEmptyOption, true) %>Yes <%: Html.RadioButtonFor(formfield => formfield.IsEmptyOption, false) %>No</td>
        </tr>
        <tr>
            <td class="field-options">Empty Option Text</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.EmptyOption) %></td>
        </tr>
        <tr>
            <td class="field-options">Options (One Per Row)</td>
            <td colspan="3"><%: Html.TextAreaFor(formfield => formfield.Options) %>  <%:Html.ValidationMessageFor(formfield => formfield.Options)%></td>
        </tr>
        <tr>
            <td class="field-options">Allow Multiple Choice Selection?</td>
            <td colspan="3"><%: Html.RadioButtonFor(formfield => formfield.IsMultipleSelect, true) %>Yes <%: Html.RadioButtonFor(formfield => formfield.IsMultipleSelect, false) %>No</td>
        </tr>
        <tr>
            <td class="field-options">List Size (Rows)</td>
            <td colspan="3"><%: Html.TextBoxFor(formfield => formfield.ListSize) %>  <%:Html.ValidationMessageFor(formfield => formfield.ListSize)%></td>
        </tr>
    </table>
    <% } %>
</div>
