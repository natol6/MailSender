using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace MailSender.lib.Interfaces
{
    public interface ITextEncoder
    {
        public string Encode(SecureString str, int key = 1);
        public string Decode(string str, int key = 1);
        public SecureString DecodeSecure(string str, int key = 1);
    }
}
