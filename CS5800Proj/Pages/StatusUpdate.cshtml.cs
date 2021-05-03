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
    public class StatusUpdateModel : PageModel
    {
        public List<String> names = new List<String>();
        [Display(Name = "Select Donation Item to Update:")]
        [BindProperty(SupportsGet = true)]
        public string mod_id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string location { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string amount { get; set; }

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
                            names.Add(sdr["donorLocation"].ToString() + '(' + sdr["donationCat"] + ')' + '-' + sdr["donationAmount"].ToString());
                        }
                    }
                }
            }
        }
        public IActionResult OnPost(string location, string date, string type, string amount)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            string match = "(.+)\\((.+)\\)\\-(.+)";

            MatchCollection matches = Regex.Matches(mod_id, match);
            GroupCollection groups = matches[0].Groups;
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();
                string test2 = groups[0].ToString();
                using var command = new MySqlCommand("UPDATE donationitems SET state = '"+type+"'", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }
            return Page();
        }
    }
}
