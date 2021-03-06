//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineFloralDelivery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Blogs = new HashSet<Blog>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter firtname")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter lastname")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select birthdate")]
        [DataType(DataType.Date, ErrorMessage = "Birthdate must be date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choice gender")]
        public bool Gender { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter phone")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Incorrect Phone Number Format")]
        public string Phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        public string Picture { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select role")]
        [Range(minimum: 0, maximum: 2, ErrorMessage = "Please select role")]
        public int Role { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter status")]
        public bool Status { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
