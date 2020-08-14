using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerSeller.ViewModel
{
    public class ServiceViewModel
    {

        [Required(ErrorMessage = "Please enter Title")]
        [Display(Name = "Title")]
        [StringLength(100)]
        public string Title { get; set; }


        [Required(ErrorMessage = "Please enter Description")]
        [Display(Name = "Description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Tags")]
        [Display(Name = "Tags")]
        [StringLength(1000)]
        public string tags { get; set; }

        [Required(ErrorMessage = "Please choose  image")]
        [Display(Name = "Picture")]
        public IFormFile Picture { get; set; }
 
    }
}
