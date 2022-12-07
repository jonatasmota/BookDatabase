using BookDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.ViewComponents
{
    public class Navbar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("userLoggedSession");

            if (string.IsNullOrEmpty(userSession)) return null;

            UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);

            return View(user);
        }
    }
}
