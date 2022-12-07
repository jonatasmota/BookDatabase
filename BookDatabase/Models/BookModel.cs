using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter book Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter book Year")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Enter book Author")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Enter book Status")]
        public string Status { get; set; }

        
        [DisplayName("Upload Image")]
        public string Image { get; set; }

        [Display(Name = "Choose an Image for the book")]
        [Required(ErrorMessage = "Please upload an Image!")]
        [NotMapped]
        public IFormFile BookImage { get; set; }
    }
}
