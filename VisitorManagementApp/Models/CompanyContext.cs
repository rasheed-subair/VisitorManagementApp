using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VisitorManagementApp.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("CompanyContext")
        {
        }

        public DbSet<Admin> AdminTable { get; set; }
        public DbSet<Staff> StaffTable { get; set; }
        public DbSet<Visitor> VisitorTable { get; set; }
        public DbSet<Keycode> KeycodeTable { get; set; }
    }
}