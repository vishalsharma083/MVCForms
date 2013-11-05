using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCForms.Models
{
    [MetadataType(typeof(FormMetaData))]
    public partial class Form
    {
        [Bind(Exclude = "Uid")]
        public class FormMetaData
        {
            [ScaffoldColumn(false)]
            public object Uid { get; set; }

            [ScaffoldColumn(false)]
            public object UserId { get; set; }

            [ScaffoldColumn(false)]
            public object Timestamp { get; set; }

            [DisplayName("Form Name")]
            [DataType(DataType.Text)]
            [Required(ErrorMessage = "A name for your form is required.")]
            [StringLength(255, ErrorMessage = "Please ensure that the form title you enter is less than 50 characters.")]
            public object FormName { get; set; }
        }
    }
}