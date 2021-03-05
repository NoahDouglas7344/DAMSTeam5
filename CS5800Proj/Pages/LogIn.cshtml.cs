using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS5800Proj.Pages
{
    public class LogInModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public string userName { get; set; }
        public string passWord { get; set; }
        public string userType { get; set; }

        public void OnGet()
        {
         
        }
        public void OnPost()
        {
             
        }
    }
}
