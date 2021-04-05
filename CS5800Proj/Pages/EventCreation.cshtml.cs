using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
	public class EventCreationModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public string description { get; set; }
		public string time { get; set; }
		public string location { get; set; }
		public string address{ get; set; }
		public string email { get; set; }
		public string phone { get; set; }

		public void OnGet()
		{
		}

		public IActionResult OnPost(string description, string time,
			string location, string address, string email, string phone)
		{

			if (description == "" || time == "" || location == "" || address == ""
				|| email == "" || phone == "")
			{
				return Page(); //Reloads the page is there was an empty field
			}

			using var connection = new MySqlConnection("server=localhost;port=3306;" +
				"database=testDB;user=root;password=CS5800Team5");
			{
				connection.Open();

				using var command = new MySqlCommand("INSERT INTO testEvents" +
					"(description, time, location, address, email, phone) " +
					"values('"+description+"', '"+time+"', '"+location+"', '" +
					address+"', '"+email+"', '"+phone+"')", connection);
				var adapter = command.ExecuteNonQuery();
				if (adapter > 0)
					return RedirectToPage("./EventConfirmation"); //Used to check if table insertion was sucessful
			}
			return Page(); //Reloads the page if there was an issue inserting the values to the table
		}
	}
}
