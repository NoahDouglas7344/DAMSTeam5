using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
	public class ResponseModel : PageModel
	{

        [BindProperty (SupportsGet = true)]
        public string recipient { get; set; }
        public string type { get; set; }
        public string amount { get; set; }

        public void OnGet()
		{

		}

        public IActionResult OnPost(string recipient, string type, int amount)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            bool recipientFound = false;

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT recipient FROM testEvents;", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var value = reader.GetValue(0);
                    if (value.ToString() == recipient)
                    {
                        recipientFound = true;
                    }
                }
            }
            if (recipientFound)
            {
                using var command = new MySqlCommand("SELECT " + type + " FROM testEvents WHERE recipient = '" + recipient + "';", connection);
                using var reader = command.ExecuteReader();
                reader.Read();
                int initValue = (int)reader.GetValue(0);
                reader.Close();
                using var update = new MySqlCommand("UPDATE testEvents SET " + type + " = " + (initValue + amount) + " WHERE recipient = '" + recipient + "';", connection);
                update.ExecuteNonQuery();
                return RedirectToPage("./Home");
            }
            return Page();
        }
    }
}
