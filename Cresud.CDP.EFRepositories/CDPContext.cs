using System.Configuration;
using System.Data.Entity;
using Cresud.CDP.Entities;

namespace Cresud.CDP.EFRepositories
{
    public class CDPContext : DbContext
    {
        public CDPContext()
            : base(ConfigurationManager.ConnectionStrings["CDP"].ConnectionString)
        {
            Database.SetInitializer<CDPContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pais>().HasKey(t => t.Id);
            modelBuilder.Entity<Pais>().Property(t => t.Id).HasColumnName("IdPais");
            modelBuilder.Entity<Pais>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Pais>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Pais>().Ignore(t => t.CreateDate);
            modelBuilder.Entity<Pais>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<Pais>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Pais>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<Pais>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Pais>().ToTable("Pais");

            modelBuilder.Entity<GrupoEmpresa>().HasKey(t => t.Id);
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Id).HasColumnName("IdGrupoEmpresa");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.IdApp).HasColumnName("IdApp");
            modelBuilder.Entity<GrupoEmpresa>().HasOptional(e => e.Pais).WithMany().Map(x => x.MapKey("IdPais"));
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.Enabled);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.CreateDate);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.DeletedBy);
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
            modelBuilder.Entity<Empresa>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Empresa>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Empresa>().Ignore(t => t.CreateDate);
            modelBuilder.Entity<Empresa>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<Empresa>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Empresa>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<Empresa>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Empresa>().ToTable("Empresa");

            modelBuilder.Entity<Chofer>().HasKey(t => t.Id);
            modelBuilder.Entity<Chofer>().Property(t => t.Id).HasColumnName("IdChofer");
            modelBuilder.Entity<Chofer>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Chofer>().Property(t => t.Apellido).HasColumnName("Apellido");
            modelBuilder.Entity<Chofer>().Property(t => t.Cuit).HasColumnName("Cuit");
            modelBuilder.Entity<Chofer>().Property(t => t.Camion).HasColumnName("Camion");
            modelBuilder.Entity<Chofer>().Property(t => t.Acoplado).HasColumnName("Acoplado");
            modelBuilder.Entity<Chofer>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Chofer>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Chofer>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Chofer>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Chofer>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Chofer>().Property(t => t.EsChoferTransportista).HasColumnName("EsChoferTransportista");
            modelBuilder.Entity<Chofer>().Property(t => t.Domicilio).HasColumnName("Domicilio");
            modelBuilder.Entity<Chofer>().Property(t => t.Marca).HasColumnName("Marca");
            modelBuilder.Entity<Chofer>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Chofer>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Chofer>().ToTable("Chofer");

            modelBuilder.Entity<Grano>().HasKey(t => t.Id);
            modelBuilder.Entity<Grano>().Property(t => t.Id).HasColumnName("IdGrano");
            modelBuilder.Entity<Grano>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Grano>().Property(t => t.IdMaterialSap).HasColumnName("IdMaterialSap");
            modelBuilder.Entity<Grano>().Property(t => t.SujetoALote).HasColumnName("SujetoALote");
            modelBuilder.Entity<Grano>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Grano>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Grano>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Grano>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Grano>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Grano>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.EspecieAfip).WithMany().Map(x => x.MapKey("IdEspecieAfip"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.TipoGranoAfip).WithMany().Map(x => x.MapKey("IdTipoGrano"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.CosechaAfip).WithMany().Map(x => x.MapKey("IdCosechaAfip"));
            modelBuilder.Entity<Grano>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Grano>().ToTable("Grano");

            modelBuilder.Entity<Especie>().HasKey(t => t.Id);
            modelBuilder.Entity<Especie>().Property(t => t.Id).HasColumnName("IdEspecie");
            modelBuilder.Entity<Especie>().Property(t => t.Codigo).HasColumnName("Codigo");
            modelBuilder.Entity<Especie>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Especie>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Especie>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.Enabled);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<GrupoEmpresa>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Especie>().ToTable("Especie");

            modelBuilder.Entity<TipoGrano>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoGrano>().Property(t => t.Id).HasColumnName("IdTipoGrano");
            modelBuilder.Entity<TipoGrano>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.Enabled);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.CreateDate);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<TipoGrano>().ToTable("TipoGrano");

            modelBuilder.Entity<Cosecha>().HasKey(t => t.Id);
            modelBuilder.Entity<Cosecha>().Property(t => t.Id).HasColumnName("IdCosecha");
            modelBuilder.Entity<Cosecha>().Property(t => t.Codigo).HasColumnName("Codigo");
            modelBuilder.Entity<Especie>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Especie>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.Enabled);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<TipoGrano>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Cosecha>().ToTable("Cosecha");
        }

        public IDbSet<Pais> Paises { get; set; }
        public IDbSet<Empresa> Empresas { get; set; }
        public IDbSet<GrupoEmpresa> GrupoEmpresas { get; set; }
        public IDbSet<Chofer> Choferes { get; set; }
        public IDbSet<Grano> Granos { get; set; }
        public IDbSet<Especie> Especies { get; set; }
        public IDbSet<TipoGrano> TiposGrano { get; set; }
        public IDbSet<Cosecha> Cosechas { get; set; }
    }
}
