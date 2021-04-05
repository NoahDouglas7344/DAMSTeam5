using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS5800Proj.Pages
{
	public class ContactModel : PageModel
	{
		public string information { get; set; }

		public void OnGet(string address, string email, string phone)
		{
			
		}

		public void OnPost(string address, string email, string phone)
		{
			information = "Here is the contact information\n " +
							"for your donation:\nAddress: " + address + "\nEmail:" + email +
							"\nPhone Number:" + phone;
		}
	}
}
