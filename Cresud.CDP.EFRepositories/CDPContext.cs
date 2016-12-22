using System.Configuration;
using System.Data.Entity;

namespace Cresud.CDP.EFRepositories
{
    public class CDPContext : DbContext
    {
        public CDPContext() : base( ConfigurationManager.ConnectionStrings["CDP"].ConnectionString)
        {
            Database.SetInitializer<CDPContext>(null);  
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Department>()
            //     .Property(t => t.Name).HasColumnName("DepartmentName")
            //    .ToTable("t_Department");
         
           
        }

        public IDbSet<object> Usuarios { get; set; }
    }
}
