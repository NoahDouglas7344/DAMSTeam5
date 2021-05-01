using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class ResponseModel : PageModel
    {
        public List<String> requests = new List<String>();
        int id;
        [Display(Name = "Responses:")]
        [BindProperty(SupportsGet = true)]
        public string recipient { get; set; }
        public string type { get; set; }
        public string amount { get; set; }

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
                            requests.Add(sdr["DisasterLocation"].ToString() + '(' + sdr["DisasterDate"].ToString() + ')');
                        }
                    }
                }
            }
        }

        public IActionResult OnPost(string recipient, string type, int amount)
        {
            bool recipientFound = true;

            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                //using var command = new MySqlCommand("SELECT description FROM testEvents;", connection);
                //using var reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //var value = reader.GetValue(0);
                //if (value.ToString() == recipient)
                //{
                //recipientFound = true;
                //}
                //}
                //}
                if (recipientFound)
                {

                    string Disastermatch = "(.+)\\((.+)\\)";

                    MatchCollection Disastermatches = Regex.Matches(recipient, Disastermatch);
                    GroupCollection Disastergroups = Disastermatches[0].Groups;

                    using var qcommand = new MySqlCommand("select id FROM requests WHERE DisasterLocation = '" + Disastergroups[1] + "' AND DisasterDate = '" + Disastergroups[2] + "'", connection);
                    {
                        using (MySqlDataReader sdr = qcommand.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                id = Convert.ToInt32(sdr["ID"]);
                            }
                        }
                    }



                    using var command = new MySqlCommand("SELECT RequestQuantity FROM requests WHERE ID = '" + id + "';", connection);
                    using var reader = command.ExecuteReader();
                    reader.Read();
                    int initValue = (int)reader.GetValue(0);
                    reader.Close();
                    int newVal = initValue - amount;
                    if (newVal > 0)
                    {
                        using var update = new MySqlCommand("UPDATE requests SET RequestQuantity  = " + newVal + " WHERE ID = " + id + ";", connection);
                        update.ExecuteNonQuery();
                    }
                    else
                    {
                        using var update = new MySqlCommand("UPDATE requests SET RequestQuantity  = " + 0 + " WHERE ID = " + id + ";", connection);
                        update.ExecuteNonQuery();
                        using var update2 = new MySqlCommand("UPDATE requests SET Fulfilled  = " + 1 + " WHERE ID = " + id + ";", connection);
                        update2.ExecuteNonQuery();
                    }

                    // var pageModel = new CS5800Proj.Pages.ContactModel();
                    //var httpContext = new DefaultHttpContext();

                    //var info = new MySqlCommand("SELECT address FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                    //var read = info.ExecuteReader();
                    //read.Read();
                    //string address = (string)read.GetValue(0);
                    //read.Close();

                    // info = new MySqlCommand("SELECT email FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                    // read = info.ExecuteReader();
                    // read.Read();
                    //string email = (string)read.GetValue(0);
                    //read.Close();

                    //  info = new MySqlCommand("SELECT phone FROM testEvents WHERE description = '" + recipient + "'; ", connection);
                    //  read = info.ExecuteReader();
                    //  read.Read();
                    //  string phone = (string)read.GetValue(0);
                    //  read.Close();


                    // return RedirectToPage("./Response");//(pageModel.OnPost(address, email, phone));
                    return RedirectToPage("./Home");
                }
                return RedirectToPage("./ResponseFail");
            }
        }
    }
}
