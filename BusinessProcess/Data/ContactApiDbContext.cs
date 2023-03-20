using BusinessProcess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BusinessProcess.Data
{
    public class ContactApiDbContext : DbContext
    {
        public ContactApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ContactsModel> Contacts { get; set; }
    }
}
