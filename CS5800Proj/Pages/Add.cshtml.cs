using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace CS5800Proj.Pages
{
    public class AddModel : PageModel
    {
        int id;
        public List<String> IDs = new List<String>();
        [BindProperty(SupportsGet = true)]
        public string donor_location { get; set; }
        public string donation_items { get; set; }
        public int amount { get; set; }
        public string donation_request { get; set; }
        public void OnGet()
        {
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command2 = new MySqlCommand("SELECT * FROM requests", connection);
                {
                    using (MySqlDataReader sdr = command2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            IDs.Add(sdr["DisasterLocation"].ToString() + '(' + sdr["DisasterDate"].ToString() + ')');
                        }
                    }
                }
            }
        }
        public IActionResult OnPost(string donor_location, string donation_items, int amount,  string donation_request)
        {
            if (ModelState.IsValid == false || amount < 1)
            {
                return Page();
            }

            string Disastermatch = "(.+)\\((.+)\\)";

            MatchCollection Disastermatches = Regex.Matches(donation_request, Disastermatch);
            GroupCollection Disastergroups = Disastermatches[0].Groups;


            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();


                using var qcommand = new MySqlCommand("select id FROM requests WHERE DisasterLocation = '" + Disastergroups[1] + "' AND DisasterDate = '" + Disastergroups[2] + "'", connection);
                {
                    using (MySqlDataReader sdr = qcommand.ExecuteReader()) 
                {
                    while (sdr.Read())
                    {
                        id = Convert.ToInt32(sdr["id"]);
                    }
                }

            }
                using var command = new MySqlCommand("INSERT INTO donationitems(donorLocation, donationCat, donationAmount, donationRequest) values('" + donor_location + "', '" + donation_items + "', '" + amount + "', " + id + ")", connection);
                    var adapter = command.ExecuteNonQuery();
                    if (adapter > 0)
                        return RedirectToPage("./Home");
                }
            
            return Page();
        }
    }
}
