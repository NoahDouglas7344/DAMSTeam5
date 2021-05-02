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
        public List<String> donations = new List<String>();
        int id;
        public string display = "Donation History:";
        String disasterLocation;
        public void OnGet()
        {
            String curr_user = "user";

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("SELECT * FROM donationitems WHERE user = '" + curr_user + "'", connection);
                {
                    using (MySqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["donationRequest"] is DBNull)
                            {
                                id = -1;
                            }
                            else
                            {
                                id = Convert.ToInt32(sdr["donationRequest"]);
                            }

                            if (! (id == -1)) {

                                using var connection2 = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
                                {
                                    connection2.Open();
                                    using var command2 = new MySqlCommand("SELECT DisasterLocation FROM requests WHERE ID = " + id, connection2);
                                    {

                                        using var reader = command2.ExecuteReader();
                                        {
                                            reader.Read();
                                            disasterLocation = reader.GetValue(0).ToString();
                                            reader.Close();

                                        }
                                    }
                                }
                            }
                            else
                            {
                                disasterLocation = "Pledged";
                            }

                            donations.Add(sdr["donationCat"].ToString() + '(' + sdr["donationAmount"] + ')' + '-' + disasterLocation);
                        }
                    }
                }
            }


        }

    }
}
