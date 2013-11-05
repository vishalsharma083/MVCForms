using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MVCForms.Models
{
    //-----------------------------------------------------------------------------------------------------
    //Form

    public class FormViewModel
    {
        //Properties
        public Form Form { get; set; }
        public List<FormFieldViewModel> FormFields { get; set; }

        //Constructor
        public FormViewModel()
        {
            Form = new Form();
            FormFields = new List<FormFieldViewModel>();
        }
    }

    public class FormResponseViewModel
    {
        //Properties
        public Form Form { get; set; }
        public List<FormField> FormFields { get; set; }
        public List<FormFieldType> FormFieldTypes { get; set; }

        //Constructor
        public FormResponseViewModel()
        {
            Form = new Form();
            FormFields = new List<FormField>();
            FormFieldTypes = new List<FormFieldType>();
        }
    }

    public class FormFieldViewModel
    {
        //Set int properties to int? nullable to avoid issues across field types

        //Primary key
        public Guid Uid { get; set; }

        //Control type
        public string ControlType { get; set; }

        //Settings
        public bool ShowDelete { get; set; }
        public bool IsRequired { get; set; }

        //Name
        [Required(ErrorMessage = "* Required field")]
        public string FormFieldName { get; set; }

        //Prompt (description)
        public string FormFieldPrompt { get; set; }

        //Form field type (list & value)
        public List<SelectListItem> FormFieldTypes { get; set; }
        [Required(ErrorMessage = "* Select a field type")]
        public string SelectedFormFieldType { get; set; }

        //List options, row delimited
        [Required(ErrorMessage = "* Please enter an option")]
        public string Options { get; set; }

        //List layout, horizontal or vertical
        [Required(ErrorMessage = "* Please select orientation")]
        public string Orientation { get; set; }     
   
        //Select list options
        public bool IsMultipleSelect { get; set; }
        [Range(1, 20, ErrorMessage = "* Must be between 1-20")]
        public int? ListSize { get; set; }
        public bool IsEmptyOption { get; set; }
        public string EmptyOption { get; set; }

        //Text area options
        [Range(1, 50, ErrorMessage = "* Must be between 1-50")]
        public int? Rows { get; set; }
        [Range(1, 100, ErrorMessage = "* Must be between 1-100")]
        public int? Cols { get; set; }

        //File upload options
        [RegularExpression(@"[^a-zA-Z0-9]", ErrorMessage = "Please enter valid file extension(s)")]
        public string ValidExtensions { get; set; }
        public string ErrorExtensions { get; set; }
        [Required(ErrorMessage = "* Please enter a max size")]
        [Range(1, 10000000, ErrorMessage = "* Must be between 1-10000000 bytes")]
        public int? MaxSizeBytes { get; set; }

        //Literal options
        [Required(ErrorMessage = "* Please enter display text")]
        public string LiteralText { get; set; }
    }

    //-----------------------------------------------------------------------------------------------------

    //-----------------------------------------------------------------------------------------------------
    //Effectively, this is the admin ViewModel
    public class UserProfileViewModel
    {
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }

    //TODO: May wish to reconsider managing this data for a fuller per-user admin ViewModel?
    public class UserAllocationsViewModel
    {
        [DisplayName("User ID")]
        public Guid UserId { get; set; }
        
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Available Submissions")]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d+", ErrorMessage = "Please enter a valid number.")]
        [Required(ErrorMessage = "Please enter a value for remaining, available submissions.")]
        [Range(0, 99999, ErrorMessage = "Please enter a number between 0 - 99,999")]
        public int AvailableSubmissions { get; set; }

        [DisplayName("Last Submission Date")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date-time format (e.g. 1/1/1900 12:00:00AM).")]
        public DateTime LastSubmission { get; set; }
    }
    //-----------------------------------------------------------------------------------------------------
}