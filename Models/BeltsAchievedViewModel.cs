using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Belt_Exam.Models;
using Microsoft.AspNetCore.Http;


namespace Belt_Exam.Models
{

    public class BeltsAchievedViewModel
    {
        [Required]
        [FutureDate(ErrorMessage = "Date cannot be in the future")]
        public DateTime achieved_at { get; set; }

        
   
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value < DateTime.Today;
        }
    } 
}