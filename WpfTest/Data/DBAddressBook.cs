using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfTest.Models;

namespace WpfTest.Data
{
    class DBAddressBook : DbContext
    {
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DBAddressBook(DbContextOptions<DBAddressBook> options) : base(options)
        {

        }
    }
}
