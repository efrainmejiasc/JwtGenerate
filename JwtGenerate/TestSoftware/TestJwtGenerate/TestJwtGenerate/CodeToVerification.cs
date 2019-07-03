using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJwtGenerate
{
    class CodeToVerification
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime VerificationDate { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }
    }
}
