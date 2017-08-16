using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using travel.Models;
using travel.Factories;

namespace travel.Controllers
{
    public class DashController : Controller
    {
        private readonly UserFactory userFactory;
        public DashController(UserFactory connection)
        {
            userFactory = connection;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home(){
            if(HttpContext.Session.GetInt32("logged") == null){
                return Redirect("/login");
            }
            int? user_id = HttpContext.Session.GetInt32("logged");
            User user = userFactory.GetUserById((int) user_id);

            ViewBag.user = user;
            return View("travel");
        }
    }
}