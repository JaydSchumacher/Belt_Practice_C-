using System.ComponentModel.DataAnnotations;


namespace Belt_Exam.Models
{

    public class BeltViewModel
    {
        [Required]
        public string belt_name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string color { get; set; }

        
    }
}