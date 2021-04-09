using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LoginRegister.Cryptography
{
    public class CipherService : ICipherService
    {
        private const string Key = "cut-the-night-with-the-light";
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public CipherService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }
        public string Encrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(cipherText);
        }   

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }


    }
}
