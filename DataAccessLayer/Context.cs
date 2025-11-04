using Model;
using System.Data.Entity;

namespace DataAccessLayer
{

    public class Context : DbContext
    {
        public Context() : base("ShipsDB") { }
        public DbSet<Ship> Ships { get; set; }
    }
}
