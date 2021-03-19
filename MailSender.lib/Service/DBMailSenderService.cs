using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MailSender.lib.Models;
using MailSender.lib.Data;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Service
{
    public class DBMailSenderService : IDataBaseMailSender
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        public static string ConnectionString => Configuration.GetConnectionString("Default");
        private DBMailSender db = new DBMailSender(new DbContextOptionsBuilder<DBMailSender>()
            .UseSqlServer(ConnectionString).Options);
        public DBMailSenderService()
        {
            db.Database.EnsureCreated();
        }
        public IEnumerable<SmtpAccount> DBGetSmtpAccounts() => db.SmtpAccounts.ToArray();

        public IEnumerable<SmtpServer> DBGetSmtpServers() => db.SmtpServers;
        
        public IEnumerable<MessagePattern> DBGetMessagePatterns() => db.MessagePatterns;
        
        public IEnumerable<EmailAddress> DBGetEmailAddresses() => db.EmailAddresses;
        
        public IEnumerable<MessageSendContainer> DBGetMessageSendContainers() => db.MessageSendContainers;
        
        public MessagePattern AddDb(MessagePattern mp)
        {
            db.MessagePatterns.Add(mp);
            db.SaveChanges();
            return db.MessagePatterns.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteDb(MessagePattern mp)
        {
            MessagePattern mpdb = db.MessagePatterns.FirstOrDefault(d => d.Id == mp.Id);
            if (mpdb != null)
            {
                db.MessagePatterns.Remove(mpdb);
                db.SaveChanges();
            }
        }
        public void UpdateDb(MessagePattern mp)
        {
            MessagePattern mpdb = db.MessagePatterns.FirstOrDefault(d => d.Id == mp.Id);
            if (mpdb != null)
            {
                mpdb.Subject = mp.Subject;
                mpdb.Body = mp.Body;
                db.MessagePatterns.Update(mpdb);
                db.SaveChanges();
            }

        }
        public SmtpAccount AddDb(SmtpAccount ac)
        {
            db.SmtpAccounts.Add(ac);
            db.SaveChanges();
            return db.SmtpAccounts.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteDb(SmtpAccount ac)
        {
            SmtpAccount acdb = db.SmtpAccounts.FirstOrDefault(d => d.Id == ac.Id);
            if (acdb != null)
            {
                db.SmtpAccounts.Remove(acdb);
                db.SaveChanges();
            }
        }
        public void UpdateDb(SmtpAccount ac)
        {
            SmtpAccount acdb = db.SmtpAccounts.FirstOrDefault(d => d.Id == ac.Id);
            if (acdb != null)
            {
                acdb.AccountEmail = ac.AccountEmail;
                acdb.Password = ac.Password;
                acdb.Person_Company = ac.Person_Company;
                db.SmtpAccounts.Update(acdb);
                db.SaveChanges();
            }

        }
        public SmtpServer AddDb(SmtpServer serv)
        {
            db.SmtpServers.Add(serv);
            db.SaveChanges();
            return db.SmtpServers.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteDb(SmtpServer ms)
        {
            SmtpServer msdb = db.SmtpServers.FirstOrDefault(d => d.Id == ms.Id);
            if (msdb != null)
            {
                db.SmtpServers.Remove(msdb);
                db.SaveChanges();
            }
        }
        public void UpdateDb(SmtpServer serv)
        {
            SmtpServer servdb = db.SmtpServers.FirstOrDefault(d => d.Id == serv.Id);
            if (servdb != null)
            {
                servdb.Title = serv.Title;
                servdb.SmtpServ = serv.SmtpServ;
                servdb.Port = serv.Port;
                servdb.UseSSL = serv.UseSSL;
                db.SmtpServers.Update(servdb);
                db.SaveChanges();
            }

        }
        public MessageSendContainer AddDb(MessageSendContainer msc)
        {
            db.MessageSendContainers.Add(msc);
            db.SaveChanges();
            return db.MessageSendContainers.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteDb(MessageSendContainer msc)
        {
            MessageSendContainer mscdb = db.MessageSendContainers.FirstOrDefault(d => d.Id == msc.Id);
            if (mscdb != null)
            {
                db.MessageSendContainers.Remove(mscdb);
                db.SaveChanges();
            }
        }
        public void UpdateDb(MessageSendContainer msc)
        {
            MessageSendContainer mscdb = db.MessageSendContainers.FirstOrDefault(d => d.Id == msc.Id);
            if (mscdb != null)
            {
                mscdb.SmtpServerUse = msc.SmtpServerUse;
                mscdb.PortUse = msc.PortUse;
                mscdb.SSLUse = msc.SSLUse;
                mscdb.SmtpAccountEmailUse = msc.SmtpAccountEmailUse;
                mscdb.SmtpAccountPasswordUse = msc.SmtpAccountPasswordUse;
                mscdb.SmtpAccountPerson_CompanyUse = msc.SmtpAccountPerson_CompanyUse;
                mscdb.EmailAddressesTo = msc.EmailAddressesTo;
                mscdb.Subject = msc.Subject;
                mscdb.Body = msc.Body;
                mscdb.SendDate = msc.SendDate;
                mscdb.Status = msc.Status;
                db.MessageSendContainers.Update(mscdb);
                db.SaveChanges();
            }

        }
        public EmailAddress AddDb(EmailAddress em)
        {
            db.EmailAddresses.Add(em);
            db.SaveChanges();
            return db.EmailAddresses.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteDb(EmailAddress em)
        {
            EmailAddress emdb = db.EmailAddresses.FirstOrDefault(d => d.Id == em.Id);
            if (emdb != null)
            {
                db.EmailAddresses.Remove(emdb);
                db.SaveChanges();
            }
        }
        public void UpdateDb(EmailAddress em)
        {
            EmailAddress emdb = db.EmailAddresses.FirstOrDefault(d => d.Id == em.Id);
            if (emdb != null)
            {
                emdb.Person_Company = em.Person_Company;
                emdb.Email = em.Email;
                db.EmailAddresses.Update(emdb);
                db.SaveChanges();
            }

        }
    }
}
