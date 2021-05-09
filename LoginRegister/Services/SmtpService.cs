using LoginRegister.Cryptography;
using LoginRegister.Models;
using LoginRegister.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public class SmtpService : ISmtpService
    {

        private readonly IRepository<RegisterModel> _repository;
        private readonly ICipherService _cipherService;
        public SmtpService(IRepository<RegisterModel> repository, ICipherService cipherService)
        {
            _repository = repository;
            _cipherService = cipherService;

        }


        public async Task ForgetPasswordAsync(string Email)
        {


            var cryptPas = await _repository.FetchSingleAsync(x => x.Email == Email);
            var solvedPas =_cipherService.Decrypt(cryptPas.Password1);


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;


            NetworkCredential kullanicibilgi = new NetworkCredential("denemeyea@gmail.com", "Pasword");
            smtp.Credentials = kullanicibilgi;


            MailAddress gonderen = new MailAddress("denemeyea@gmail.com");
           
            MailAddress alici = new MailAddress(Email);
           
            MailMessage mail = new MailMessage(gonderen, alici);
           
            mail.Subject = "Şifre Hatırlatma";
            mail.Body = "şifreniz="+solvedPas;
            mail.IsBodyHtml = true;
            
            smtp.Send(mail);
            



           
        }
    }
}
