using LoginRegister.Models;
using LoginRegister.Repository;
using LoginRegister.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using LoginRegister.Cryptography;

namespace LoginRegister.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepository<RegisterModel> _repository;
        private readonly ICipherService _cipher;
        public RegisterService(IRepository<RegisterModel> repository, ICipherService cipher)
        {
            _repository = repository;
            _cipher = cipher;
        }


        public async Task<bool> CreateUserAsync(ViewRegisterModel ViewRegisterModels)
        {

            var IsUniqueMail = await _repository.FetchSingleAsync(x => x.Email == ViewRegisterModels.Email || x.UserName == ViewRegisterModels.UserName);
            if (IsUniqueMail != null)
            {
                return false;
            }
            string protedPasword = _cipher.Encrypt(ViewRegisterModels.Password1);

            var RegisterModel = new RegisterModel
            {
                UserName = ViewRegisterModels.UserName,
                FirstName = ViewRegisterModels.FirstName,
                LastName = ViewRegisterModels.LastName,
                Email = ViewRegisterModels.Email,
                Password1 = protedPasword,
                City = ViewRegisterModels.City,
                State = ViewRegisterModels.State,
                Zip = ViewRegisterModels.Zip,
                IsAgreeTerms = ViewRegisterModels.IsAgreeTerms
            };


            var IsDone = await _repository.CreateUserAsync(RegisterModel);

            if (IsDone == null)
            {
                return false;
            }
            return true;
        }

        public async Task<RegisterModel> Login(ViewLoginModel ViewLoginModel)
        {
            var CryptPass = await _repository.FetchSingleAsync(x => x.Email == ViewLoginModel.Email);
            var CozulmusSifre = _cipher.Decrypt(CryptPass.Password1);


            var acceptLogin = await _repository.FetchSingleAsync(x => x.Email == ViewLoginModel.Email && CozulmusSifre == ViewLoginModel.Password);
            if (acceptLogin == null)
            {
                return null;
            }

            return acceptLogin;


        }

    }




}

