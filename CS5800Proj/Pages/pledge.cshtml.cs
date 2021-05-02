using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class PledgeModel : PageModel
    {

        String curr_user = "user";
        [BindProperty(SupportsGet = true)]
        public string donor_location { get; set; }
        public string donation_items { get; set; }
        public int amount { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string donor_location, string donation_items, int amount)
        {
            if (ModelState.IsValid == false || amount < 1)
            {
                return Page();
            }

            string pledge = "Pledge";
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("INSERT INTO donationitems(donorLocation, donationCat, donationAmount, user) values('" + donor_location + "', '" + donation_items + "', '" + amount + "', '" + curr_user +"')", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }

            return Page();
        }
    }
}
