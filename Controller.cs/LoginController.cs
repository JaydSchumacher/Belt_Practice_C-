using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Belt_Exam.Models;

namespace Belt_Exam.Controllers
{
    public class LoginController : Controller
    {

        private Belt_ExamContext _context;
 
        public LoginController(Belt_ExamContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {     
            Console.WriteLine("We are here");
            User ReturnValue = _context.users.SingleOrDefault(user => user.email == model.email); 
            if(ModelState.IsValid == false){
                List<string> errorList = new List<string>();
                foreach (var error in ModelState.Values)
                {
                    foreach(var moreerrors in error.Errors){
                        errorList.Add(moreerrors.ErrorMessage);
                    }
                    
                }
                ViewBag.errors = errorList;
                return View("Index");
            }
            else
            {
                if(ReturnValue == null){
                    User NewUser = new User{
                        first_name = model.first_name,
                        last_name = model.last_name,
                        email = model.email,
                        password = model.password,
                    };
                    int UserID = NewUser.userid;
                    HttpContext.Session.SetInt32("Userid", UserID);
                    HttpContext.Session.SetString("UserName", NewUser.first_name);
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    return RedirectToAction("dashboard", "Belt");
                }
                else
                {
                    List<string> errorList = new List<string>();
                    errorList.Add("Email already exists");
                    ViewBag.errors = errorList;               
                    return View("Index");
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {   
            User ReturnValue = _context.users.SingleOrDefault(user => user.email == email && user.password == password);
            if(ReturnValue != null)
            {
                int UserID = ReturnValue.userid;
                HttpContext.Session.SetInt32("Userid", UserID);
                HttpContext.Session.SetString("UserName", ReturnValue.first_name);
                return RedirectToAction("dashboard", "Belt");
            }            
            else
            {
                List<string> errorList = new List<string>();
                errorList.Add("Invalid Email/Password");
                ViewBag.errors = errorList;
                return View("Index");
            }     
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        
    }
}
