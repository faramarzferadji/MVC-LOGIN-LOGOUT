using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LogingModel.Models
{
    public class User
    {
       
        [Range(100, 999)]
        [Display(Name = " PassWord")]
        public int Password { get; set; }
        [Display(Name = "UserName")]

        public string Username { get; set; }
    }
}


