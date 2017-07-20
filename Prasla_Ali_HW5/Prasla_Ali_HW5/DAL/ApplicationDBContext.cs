using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Prasla_Ali_HW5.Models;

namespace Prasla_Ali_HW5.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MyDBConnection") { }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //insert model classes here
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
    }
}