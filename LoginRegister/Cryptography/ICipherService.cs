using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Cryptography
{
   public interface ICipherService
    {
        string Encrypt( string cipherText);
        string Decrypt( string cipherText);
    }
}
