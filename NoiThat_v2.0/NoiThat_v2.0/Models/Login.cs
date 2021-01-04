using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoiThat_v2._0.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string MatKhau { get; set; }
    }
}