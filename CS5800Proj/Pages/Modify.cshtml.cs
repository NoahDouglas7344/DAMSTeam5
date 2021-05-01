using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace CS5800Proj.Pages
{
    public class ModifyModel : PageModel
    {
        public List<String> names = new List<String>();
        public List<String> IDs = new List<String>();
        int id;
        [Display(Name = "Items to Modify:")]
        [BindProperty(SupportsGet = true)]
        public string mod_id { get; set; }
        public string donor_location { get; set; }
        public string donation_items { get; set; }
        public int amount { get; set; }
        public string donation_request { get; set; }
        public void OnGet()
        {
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT * FROM donationitems", connection);
                {
                    using (MySqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["donationCat"].ToString() + '(' + sdr["donationAmount"] + ')' + '-' + sdr["donorLocation"].ToString());
                        }
                    }
                }

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

        public IActionResult OnPost(string mod_id, string donor_location, string donation_items, int amount, string donation_request)
        {
            if (ModelState.IsValid == false || amount < 1)
            {
                return Page();
            }

            string match = "(.+)\\((.+)\\)\\-(.+)";

            MatchCollection matches = Regex.Matches(mod_id, match);
            GroupCollection groups = matches[0].Groups;


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

                using var command = new MySqlCommand("UPDATE donationitems SET donorLocation = '" + donor_location + "', donationCat = '" + donation_items + "', donationAmount = " + amount + ", donationRequest = " + id + " WHERE donationCat = '" + groups[1] + "'" + " AND donationAmount = " + groups[2] + " AND donorLocation = '" + groups[3] + "'", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }


            return Page();
        }
    }
}
