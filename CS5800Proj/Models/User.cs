using System;
using System.ComponentModel.DataAnnotations;

namespace CS5800Proj.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
    }
}
