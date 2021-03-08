using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Service
{
    public class TextEncoder: ITextEncoder
    {
        public string Encode(SecureString str, int key = 1)
        {
            string pas = new System.Net.NetworkCredential(string.Empty, str).Password;
            return new (pas.Select(c => (char) (c + key)).ToArray());
        }
        public string Decode(string str, int key = 1)
        {
            return new(str.Select(c => (char)(c - key)).ToArray());
        }
        public SecureString DecodeSecure(string str, int key = 1)
        {
            SecureString sstr = new SecureString();
            sstr.Clear();
            foreach(char c in str)
            {
            sstr.AppendChar((char)(c - key));
            }
            return sstr;
            
        }
    }
}
