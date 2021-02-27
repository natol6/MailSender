using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;




namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class TextEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1() 
        {
            const string str = "ABC";
            const int key = 1;
            using SecureString sstr = new SecureString();
            sstr.Clear();
            foreach (char c in str)
            {
                sstr.AppendChar((char)(c));
            }
            TextEncoder tenc = new TextEncoder();
            const string expected_str = "BCD";

            var actual_str = tenc.Encode(sstr, key);

            Assert.AreEqual(expected_str, actual_str);
        }
        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            const string str = "BCD";
            const int key = 1;
            const string expected_str = "ABC";
            TextEncoder tenc = new TextEncoder();
            var actual_str = tenc.Decode(str, key);

            Assert.AreEqual(expected_str, actual_str);
        }
        [TestMethod]
        public void DecodeSecure_BCD_to_ABC_with_key_1()
        {
            const string str = "BCD";
            const int key = 1;
            
            const string expected_str = "ABC";
            TextEncoder tenc = new TextEncoder();
            string actual_str = new System.Net.NetworkCredential(string.Empty, tenc.DecodeSecure(str, key)).Password;
            
            Assert.AreEqual(expected_str, actual_str);

        }
    }
}
