using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class ModifyModel : PageModel
    {
        public List<String> names = new List<String>();
        [Display(Name = "Items to Modify:")]
        [BindProperty(SupportsGet = true)]
        public string mod_id { get; set; }
        public string donor_locations { get; set; }
        public string donation_items { get; set; }
        public int amount { get; set; }
        public string donation_request { get; set; }
        public void OnGet()
        {
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT name FROM donationitems", connection);
                {
                    using (MySqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["name"].ToString());
                        }
                    }
                }
            }
        }

        public IActionResult OnPost(string mod_id, string donor_locations, string donation_items, int amount, string donation_request)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("UPDATE donationitems SET donorLocation = '" + donor_locations + "', donationCat = '" + donation_items + "', donationAmount = " + amount + ", donationRequest = '" + donation_request + "' WHERE name = '" + mod_id + "'", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }


            return Page();
        }
    }
}
