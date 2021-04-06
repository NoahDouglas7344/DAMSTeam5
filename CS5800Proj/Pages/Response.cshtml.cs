using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
	public class ResponseModel : PageModel
	{
        public List<String> requests = new List<String>();
        [Display(Name = "Responses:")]
        [BindProperty (SupportsGet = true)]
        public string recipient { get; set; }
        public string type { get; set; }
        public string amount { get; set; }

        public void OnGet()
		{
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var recipients = new MySqlCommand("SELECT Disaster FROM Requests", connection);
                {
                    using (MySqlDataReader recipientreader = recipients.ExecuteReader())
                    {
                        while (recipientreader.Read())
                        {
                            requests.Add(recipientreader["Disaster"].ToString());
                        }
                    }
                }
                using var descriptions = new MySqlCommand("SELECT description FROM testEvents", connection);
                {
                    using (MySqlDataReader descriptionreader = descriptions.ExecuteReader())
                    {
                        while (descriptionreader.Read())
                        {
                            requests.Add(descriptionreader["description"].ToString());
                        }
                    }
                }
            }
        }

        public IActionResult OnPost(string recipient, string type, int amount)
        {
            bool recipientFound = false;

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT description FROM testEvents;", connection);
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
                using var command = new MySqlCommand("SELECT " + type + " FROM testEvents WHERE description = '" + recipient + "';", connection);
                using var reader = command.ExecuteReader();
                reader.Read();
                int initValue = (int)reader.GetValue(0);
                reader.Close();
                using var update = new MySqlCommand("UPDATE testEvents SET " + type + " = " + (initValue + amount) + " WHERE description = '" + recipient + "';", connection);
                update.ExecuteNonQuery();

                var pageModel = new CS5800Proj.Pages.ContactModel();
                var httpContext = new DefaultHttpContext();

                var info = new MySqlCommand("SELECT address FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                var read = info.ExecuteReader();
                read.Read();
                string address = (string)read.GetValue(0);
                read.Close();

                info = new MySqlCommand("SELECT email FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                read = info.ExecuteReader();
                read.Read();
                string email = (string)read.GetValue(0);
                read.Close();

                info = new MySqlCommand("SELECT phone FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                read = info.ExecuteReader();
                read.Read();
                string phone = (string)read.GetValue(0);
                read.Close();


                return RedirectToPage("./Response");//(pageModel.OnPost(address, email, phone));
            }
            return RedirectToPage("./ResponseFail");
        }
    }
}
