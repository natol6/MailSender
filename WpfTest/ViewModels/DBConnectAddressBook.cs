using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WpfTest.Data;
using WpfTest.Models;

namespace WpfTest.ViewModels
{
    class DBConnectAddressBook
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        public static string ConnectionString => Configuration.GetConnectionString("AddressBook");
        private DBAddressBook db = new DBAddressBook(new DbContextOptionsBuilder<DBAddressBook>()
              .UseSqlServer(ConnectionString).Options);
        public bool InternalDB { get; set; }
        public DBConnectAddressBook(bool yes)
        {
            db.Database.EnsureCreated();
            InternalDB = yes;
        }
        public IEnumerable<EmailAddress> DbGetEmailAddress()
        {
            return db.EmailAddresses.ToArray();
        }
        public IEnumerable<EmailAddress> DbGetEmailAddressOther()
        {
            return null; //Надо придумать как добывать из других источников
        }
        public EmailAddress AddBdEmailAddress(EmailAddress em)
        {
            db.EmailAddresses.Add(em);
            db.SaveChanges();
            return db.EmailAddresses.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdEmailAddress(int id)
        {
            EmailAddress em = db.EmailAddresses.FirstOrDefault(d => d.Id == id);
            if (em != null)
            {
                db.EmailAddresses.Remove(em);
                db.SaveChanges();
            }
        }
        public void UpdateBdEmailAddress(EmailAddress em)
        {
            EmailAddress emdb = db.EmailAddresses.FirstOrDefault(d => d.Id == em.Id);
            if (emdb != null)
            {
                emdb.Person = em.Person;
                emdb.Company = em.Company;
                emdb.EMail = em.EMail;
                db.EmailAddresses.Update(emdb);
                db.SaveChanges();
            }

        }
    }
}
