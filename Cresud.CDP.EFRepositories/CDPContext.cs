using System.Configuration;
using System.Data.Entity;
using Cresud.CDP.Entities;

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
            modelBuilder.Entity<Pais>().HasKey(t => t.Id);
            modelBuilder.Entity<Pais>().Property(t => t.Id).HasColumnName("IdPais");
            modelBuilder.Entity<Pais>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Pais>().ToTable("Pais");

            modelBuilder.Entity<GrupoEmpresa>().HasKey(t => t.Id);
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Id).HasColumnName("IdGrupoEmpresa");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.IdApp).HasColumnName("IdApp");            
            modelBuilder.Entity<GrupoEmpresa>().HasOptional(e => e.Pais).WithMany().Map(x => x.MapKey("IdPais")); 
            modelBuilder.Entity<GrupoEmpresa>().ToTable("GrupoEmpresa");

            modelBuilder.Entity<Empresa>().HasKey(t => t.Id);
            modelBuilder.Entity<Empresa>().Property(t => t.Id).HasColumnName("IdEmpresa");
            modelBuilder.Entity<Empresa>().Property(t => t.IdCliente).HasColumnName("IdCliente");
            modelBuilder.Entity<Empresa>().Property(t => t.IdSapCanalExpor).HasColumnName("IdSapCanalExpor");
            modelBuilder.Entity<Empresa>().Property(t => t.IdSapCanalLocal).HasColumnName("IdSapCanalLocal");
            modelBuilder.Entity<Empresa>().Property(t => t.IdSapMoneda).HasColumnName("IdSapMoneda");
            modelBuilder.Entity<Empresa>().Property(t => t.IdSapOrganizacionDeVenta).HasColumnName("IdSapOrganizacionDeVenta");
            modelBuilder.Entity<Empresa>().Property(t => t.IdSapSector).HasColumnName("IdSapSector");
            modelBuilder.Entity<Empresa>().Property(t => t.SapId).HasColumnName("Sap_Id");            
            modelBuilder.Entity<Empresa>().HasOptional<GrupoEmpresa>(s => s.GrupoEmpresa);
            modelBuilder.Entity<Empresa>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa")); 
            modelBuilder.Entity<Empresa>().ToTable("Empresa");
                    
        }

        public IDbSet<Pais> Paises { get; set; }
        public IDbSet<Empresa> Empresas { get; set; }
        public IDbSet<GrupoEmpresa> GrupoEmpresas { get; set; }
    }
}
