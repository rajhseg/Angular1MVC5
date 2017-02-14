using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Services
{
    public interface IEncryptService
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
