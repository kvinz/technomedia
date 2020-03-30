using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Entities;
using Technomedia.Domain.Migrations;

namespace Technomedia.Domain.Concrete
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=EFDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>());
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamUser> TeamUsers { get; set; }
    }
}
