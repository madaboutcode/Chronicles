using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chronicles.Web.Utility;

namespace Chronicles.Web.ViewModels
{
    [MetadataType(typeof(CommentDetails_Validations))]
    public class CommentDetails
    {
        public string Text { get; set; }
        
        public int Id { get; set; }

        public int PostId { get; set; }

        public string UserName { get; set; }
        
        public string UserWebSite { get; set; }
        
        public string UserEmail { get; set; }
        
        public DateTime Date { get; set; }

        class CommentDetails_Validations
        {
            [Required(ErrorMessage = "Please provide some comments")]
            public string Text { get; set; }

            public int Id { get; set; }

            public int PostId { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [StringLength(150, ErrorMessage = "Name should be within 150 characters")]
            public string UserName { get; set; }

            [Url(ErrorMessage = "Provide a valid url")]
            public string UserWebSite { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [RegularExpression(@"^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$", ErrorMessage = "Please provide a valid email address")]
            public string UserEmail { get; set; }

            public DateTime Date { get; set; }
        }
    }
}
