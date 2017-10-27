using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Belt_Exam.Models;
using Microsoft.AspNetCore.Http;

namespace Belt_Exam.Controllers
{
    public class BeltController : Controller
    {

        private Belt_ExamContext _context;
 
        public BeltController(Belt_ExamContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
            ViewBag.curUserName = HttpContext.Session.GetString("UserName");
            ViewBag.belts = _context.belts.Where(u => u.users_id == HttpContext.Session.GetInt32("Userid")).ToList();
            ViewBag.beltsachieved = _context.users.Where(u => u.userid == HttpContext.Session.GetInt32("Userid")).Include(u => u.BeltAchieved).ThenInclude(b => b.Belt).ToList();
            return View("Dashboard");
        }

        [HttpGet]
        [Route("newbelt")]
        public IActionResult NewAuction()
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            return View("AddBelt", "Belt");
        }

        [HttpPost]
        [Route("createbelt")]
        public IActionResult AddAuction(BeltViewModel model)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }

            
            Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
            if(ModelState.IsValid == false)
            {
                List<string> errorList = new List<string>();
                foreach (var error in ModelState.Values)
                {
                    foreach(var moreerrors in error.Errors){
                        errorList.Add(moreerrors.ErrorMessage);
                    }
                    
                }
                ViewBag.errors = errorList;
                return View("AddAuction");
            }
            else
            {
                Belt NewBelt = new Belt{
                    belt_name = model.belt_name,
                    description = model.description,
                    color = model.color,
                    user = _context.users.Where(user => user.userid == HttpContext.Session.GetInt32("Userid")).SingleOrDefault()          
                
                };
                _context.Add(NewBelt);
                _context.SaveChanges();
                ViewBag.curAuc = NewBelt;
                return RedirectToAction("Dashboard");
                
            }
        }

        [HttpGet]
        [Route("currentbelt/{beltid}")]
        public IActionResult CurrentBelt(int beltid)
        {
            HttpContext.Session.SetInt32("curBeltId", beltid);
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            List<Belt> curBelt = new List<Belt>();
            curBelt = _context.belts.Where(i => i.beltid == beltid).ToList();
            ViewBag.curBelt = curBelt;
            return View("CurrentBelt");
        }

        [HttpPost]
        [Route("completebelt/")]
        public IActionResult CompleteBelt(BeltsAchievedViewModel model, int beltid)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }

            Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
            if(ModelState.IsValid == false)
            {
                List<string> errorList = new List<string>();
                foreach (var error in ModelState.Values)
                {
                    foreach(var moreerrors in error.Errors){
                        errorList.Add(moreerrors.ErrorMessage);
                    }
                    
                }
                ViewBag.errors = errorList;
                return View("CurrentBelt");
            }
            else
            {
                          
                BeltsAchieved NewBeltAchieved = new BeltsAchieved{
                    userid = Convert.ToInt32(HttpContext.Session.GetInt32("Userid")),
                    beltid = beltid,
                    achieved_at = model.achieved_at      
                
                };
                _context.Add(NewBeltAchieved);
                _context.SaveChanges();
                ViewBag.curBeltAch = NewBeltAchieved;
                Console.WriteLine("Created Successfully");
                return RedirectToAction("Dashboard");
                
                
            }
        }  

        
    }
}
