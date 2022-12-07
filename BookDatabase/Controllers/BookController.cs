using BookDatabase.Models;
using BookDatabase.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepo _bookRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public BookController(IBookRepo bookRepo, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepo = bookRepo;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<BookModel> books = _bookRepo.FindAll();
            
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            BookModel book = _bookRepo.FindById(id);

            return View(book);
        }

        public IActionResult Details(int id)
        {
            BookModel book = _bookRepo.FindById(id);

            return View(book);
        }

        public IActionResult DeleteConfirmation(int id)
        {
            BookModel book = _bookRepo.FindById(id);

            return View(book);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _bookRepo.Delete(id);
                if(deleted)
                {
                    TempData["SuccessMessage"] = "Book deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error, the book was not deleted. Try again.";
                }
                return RedirectToAction("Index");
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the book was not deleted. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookModel book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (book.BookImage != null)
                    {
                        string imgFolder = "books/images/";
                        book.Image = await UploadImage(imgFolder, book.BookImage);
                        
                    }
                    _bookRepo.Add(book);
                    TempData["SuccessMessage"] = "Book successfully added.";
                    return RedirectToAction("Index");
                }

                return View(book);
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the book was not added. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(BookModel book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (book.BookImage != null)
                    {
                        string imgFolder = "books/images/";
                        book.Image = await UploadImage(imgFolder, book.BookImage);

                    }
                    _bookRepo.Update(book);
                    TempData["SuccessMessage"] = "Book successfully updated.";
                    return RedirectToAction("Index");
                }

                return View(book);
            }
            catch (SystemException err)
            {
                TempData["ErrorMessage"] = $"Error, the book was not updated. Try again. {err.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
