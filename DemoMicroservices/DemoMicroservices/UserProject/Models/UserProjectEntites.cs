using System.Data.Entity;
using UserProject.Migrations;

namespace UserProject.Models
{
    public class UserProjectEntites:DbContext
    {
        public UserProjectEntites():base("name=UserProjectEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserProjectEntites, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<User>().ToTable("User").HasKey(t => new { t.Id });
            modelBuilder.Entity<Project>().ToTable("Project").HasKey(t => new { t.Id });
            modelBuilder.Entity<Project_UserMapping>().ToTable("Project_UserMapping").HasKey(t => new { t.Id });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Project_UserMapping> project_Users { get; set; }

    }
}