using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

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

        public IActionResult OnPost(string userName, string passWord)
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }
            bool userFound = false;
            bool passwordMatch = false;

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=remoteUser;password=dbLogin123");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT userName, passWord FROM testUser;", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var value = reader.GetValue(0);
                    if(value.ToString() == userName && reader.GetValue(1).ToString() == passWord)
                    {
                        userFound = true;
                        passwordMatch = true;
                    }
                }
            }
            if (userFound && passwordMatch)
            {
               return RedirectToPage("./Home");
            }
            return Page();
        }
    }
}
