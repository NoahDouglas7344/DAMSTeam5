using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS5800Proj.Pages
{
    public class DonationRequestModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string disaster { get; set; }
        

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            return RedirectToPage("./Home");
        }
    }
}
