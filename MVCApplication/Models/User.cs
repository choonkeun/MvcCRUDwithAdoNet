
namespace MVCApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please type your UserId", AllowEmptyStrings=false)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please type your Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(30,MinimumLength=8,ErrorMessage="Password must be over 8 alphanumeric characters")]
        public string Password { get; set; }

        //---Add New Field to compare password
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage="Please Provide valid Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please type 1 for Mal2, 2 for Female", AllowEmptyStrings = false)]
        public Nullable<short> Gender { get; set; }

        [Required(ErrorMessage = "Please check true for Term & Conditions", AllowEmptyStrings = false)]
        public Nullable<bool> TermCondition { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}
