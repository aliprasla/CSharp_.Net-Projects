using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Prasla_Ali_HW6.Models
{
    public enum Major
    {
        Accounting,
        [Description("Business Honors")]
        [Display(Name = "Business Honors")]
        BusinessHonors,
        [Description("Supply Chain Management")]
        [Display(Name = "Supply Chain Management")]
        SupplyChainManagement,
        Finance,
        [Description("International Business")]
        [Display(Name = "International Business")]
        InternationalBusiness,
        Management,
        MIS,
        Marketing,
        STM
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {

        
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        public bool OkToText { get; set; }
        //only mccombs majors
        public Major Major { get; set; }
        public string Password { get; set; }
        
        public virtual List<Event> Events { get; set; }
        



        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Committee> Committees { get; set; }
        //public System.Data.Entity.DbSet <AppUser> AppUsers { get; set; }
        
        public ApplicationDbContext()
            : base("MyDBConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AppRole> AppRoles { get; set; }
    }
}