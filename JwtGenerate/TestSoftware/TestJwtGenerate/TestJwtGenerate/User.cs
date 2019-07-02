using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJwtGenerate
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FavoriteGame { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string PhoneNumber { get; set; }
        public string ExpiracionToken { get; set; }
        public string SignatureApp { get; set; }
        public string Token { get; set; }
    }
}
