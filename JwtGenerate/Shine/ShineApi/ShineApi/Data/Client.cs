using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Data
{
    public class Client
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
