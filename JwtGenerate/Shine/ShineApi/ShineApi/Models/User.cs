using ShineApi.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ExpiracionToken { get; set; }
        public string  FechaRegistro { get; set; }
        public string SignatureApp { get; set; }
        public string Token { get; set; }
    }
}
