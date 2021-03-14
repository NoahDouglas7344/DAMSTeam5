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
		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			var recipient = Request.Form["recipient"];
			var time = Request.Form["time"];
			var location = Request.Form["location"];

			if (recipient == "" || time == "" || location == "")
			{
				return Page();
			}

			using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
			{
				connection.Open();

				using var command = new MySqlCommand("INSERT INTO testEvents(recipient, time, location) values('"+recipient+"', '"+time+"', '"+location+"')", connection);
				var adapter = command.ExecuteNonQuery();
				if (adapter > 0)
					return RedirectToPage("./Home");
			}
			return Page();
		}
	}
}
