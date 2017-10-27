using System.ComponentModel.DataAnnotations;


namespace Belt_Exam.Models
{

    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string first_name { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string last_name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "Password confirmation must match Password")]
        public string cpassword { get; set; }

        
    }
}