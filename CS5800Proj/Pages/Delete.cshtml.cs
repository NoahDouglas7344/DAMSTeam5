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
    public class DeleteModel : PageModel
    {

        public List<String> names = new List<String>();
        [Display(Name = "Items:")]

        public int del_id { get; set; }
        //public string del_items { get; set; }
        

        

        public void OnGet()
        {
            // List<String> names = new List<String>();
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

        public IActionResult OnPost()
        {
            string id = Request.Form["del_id"];
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            using var connection = new MySqlConnection("server=localhost;port=3306;database=testDB;user=root;password=CS5800Team5");
            {
                connection.Open();

                using var command = new MySqlCommand("DELETE FROM donationitems where name = '" + id + "'", connection);
                var adapter = command.ExecuteNonQuery();
                if (adapter > 0)
                    return RedirectToPage("./Home");
            }

            return Page();
        }
    }
}
