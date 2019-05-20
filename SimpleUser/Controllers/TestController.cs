using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleUser.Models;

namespace SimpleUser.Controllers
{
    using Constants;
    public class TestController : Controller
    {
        public IActionResult LoggedIn()
        {
            string sessionUser = HttpContext.Session.GetString(Constants.Statekey);
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var userState = JsonConvert.DeserializeObject<UserState>(sessionUser);

            if (userState.IsLoggedIn)
            {
                return View();
            }

            return RedirectToAction("Login", "Users");
        }

        public IActionResult AdminOnly()
        {
            string sessionUser = HttpContext.Session.GetString(Constants.Statekey);
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var userState = JsonConvert.DeserializeObject<UserState>(sessionUser);

            if (userState.IsLoggedIn && userState.IsAdmin)
            {
                return View();
            }

            return RedirectToAction("Login", "Users");
        }
    }
}