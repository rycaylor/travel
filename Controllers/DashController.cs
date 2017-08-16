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
        private readonly TripFactory tripFactory;
        public DashController(UserFactory connection, TripFactory connection1)
        {
            userFactory = connection;
            tripFactory = connection1;
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

            ViewBag.trips = tripFactory.GetAllTrips();
            return View("travel");
        }

        [HttpGet]
        [Route("createtrip")]
        public IActionResult CreateTrip(){
            if(HttpContext.Session.GetInt32("logged") == null){
                return Redirect("/login");
            }
            int? user_id = HttpContext.Session.GetInt32("logged");

            return View("Createtrip");
        }

        [HttpPost]
        [Route("addtrip")]
        public IActionResult AddTrip(TripVal newTrip)
        {
            if(ModelState.IsValid)
            {
                Trip passTrip = new Trip
                {
                    Title = newTrip.Title,
                    Description = newTrip.Description,
                    LeaveDate = newTrip.LeaveDate,
                    ReturnDate = newTrip.ReturnDate,
                };

                passTrip.UserId = (int)HttpContext.Session.GetInt32("logged");
                if(passTrip.LeaveDate<passTrip.ReturnDate && passTrip.LeaveDate>DateTime.Now)
                {
                    tripFactory.CreateTrip(passTrip);
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.error = "Invalid Dates!";
                    return View("Createtrip");
                }
            }
            return View("Createtrip");
        }
    }
}