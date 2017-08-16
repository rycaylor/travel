using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using travel.Models;
using travel.Factories;

namespace travel.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserFactory userFactory;

        public HomeController(UserFactory connection){
            userFactory = connection;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate(User newUser)
        {
            TryValidateModel(newUser);
            if(ModelState.IsValid)
            {
               List<User> testing = userFactory.TestUser(newUser);
               if(testing.Count > 0)
               {
                   return View("Index");
               }
               else
               {
                   userFactory.CreateUser(newUser);
                   return View("Index");
               }
            }
            else
            {
                return View("Index");

            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        [Route("try")]
        public IActionResult Log(UserTest test)
        {
            TryValidateModel(test);
            if(ModelState.IsValid)
            {
                
                List<User> grab = userFactory.LoginUser(test);
                if(grab.Count > 0)
                {
                    int logged;
                    foreach(var user in grab)
                    {
                       logged  
                    }
                }
                else
                {
                    return View("login");
                }

                
            }
            else
            {
                return RedirectToAction("Login");

            }
        }

    }
}
