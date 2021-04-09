using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.ViewModels
{
    public class ViewRegisterModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }     
        public string Email { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public short Zip { get; set; }
        public bool IsAgreeTerms { get; set; }

    }
}
