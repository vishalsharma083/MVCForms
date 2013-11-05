using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using MVCForms.Models;
using Microsoft.Security.Application;
using MVCForms.Helpers;
using MvcDynamicForms.Fields;


namespace MVCForms.Controllers
{
    public class FormController : Controller
    {
        readonly MVCFormsFormEntities _mvcForms = new MVCFormsFormEntities();
        readonly MVCFormsResponseEntities _mvcResponses = new MVCFormsResponseEntities();

        //[Authorize]
        [HttpGet]
        public ActionResult List()
        {
            var user = Membership.GetUser(User.Identity.Name);
            var viewModel = _mvcForms.Forms.Where(form => form.UserId == (Guid)user.ProviderUserKey).ToList();
            switch (Request["Message"])
            {
                case "created":
                    ViewData["Message"] = "Your new form was created!";
                    break;
                case "updated":
                    ViewData["Message"] = "Your form was updated successfully!";
                    break;
                case "loggedin":
                    ViewData["Message"] = "Welcome to MVC Forms, you are logged in!";
                    break;
                case "registered":
                    ViewData["Message"] = "You are successfully registered with MVC Forms!";
                    break;
            }
            var domain = (DomainInfoSection)System.Configuration.ConfigurationManager.GetSection("domainInfoGroup/domainInfo");
            ViewData["Domain"] = domain.Domain.Name;
            return View(viewModel);
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new FormViewModel();
            var newField = new FormFieldViewModel {FormFieldTypes = GetFormFieldTypes()};
            viewModel.FormFields.Add(newField);
            return View(viewModel);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Create(FormViewModel viewModel)
        {
            //TODO: With the complexity of what we're sending back in the viewModel, the ModelState.IsValid breaks down ... need to re-evaluate
            //if (ModelState.IsValid)
            //{
                var user = Membership.GetUser(User.Identity.Name);
                var newForm = _mvcForms.Forms.CreateObject();
                newForm.Uid = Guid.NewGuid();
                newForm.UserId = (Guid)user.ProviderUserKey;
                newForm.ShortPath = RandomString(5);
                newForm.FormName = viewModel.Form.FormName;
                newForm.Timestamp = DateTime.Now;
                _mvcForms.AddToForms(newForm);
                var sortOrder = 1;
                foreach (var formField in viewModel.FormFields)
                {
                    var thisField = formField;
                    var thisFormFieldUid = new Guid(formField.SelectedFormFieldType);
                    var newFormField = _mvcForms.FormFields.CreateObject();
                    newFormField.FormUid = newForm.Uid;
                    newFormField.Uid = Guid.NewGuid();
                    newFormField.FormFieldTypeUid = thisFormFieldUid;
                    newFormField.FormFieldName = AntiXss.GetSafeHtmlFragment(thisField.FormFieldName.PreserveBreaks()).KillHtml().RestoreBreaks();
                    newFormField.FormFieldPrompt = AntiXss.GetSafeHtmlFragment(thisField.FormFieldPrompt.PreserveBreaks()).KillHtml().RestoreBreaks();
                    newFormField.IsHidden = 0;
                    newFormField.IsRequired = Convert.ToByte(thisField.IsRequired);
                    newFormField.SortOrder = sortOrder++;
                    newFormField.Timestamp = DateTime.Now;
                    //TODO: Not sure if this is per field type, but it shouldn't matter if validation works and nulls don't matter
                    newFormField.Options = AntiXss.GetSafeHtmlFragment(thisField.Options.PreserveBreaks()).KillHtml().RestoreBreaks();
                    newFormField.Orientation = thisField.Orientation;
                    newFormField.IsMultipleSelect = Convert.ToByte(thisField.IsMultipleSelect);
                    newFormField.ListSize = thisField.ListSize;
                    newFormField.IsEmptyOption = Convert.ToByte(thisField.IsEmptyOption);
                    newFormField.EmptyOption = thisField.EmptyOption;
                    newFormField.Rows = thisField.Rows;
                    newFormField.Cols = thisField.Cols;
                    newFormField.ValidExtensions = AntiXss.GetSafeHtmlFragment(thisField.ValidExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                    newFormField.ErrorExtensions = AntiXss.GetSafeHtmlFragment(thisField.ErrorExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                    newFormField.MaxSizeBytes = thisField.MaxSizeBytes;
                    newFormField.LiteralText = AntiXss.GetSafeHtml(thisField.LiteralText);
                    _mvcForms.AddToFormFields(newFormField);
                }
                _mvcForms.SaveChanges();
                return RedirectToAction("List", new { Message = "created" });
            //}
            //Rebuild the select lists then return on invalid model state
            foreach (var formField in viewModel.FormFields)
            {
                formField.FormFieldTypes = GetFormFieldTypes();
            }
            return View(viewModel);
        }

        /// <summary>
        /// Generates a random alpha string of the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <returns>Random string</returns>
        public string RandomString(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString().ToLowerInvariant();
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var viewModel = new FormViewModel { Form = _mvcForms.Forms.Single(form => form.Uid == id) };
            var formFields = _mvcForms.FormFields.Where(items => items.FormUid == id && items.IsHidden == 0).OrderBy(items => items.SortOrder);
            if (formFields.Count() > 0)
            {
                var countFields = 1;
                var listFields = String.Empty;
                foreach (var formField in formFields)
                {
                    var thisField = formField;
                    var thisFieldType =
                        _mvcForms.FormFieldTypes.Single(fieldtype => fieldtype.Uid == thisField.FormFieldTypeUid);
                    var editFormField = new FormFieldViewModel
                    {
                        Uid = formField.Uid,
                        FormFieldName = formField.FormFieldName,
                        FormFieldPrompt = formField.FormFieldPrompt,
                        ControlType = thisFieldType.ControlType,
                        SelectedFormFieldType = thisFieldType.Uid.ToString(),
                        IsRequired = Convert.ToBoolean(formField.IsRequired),
                        ShowDelete = (countFields > 1) ? true : false,
                        FormFieldTypes = GetFormFieldTypes(),
                        //TODO: Not sure if this is per field type, but it shouldn't matter 
                        Options = formField.Options,
                        Orientation = formField.Orientation,
                        IsMultipleSelect = Convert.ToBoolean(formField.IsMultipleSelect),
                        ListSize = formField.ListSize,
                        IsEmptyOption = Convert.ToBoolean(formField.IsEmptyOption),
                        EmptyOption = formField.EmptyOption,
                        Rows = formField.Rows,
                        Cols = formField.Cols,
                        ValidExtensions = formField.ValidExtensions,
                        ErrorExtensions = formField.ErrorExtensions,
                        MaxSizeBytes = formField.MaxSizeBytes,
                        LiteralText = formField.LiteralText
                    };                  
                    viewModel.FormFields.Add(editFormField);
                    listFields += "," + thisField.Uid;
                    countFields++;
                }
                ViewData["ListFields"] = listFields.Substring(1); //Starts at 0; remove the first ','
            }
            else
            {
                var createFormField = new FormFieldViewModel
                {
                    Uid = Guid.NewGuid(),
                    ShowDelete = false,
                    FormFieldTypes = GetFormFieldTypes()
                };
                viewModel.FormFields.Add(createFormField);
            }
            return View(viewModel);
        }

        //[Authorize]
        [HttpPost]
        [ValidateInput(false)] //This allows HTML posts to be accepted; we use AntiXss to avoid issues
        public ActionResult Edit(FormViewModel viewModel)
        {
            //TODO: With the complexity of what we're sending back in the viewModel, the ModelState.IsValid breaks down ... need to re-evaluate
            //if (ModelState.IsValid)
            //{
                var thisFormUid = new Guid(Request["Form.Uid"]);
                var editForm = _mvcForms.Forms.Single(form => form.Uid == thisFormUid);
                ViewData["FormName"] = editForm.FormName;
                editForm.FormName = viewModel.Form.FormName;
                if (!string.IsNullOrEmpty(Request["ListFields"]))
                {
                    var listFields = Request["ListFields"].Split(',');
                    foreach (var listField in listFields)
                    {
                        var listFieldUid = new Guid(listField);
                        if (viewModel.FormFields.Where(fields => fields.Uid == listFieldUid).Count() == 0)
                            _mvcForms.FormFields.Single(field => field.Uid == listFieldUid).IsHidden = 1;
                    }                    
                }
                var sortOrder = 1;
                foreach (var formField in viewModel.FormFields)
                {
                    var thisField = formField;
                    //Determine if this is an existing form item or not
                    var oldFormField = _mvcForms.FormFields.FirstOrDefault(item => item.Uid == thisField.Uid);
                    if (oldFormField != null)
                    {
                        //For fields where AntiXss is helpful but we still need to preserve linebreaks and not include HTML/BODY tags, use this hack
                        oldFormField.FormFieldName = AntiXss.GetSafeHtmlFragment(thisField.FormFieldName.PreserveBreaks()).KillHtml().RestoreBreaks();
                        oldFormField.FormFieldPrompt = AntiXss.GetSafeHtmlFragment(thisField.FormFieldPrompt.PreserveBreaks()).KillHtml().RestoreBreaks();
                        oldFormField.IsRequired = Convert.ToByte(thisField.IsRequired);
                        oldFormField.SortOrder = sortOrder++;
                        //TODO: Not sure if this is per field type, but it shouldn't matter if validation works and nulls don't matter
                        oldFormField.Options = AntiXss.GetSafeHtmlFragment(thisField.Options.PreserveBreaks()).KillHtml().RestoreBreaks();
                        oldFormField.Orientation = thisField.Orientation;
                        oldFormField.IsMultipleSelect = Convert.ToByte(thisField.IsMultipleSelect);
                        oldFormField.ListSize = thisField.ListSize;
                        oldFormField.IsEmptyOption = Convert.ToByte(thisField.IsEmptyOption);
                        oldFormField.EmptyOption = thisField.EmptyOption;
                        oldFormField.Rows = thisField.Rows;
                        oldFormField.Cols = thisField.Cols;
                        oldFormField.ValidExtensions = AntiXss.GetSafeHtmlFragment(thisField.ValidExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                        oldFormField.ErrorExtensions = AntiXss.GetSafeHtmlFragment(thisField.ErrorExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                        oldFormField.MaxSizeBytes = thisField.MaxSizeBytes;
                        oldFormField.LiteralText = AntiXss.GetSafeHtml(thisField.LiteralText);
                    }
                    else
                    {
                        var thisFormFieldUid = new Guid(formField.SelectedFormFieldType);
                        var newFormField = _mvcForms.FormFields.CreateObject();
                        newFormField.FormUid = thisFormUid;
                        newFormField.Uid = Guid.NewGuid();
                        newFormField.FormFieldTypeUid = thisFormFieldUid;
                        newFormField.FormFieldName = AntiXss.GetSafeHtmlFragment(thisField.FormFieldName.PreserveBreaks()).KillHtml().RestoreBreaks();
                        newFormField.FormFieldPrompt = AntiXss.GetSafeHtmlFragment(thisField.FormFieldPrompt.PreserveBreaks()).KillHtml().RestoreBreaks();
                        newFormField.IsHidden = 0;
                        newFormField.IsRequired = Convert.ToByte(thisField.IsRequired);
                        newFormField.SortOrder = sortOrder++;
                        newFormField.Timestamp = DateTime.Now;
                        //TODO: Not sure if this is per field type, but it shouldn't matter if validation works and nulls don't matter
                        newFormField.Options = AntiXss.GetSafeHtmlFragment(thisField.Options.PreserveBreaks()).KillHtml().RestoreBreaks();
                        newFormField.Orientation = thisField.Orientation;
                        newFormField.IsMultipleSelect = Convert.ToByte(thisField.IsMultipleSelect);
                        newFormField.ListSize = thisField.ListSize;
                        newFormField.IsEmptyOption = Convert.ToByte(thisField.IsEmptyOption);
                        newFormField.EmptyOption = thisField.EmptyOption;
                        newFormField.Rows = thisField.Rows;
                        newFormField.Cols = thisField.Cols;
                        newFormField.ValidExtensions = AntiXss.GetSafeHtmlFragment(thisField.ValidExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                        newFormField.ErrorExtensions = AntiXss.GetSafeHtmlFragment(thisField.ErrorExtensions.PreserveBreaks()).KillHtml().RestoreBreaks();
                        newFormField.MaxSizeBytes = thisField.MaxSizeBytes;
                        newFormField.LiteralText = AntiXss.GetSafeHtml(thisField.LiteralText);
                        _mvcForms.AddToFormFields(newFormField);
                    }
                }
                _mvcForms.SaveChanges();
                return RedirectToAction("List", new { Message = "updated" });
            //}
            //Rebuild the drop-down lists since they're not in the postback
            foreach (var formField in viewModel.FormFields)
            {
                formField.FormFieldTypes = GetFormFieldTypes();
            }
            return View(viewModel);                
        }

        [HttpGet]
        public ActionResult Respond(string shortPath)
        {
            var thisForm = _mvcForms.Forms.FirstOrDefault(form => form.ShortPath == shortPath);
            if (thisForm != null)
            {
                ViewData["FormName"] = thisForm.FormName;
                //Only retrieve non-hidden fields
                var formFields =
                    _mvcForms.FormFields.Where(formitems => formitems.FormUid == thisForm.Uid & formitems.IsHidden == 0).OrderBy(
                        formitem => formitem.SortOrder);
                //CUSTOM, reference http://mvcdynamicforms.codeplex.com/
                //Using the MVC Dynamic Form project, build a form comprised of fields tied to the form items used by this custom form
                //TODO: Consider switching to conditionally build views using partial views with the KnockOutJS library for custom validation
                if (formFields.Count() > 0)
                {
                    var dynamicFormFields = new List<Field>();
                    foreach (var formField in formFields)
                    {
                        var field = formField;
                        var thisFieldType =
                            _mvcForms.FormFieldTypes.Single(fieldtype => fieldtype.Uid == field.FormFieldTypeUid);
                        switch (thisFieldType.FieldType)
                        {
                            case ("Literal"):
                                dynamicFormFields.Add(new TextBox()
                                                          {
                                                              Key = field.Uid.ToString(),
                                                              Template =
                                                                  String.Format("<p>{0}</p>",
                                                                                field.LiteralText.KillHtml()),
                                                              DisplayOrder = field.SortOrder
                                                          });
                                break;
                            case ("TextBox"):
                                dynamicFormFields.Add(new TextBox()
                                                          {
                                                              Key = field.Uid.ToString(),
                                                              ResponseTitle = field.FormFieldName,
                                                              Prompt =
                                                                  (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                      ? field.FormFieldPrompt
                                                                      : null,
                                                              DisplayOrder = field.SortOrder,
                                                              Required = Convert.ToBoolean(field.IsRequired),
                                                              RequiredMessage =
                                                                  Convert.ToBoolean(field.IsRequired)
                                                                      ? thisFieldType.ErrorMsgRequired.Replace(
                                                                          "%FormFieldName%", field.FormFieldName)
                                                                      : string.Empty,
                                                              RegularExpression =
                                                                  (!string.IsNullOrEmpty(thisFieldType.RegExDefault))
                                                                      ? thisFieldType.RegExDefault
                                                                      : string.Empty,
                                                              RegexMessage =
                                                                  (!string.IsNullOrEmpty(thisFieldType.RegExDefault))
                                                                      ? thisFieldType.ErrorMsgRegEx.Replace(
                                                                          "%FormFieldName%", field.FormFieldName)
                                                                      : string.Empty
                                                          });
                                break;
                            case ("TextArea"):
                                var newTextArea = new TextArea()
                                                      {
                                                          Key = field.Uid.ToString(),
                                                          ResponseTitle = field.FormFieldName,
                                                          Prompt =
                                                              (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                  ? field.FormFieldPrompt
                                                                  : null,
                                                          DisplayOrder = field.SortOrder,
                                                          Required = Convert.ToBoolean(field.IsRequired),
                                                          RequiredMessage =
                                                              Convert.ToBoolean(field.IsRequired)
                                                                  ? thisFieldType.ErrorMsgRequired.Replace(
                                                                      "%FormFieldName%", field.FormFieldName)
                                                                  : string.Empty,
                                                          RegularExpression =
                                                              (!string.IsNullOrEmpty(thisFieldType.RegExDefault))
                                                                  ? thisFieldType.RegExDefault
                                                                  : string.Empty,
                                                          RegexMessage =
                                                              (!string.IsNullOrEmpty(thisFieldType.RegExDefault))
                                                                  ? thisFieldType.ErrorMsgRegEx.Replace(
                                                                      "%FormFieldName%",
                                                                      field.FormFieldName)
                                                                  : string.Empty
                                                      };
                                int number;
                                if (Int32.TryParse(field.Rows.ToString(), out number))
                                    newTextArea.InputHtmlAttributes.Add("rows", field.Rows.ToString());
                                if (Int32.TryParse(field.Cols.ToString(), out number))
                                    newTextArea.InputHtmlAttributes.Add("cols", field.Cols.ToString());
                                dynamicFormFields.Add(newTextArea);
                                break;
                            case ("SelectList"):
                                var newSelectList = new Select
                                                        {
                                                            Key = field.Uid.ToString(),
                                                            ResponseTitle = field.FormFieldName,
                                                            Prompt =
                                                                (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                    ? field.FormFieldPrompt
                                                                    : null,
                                                            DisplayOrder = field.SortOrder,
                                                            Required = Convert.ToBoolean(field.IsRequired),
                                                            RequiredMessage =
                                                                Convert.ToBoolean(field.IsRequired)
                                                                    ? thisFieldType.ErrorMsgRequired.Replace(
                                                                        "%FormFieldName%", field.FormFieldName)
                                                                    : string.Empty
                                                        };
                                if (Convert.ToBoolean(field.IsMultipleSelect))
                                {
                                    newSelectList.MultipleSelection = true;
                                    newSelectList.Size = Convert.ToInt32(field.ListSize);
                                    newSelectList.CommaDelimitedChoices = field.Options.Replace(Environment.NewLine, ",");                                
                                }
                                else
                                {
                                    newSelectList.ShowEmptyOption = Convert.ToBoolean(field.IsEmptyOption);
                                    newSelectList.EmptyOption = (Convert.ToBoolean(field.IsEmptyOption))
                                                                    ? field.EmptyOption
                                                                    : null;
                                    newSelectList.CommaDelimitedChoices = field.Options.Replace(Environment.NewLine, ",");
                                }
                                dynamicFormFields.Add(newSelectList);
                                break;
                            case ("CheckBox"):
                                var newCheckBox = new CheckBox
                                                      {
                                                          Key = field.Uid.ToString(),
                                                          ResponseTitle = field.FormFieldName,
                                                          Prompt =
                                                              (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                  ? field.FormFieldPrompt
                                                                  : null,
                                                          DisplayOrder = field.SortOrder,
                                                          Required = Convert.ToBoolean(field.IsRequired),
                                                          RequiredMessage =
                                                              Convert.ToBoolean(field.IsRequired)
                                                                  ? thisFieldType.ErrorMsgRequired.Replace(
                                                                      "%FormFieldName%", field.FormFieldName)
                                                                  : string.Empty
                                                      };
                                dynamicFormFields.Add(newCheckBox);
                                break;
                            case ("CheckBoxList"):
                                var newCheckBoxList = new CheckBoxList
                                                          {
                                                              Key = field.Uid.ToString(),
                                                              ResponseTitle = field.FormFieldName,
                                                              Prompt =
                                                                  (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                      ? field.FormFieldPrompt
                                                                      : null,
                                                              DisplayOrder = field.SortOrder,
                                                              Required = Convert.ToBoolean(field.IsRequired),
                                                              RequiredMessage =
                                                                  Convert.ToBoolean(field.IsRequired)
                                                                      ? thisFieldType.ErrorMsgRequired.Replace(
                                                                          "%FormFieldName%", field.FormFieldName)
                                                                      : string.Empty,
                                                              CommaDelimitedChoices = field.Options.Replace(Environment.NewLine, ","),
                                                              Orientation = (field.Orientation == "vertical") ? Orientation.Vertical : Orientation.Horizontal
                                                          };
                                dynamicFormFields.Add(newCheckBoxList);
                                break;
                            case ("RadioList"):
                                var newRadioList = new RadioList
                                                          {
                                                              Key = field.Uid.ToString(),
                                                              ResponseTitle = field.FormFieldName,
                                                              Prompt =
                                                                  (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                      ? field.FormFieldPrompt
                                                                      : null,
                                                              DisplayOrder = field.SortOrder,
                                                              Required = Convert.ToBoolean(field.IsRequired),
                                                              RequiredMessage =
                                                                  Convert.ToBoolean(field.IsRequired)
                                                                      ? thisFieldType.ErrorMsgRequired.Replace(
                                                                          "%FormFieldName%", field.FormFieldName)
                                                                      : string.Empty,
                                                              CommaDelimitedChoices = field.Options.Replace(Environment.NewLine, ","),
                                                              Orientation = (field.Orientation == "vertical") ? Orientation.Vertical : Orientation.Horizontal
                                                          };
                                dynamicFormFields.Add(newRadioList);
                                break;
                            case ("FileUpload"):
                                var newFileUpload = new FileUpload
                                                        {
                                                            Key = field.Uid.ToString(),
                                                            ResponseTitle = field.FormFieldName,
                                                            Prompt =
                                                                (!string.IsNullOrEmpty(field.FormFieldPrompt))
                                                                    ? field.FormFieldPrompt
                                                                    : null,
                                                            InvalidExtensionError = field.ErrorExtensions,
                                                            Required = Convert.ToBoolean(field.IsRequired),
                                                            RequiredMessage =
                                                                Convert.ToBoolean(field.IsRequired)
                                                                    ? thisFieldType.ErrorMsgRequired.Replace(
                                                                        "%FormFieldName%", field.FormFieldName)
                                                                    : string.Empty,
                                                            DisplayOrder = field.SortOrder,
                                                        };
                                if (!string.IsNullOrEmpty(field.ValidExtensions))
                                    newFileUpload.ValidExtensions = "." +
                                                                    field.ValidExtensions.Replace(Environment.NewLine,
                                                                                                  ",.");
                                var user = Membership.GetUser(User.Identity.Name);
                                UserKey = user.ProviderUserKey.ToString();
                                newFileUpload.Validated += FileValidated;
                                newFileUpload.Posted += FilePosted;
                                dynamicFormFields.Add(newFileUpload);
                                break;
                        }
                    }
                    var dynamicForm = new MvcDynamicForms.Form();
                    dynamicForm.AddFields(dynamicFormFields.ToArray());
                    dynamicForm.Serialize = true;
                    ViewData["FormUid"] = thisForm.Uid;
                    return View(dynamicForm);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        protected static string UserKey = string.Empty;

        static void FilePosted(MvcDynamicForms.Fields.FileUpload fileUploadField, EventArgs e)
        {
            // here, you can do something with the posted file
            // (save it, email it, etc, or test it and report back to the user)
            // this event gets fired as soon as the dynamic form is model bound
            var thisDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\Content\" + UserKey;
            if (!Directory.Exists(thisDirectory))
                Directory.CreateDirectory(thisDirectory);
            var fileNameArray = fileUploadField.PostedFile.FileName.Split('\\');
            var fileName = fileNameArray[fileNameArray.GetUpperBound(0)];
            var fileSavePath = thisDirectory + @"\" + fileName;
            fileUploadField.PostedFile.SaveAs(fileSavePath);
        }

        static void FileValidated(InputField inputField, InputFieldValidationEventArgs e)
        {

            // here, you can also do something with the posted file
            // (save it, email it, etc, or test it and report back to the user)
            // this event gets fired following the validation of any class derived from InputField
            // here you can have more fine grained control of validation
            // for example:

            if (e.IsValid)
            {
                var fileUpload = inputField as FileUpload;

                if (fileUpload != null)
                    if (fileUpload.PostedFile.ContentLength > 102400)
                    {
                        fileUpload.Error = "The file is too large.";
                    }
            }
        }

        [HttpPost]
        public ActionResult Respond(MvcDynamicForms.Form form)
        {
            var formInfo = (FormInfoSection) System.Configuration.ConfigurationManager.GetSection("formInfoGroup/formInfo");
            var formUid = new Guid(Request["FormUid"]);
            if (form.Validate())
            {
                var responseUid = Guid.NewGuid();
                _mvcResponses.AddToFormResponses(new FormResponse { Uid = responseUid, FormUid = formUid, Timestamp = DateTime.Now });
                foreach (var inputfield in form.InputFields)
                {
                    string response;
                    var fieldType = inputfield.GetType().ToString();
                    switch (fieldType)
                    {                          
                        case "MvcDynamicForms.Fields.FileUpload":
                            response = inputfield.Response.Split('\\')[inputfield.Response.Split('\\').GetUpperBound(0)];
                            break;
                        default:
                            response = inputfield.Response;
                            if (response.Length > formInfo.MaxLength.Value)
                                response = response.Substring(0, formInfo.MaxLength.Value);
                            break;
                    }
                    var key = new Guid(inputfield.Key);
                    if (fieldType != "MvcDynamicForms.Fields.Literal" && !string.IsNullOrEmpty(response)) 
                        _mvcResponses.AddToFormResponseItems(new FormResponseItem { Uid  = Guid.NewGuid(), FormResponseUid = responseUid, FormItemUid = key, ResponseStr = response, Timestamp = DateTime.Now });
                }
                _mvcResponses.SaveChanges();
                return null;
            }
            ViewData["FormUid"] = formUid.ToString();
            ViewData["FormName"] = _mvcForms.Forms.FirstOrDefault(f => f.Uid == formUid).FormName;
            return View(form);
        }
        
        //Optional values or not to optional values... hmm...
        protected List<SelectListItem> GetFormFieldTypes(bool includeEmptyChoice = false)
        {
            var formFieldTypeList = new List<SelectListItem>();
            if (includeEmptyChoice)
                formFieldTypeList.Add(new SelectListItem { Selected = true, Text = "--Select a Field Type--", Value = string.Empty } );
            //Retain the foreach since a LINQ statement bombs on the .ToString for Uid
            foreach (var formFieldType in _mvcForms.FormFieldTypes.OrderBy(formfield => formfield.SortOrder))
            {
                formFieldTypeList.Add(new SelectListItem { Text = formFieldType.FieldTypeName, Value = formFieldType.Uid.ToString() });
            }
            return formFieldTypeList;
        }

        //[Authorize]
        [HttpGet]
        public AjaxViewResult NewFormField(string formId, Guid selectedFieldType)
        {
            var fieldType = _mvcForms.FormFieldTypes.Single(type => type.Uid == selectedFieldType);
            var viewModel = new FormFieldViewModel
                                {
                                    Uid = Guid.NewGuid(),
                                    ShowDelete = true,
                                    FormFieldTypes = GetFormFieldTypes(),
                                    SelectedFormFieldType = selectedFieldType.ToString(),
                                    //TODO: Not sure if this is per field type, but it shouldn't matter 
                                    //Create a default radiobutton selection
                                    Orientation = "vertical" 
                                };
            return new AjaxViewResult(fieldType.ControlType, viewModel) { UpdateValidationForFormId = formId };        
        }

        // CUSTOM, reference http://blog.stevensanderson.com/2010/01/28/validating-a-variable-length-list-aspnet-mvc-2-style/
        // Inserted by Jon Jenkins, 2010-07-17
        public class AjaxViewResult : ViewResult
        {
            public string UpdateValidationForFormId { get; set; }

            public AjaxViewResult(string viewName, object model)
            {
                ViewName = viewName;
                ViewData = new ViewDataDictionary { Model = model };
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var result = base.FindView(context);
                var viewContext = new ViewContext(context, result.View, ViewData, TempData, context.HttpContext.Response.Output);

                BeginCapturingValidation(viewContext);
                base.ExecuteResult(context);
                EndCapturingValidation(viewContext);

                result.ViewEngine.ReleaseView(context, result.View);
            }

            private void BeginCapturingValidation(ViewContext viewContext)
            {
                if (string.IsNullOrEmpty(UpdateValidationForFormId))
                    return;
                viewContext.ClientValidationEnabled = true;
                viewContext.FormContext = new FormContext { FormId = UpdateValidationForFormId };
            }

            private static void EndCapturingValidation(ViewContext viewContext)
            {
                if (!viewContext.ClientValidationEnabled)
                    return;
                viewContext.OutputClientValidation();
                viewContext.Writer.WriteLine("<script type=\"text/javascript\">Sys.Mvc.FormContext._Application_Load()</script>");
            }
            //End CUSTOM
        }
    }
}
