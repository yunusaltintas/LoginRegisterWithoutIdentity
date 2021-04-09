using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public interface ISmtpService
    {

        Task ForgetPasswordAsync(string Email);

    }
}
