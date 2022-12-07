using BookDatabase.Models;
using BookDatabase.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepo userRepo, IWebHostEnvironment webHostEnvironment)
        {
            _userRepo = userRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _userRepo.FindAll();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepo.Add(user);
                    TempData["SuccessMessage"] = "User successfully added.";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the user was not added. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult DeleteConfirmation(int id)
        {
            UserModel user = _userRepo.FindById(id);

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _userRepo.Delete(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "User deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error, the user was not deleted. Try again.";
                }
                return RedirectToAction("Index");
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the user was not deleted. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            UserModel user = _userRepo.FindById(id);

            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(UserModelNoPassword userNoPass)
        {
            try
            {
                UserModel user = null;

                if (ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        Id = userNoPass.Id,
                        Name = userNoPass.Name,
                        Email = userNoPass.Email,
                        Username = userNoPass.Username
                    };

                    user = _userRepo.Update(user);
                    TempData["SuccessMessage"] = "User successfully updated.";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the user was not updated. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Details(int id)
        {
            UserModel user = _userRepo.FindById(id);

            return View(user);
        }
    }
}
