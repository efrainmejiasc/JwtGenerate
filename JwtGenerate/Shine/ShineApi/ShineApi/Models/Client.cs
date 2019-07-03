using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Models
{
    [Table("Client")]
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }
        public string FavoriteGame { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender{ get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool RegisteredStatus { get; set; }
    }
}
