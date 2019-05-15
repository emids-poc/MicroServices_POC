using System.Data.Entity;
using UserRegistration.Migrations;

namespace UserRegistration.Models
{
    public class UserRegisterEntites:DbContext
    {
        public UserRegisterEntites():base("name=UserRegistartionEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserRegisterEntites, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<User>().ToTable("User").HasKey(t => new { t.Id });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}