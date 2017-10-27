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

    public class User
    {
        public int userid {get; set;}
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        
        public List<BeltsAchieved> BeltAchieved { get; set; }
        
        public User ()
        {
            BeltAchieved = new List<BeltsAchieved>();
        }
        
    }
}