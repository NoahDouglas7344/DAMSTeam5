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
	public class MatchingModel : PageModel
	{
        public List<String> names = new List<String>();
        public List<String> donors = new List<String>();

        [Display(Name = "Select the Recipient:")]
        [BindProperty(SupportsGet = true)]
        public string mod_id { get; set; }

        [Display(Name = "Select the Donor:")]
        [BindProperty(SupportsGet = true)]
        public string don_id { get; set; }
        
        public string location { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string amount { get; set; }


        public void OnGet()
		{
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                var command = new MySqlCommand("SELECT * FROM requests", connection);
                {
                    using (MySqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Recipient"].ToString() + '(' + sdr["DisasterLocation"] + ')' + '-' + sdr["DisasterDate"].ToString());
                        }
                    }
                }
                var next_command = new MySqlCommand("SELECT * FROM donationitems", connection);
                {
                    using (MySqlDataReader sdr = next_command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            donors.Add(sdr["donationCat"].ToString() + '(' + sdr["donationAmount"] + ')' + ' ' + sdr["donorLocation"].ToString());
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
		{
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            string recipientMatch = "(.+)\\((.+)\\)\\-(.+)";
            string donorMatch = "(.+)\\((.+)\\)\\ (.+)";

            MatchCollection Rmatches = Regex.Matches(mod_id, recipientMatch);
            GroupCollection Rgroups = Rmatches[0].Groups;

            MatchCollection Dmatches = Regex.Matches(don_id, donorMatch);
            GroupCollection Dgroups = Dmatches[0].Groups;

            int recipientID = 0;
            int donorID = 0;
            int requestQuantity = 0;
            int donationAmount = 0;
            int newQuantity = 0;
            int remainder = 0;

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                var Rcommand = new MySqlCommand("SELECT id FROM requests WHERE DisasterLocation = '" + Rgroups[2] + "' AND DisasterDate = '" + Rgroups[3] + "'", connection);
                {
                    using (MySqlDataReader sdr = Rcommand.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            recipientID = Convert.ToInt32(sdr["id"]);
                        }
                    }
                }

                var Dcommand = new MySqlCommand("SELECT id FROM donationitems WHERE donationCat = '" + Dgroups[1] + "' AND donationAmount = '" + Dgroups[2] + "' AND donorLocation = '" + Dgroups[3] + "'", connection);
				{
                    using (MySqlDataReader sdr = Dcommand.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            donorID = Convert.ToInt32(sdr["id"]);
                        }
                    }
                }

				Rcommand = new MySqlCommand("SELECT RequestQuantity FROM requests WHERE id = '" + recipientID + "'", connection);
                {
                    using (MySqlDataReader sdr = Rcommand.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            requestQuantity = Convert.ToInt32(sdr["RequestQuantity"]);
                        }
                    }
                }

                Dcommand = new MySqlCommand("SELECT donationAmount FROM donationitems WHERE id = '" + donorID + "'", connection);
                {
                    using (MySqlDataReader sdr = Dcommand.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            donationAmount = Convert.ToInt32(sdr["donationAmount"]);
                        }
                    }
                }
                var updateDonorCommand = new MySqlCommand();
                if (requestQuantity >= donationAmount)
                {
                    newQuantity = requestQuantity - donationAmount;
                    remainder = 0;
                    updateDonorCommand = new MySqlCommand("DELETE FROM donationitems WHERE id = '" + donorID + "'", connection);
                }
                if (requestQuantity < donationAmount)
				{
                    newQuantity = 0;
                    remainder = donationAmount - requestQuantity;
                    updateDonorCommand = new MySqlCommand("UPDATE donationitems SET donationAmount = '" + remainder + "' WHERE id = '" + donorID + "'", connection);
				}
                var updateRecipientCommand = new MySqlCommand("UPDATE requests SET RequestQuantity = '" + newQuantity + "' WHERE id = '" + recipientID + "'", connection);
                
                var adapter = updateRecipientCommand.ExecuteNonQuery();
                adapter = updateDonorCommand.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Shipping");
            }


            return Page();
		}
	}
}
