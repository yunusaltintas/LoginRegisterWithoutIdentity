using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Models
{
    public class RegisterModel:BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public short Zip { get; set; }
        public bool IsAgreeTerms { get; set; }


    }
}
