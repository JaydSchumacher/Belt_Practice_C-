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

    public class Belt
    {
        public int beltid { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public string belt_name { get; set; }
        public int users_id { get; set; }
        [ForeignKey("users_id")]
        public User user { get; set; } 

        public List<BeltsAchieved> belts { get; set; }
        
        public Belt ()
        {
            belts = new List<BeltsAchieved>();
        }
    }
}