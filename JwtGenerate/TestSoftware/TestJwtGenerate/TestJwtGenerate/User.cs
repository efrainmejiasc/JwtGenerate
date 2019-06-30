﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJwtGenerate
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string DateOfJoing { get { return DateTime.UtcNow.ToString(); } }
        public DateTime FechaRegistro { get; set; }
        public string SignatureApp { get; set; }
    }
}