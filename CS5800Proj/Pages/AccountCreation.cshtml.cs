using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class AccountCreationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string userName { get; set; }
        public string passWord { get; set; }
        public string passWord2 { get; set; }
        public string userType { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string userName, string passWord,string passWord2, string userType)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            if(passWord == passWord2 && passWord.Length>2 && userName.Length >2 && userType != "Select...")
            {
                using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
                {
                    connection.Open();

                    using var command = new MySqlCommand("INSERT INTO testUser(userName, passWord, userType) values('"+userName+"', '"+passWord+"', '"+userType+"')", connection);
                    var adapter = command.ExecuteNonQuery();
                    if(adapter > 0)
                        return RedirectToPage("./Home");
                }
            }
            return Page();
        }
    }
}
