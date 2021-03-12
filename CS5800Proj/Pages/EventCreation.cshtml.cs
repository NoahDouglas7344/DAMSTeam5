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

		public void OnPost()
		{
			var recipient = Request.Form["recipient"];
			var time = Request.Form["time"];
			var location = Request.Form["location"];
			
		}
	}
}
