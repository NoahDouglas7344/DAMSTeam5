using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class HistoryModel : PageModel
    {
        public List<String> names = new List<String>();
        public void OnGet()
        {
            String curr_user = "user";

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
            }


        }

    }
}
