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
        [BindProperty(SupportsGet = true)]
        public string name { get; set; }
        public string donor_locations { get; set; }
        public string donation_items { get; set; }
        public int amount { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string name, string donor_locations, string donation_items, int amount)
        {
            if (ModelState.IsValid == false || amount < 1)
            {
                return Page();
            }
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("INSERT INTO donationitems(donorLocation, donationCat, donationAmount, donationRequest, name) values('" + donor_locations + "', '" + donation_items + "', '" + amount + "', '" + "Pledge" + "', '" + name + "')", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }

            return Page();
        }
    }
}
