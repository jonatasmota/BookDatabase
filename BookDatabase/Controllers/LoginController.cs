using BookDatabase.Helper;
using BookDatabase.Models;
using BookDatabase.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ISession _session;

        public LoginController(IUserRepo userRepo, ISession session)
        {
            _userRepo = userRepo;
            _session = session;
        }

        public IActionResult Index()
        {
            // If user is logged redirect to Home

            if(_session.GetUserSession() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepo.FindByUsername(loginModel.Username);

                    if(user != null)
                    {
                        if (user.IsValidPassword(loginModel.Password))
                        {
                            _session.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["ErrorMessage"] = $"Error, Password is invalid. Try again.";
                    }

                    TempData["ErrorMessage"] = $"Error, Username or Password is invalid. Try again.";
                }

                return View("Index");
            }
            catch (Exception err)
            {
                TempData["ErrorMessage"] = $"Error, login was not possible. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult Logout()
        {
            _session.RemoveUserSession();

            return RedirectToAction("Index", "Login");
        }
    }


}
