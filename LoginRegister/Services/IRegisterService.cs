using LoginRegister.Models;
using LoginRegister.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public interface IRegisterService
    {
        Task<bool> CreateUserAsync(ViewRegisterModel ViewRegisterModels);
        Task<RegisterModel> Login(ViewLoginModel viewLoginModel);
    }

}
