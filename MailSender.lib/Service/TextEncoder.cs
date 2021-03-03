using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public static class TextEncoder
    {
        public static string Encode(SecureString str, int key = 1)
        {
            string pas = new System.Net.NetworkCredential(string.Empty, str).Password;
            return new (pas.Select(c => (char) (c + key)).ToArray());
        }
        public static string Decode(string str, int key = 1)
        {
            //using SecureString sstr = new SecureString();
            //sstr.Clear();
            //foreach(char c in str)
            //{
            //sstr.AppendChar((char)(c - key));
            //}
            //return sstr;
            return new(str.Select(c => (char)(c + key)).ToArray());
        }
    }
}
