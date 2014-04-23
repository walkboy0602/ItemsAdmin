using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Framework.Model;

namespace Framework.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=shopDB")
        {
        }

        public DbSet<Listing> Listings { get; set; }
        
    }
}
