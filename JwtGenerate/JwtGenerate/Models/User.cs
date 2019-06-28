using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtGenerate.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfJoing { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
