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
        private readonly ICipherService _cipherService;
        public RegisterService(IRepository<RegisterModel> repository, ICipherService cipherService)
        {
            _repository = repository;
            _cipherService = cipherService;
        }

        public async Task<bool> ChangePassword(ViewChangePasswordModel ViewChangePasswordModel)
        {
            var result = await _repository.FetchSingleAsync(x => x.Email == ViewChangePasswordModel.Email);
            //var solvedPas = _cipherService.Decrypt(result.Password1);

            bool passwordDone = _cipherService.Decrypt(result.Password1) == ViewChangePasswordModel.OldPasword;
            if (!passwordDone || ViewChangePasswordModel.Password != ViewChangePasswordModel.ConfirmPassword)
            {
                return false;
            }

            string protedPasword = _cipherService.Encrypt(ViewChangePasswordModel.Password);

            result.Password1 = protedPasword;
            _repository.Update(result);
            


            

            return true;
        }

        public async Task<bool> CreateUserAsync(ViewRegisterModel ViewRegisterModels)
        {

            var IsUniqueMail = await _repository.FetchSingleAsync(x => x.Email == ViewRegisterModels.Email || x.UserName == ViewRegisterModels.UserName);
            if (IsUniqueMail != null)
            {
                return false;
            }
            string protedPasword = _cipherService.Encrypt(ViewRegisterModels.Password1);

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
            var CozulmusSifre = _cipherService.Decrypt(CryptPass.Password1);


            var acceptLogin = await _repository.FetchSingleAsync(x => x.Email == ViewLoginModel.Email && CozulmusSifre == ViewLoginModel.Password);
            if (acceptLogin == null)
            {
                return null;
            }

            return acceptLogin;
        }



    }




}

