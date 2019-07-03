using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Models
{
    //Mobil
    [Table("CodeToVerification")]
    public class CodeToVerification
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password{ get; set; }
        public string Email { get; set; }
        public DateTime  SendDate { get; set; }
        public DateTime VerificationDate { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }
    }
}
