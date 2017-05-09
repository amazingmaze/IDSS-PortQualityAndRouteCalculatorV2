using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IDSS_RouteAndQualityForShippers.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Port> Ports { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}