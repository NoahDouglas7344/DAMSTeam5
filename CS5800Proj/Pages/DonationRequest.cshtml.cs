using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CS5800Proj.Pages
{
    public class DonationRequestModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string location { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string amount { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPost(string location, string date, string type, string amount)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            if (location.Length > 2)
            {
                using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
                {
                    connection.Open();

                    using var command = new MySqlCommand("INSERT INTO Requests(DisasterLocation, DisasterDate, Recipient, RequestType, RequestQuantity)" +
                        " VALUES('" + location + "','" + date + "','Noah', '" + type + "', '" + amount + "')", connection);
                    var adapter = command.ExecuteNonQuery();
                    if (adapter > 0)
                        return RedirectToPage("./Home");
                }
            }
            return Page();
        }
    }
}
