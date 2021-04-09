using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.ViewModels
{
    public class ViewChangePasswordModel
    {
        public string Email { get; set; }
        public string OldPasword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
