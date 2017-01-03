﻿using System.Configuration;
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
            modelBuilder.Entity<Empresa>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa")); 
            modelBuilder.Entity<Empresa>().ToTable("Empresa");

            modelBuilder.Entity<Chofer>().HasKey(t => t.Id);
            modelBuilder.Entity<Chofer>().Property(t => t.Id).HasColumnName("IdChofer");
            modelBuilder.Entity<Chofer>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Chofer>().Property(t => t.Apellido).HasColumnName("Apellido");
            modelBuilder.Entity<Chofer>().Property(t => t.Cuit).HasColumnName("Cuit");
            modelBuilder.Entity<Chofer>().Property(t => t.Camion).HasColumnName("Camion");
            modelBuilder.Entity<Chofer>().Property(t => t.Acoplado).HasColumnName("Acoplado");
            modelBuilder.Entity<Chofer>().Property(t => t.FechaCreacion).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Chofer>().Property(t => t.UsuarioCreacion).HasColumnName("UsuarioCreacion");            
            modelBuilder.Entity<Chofer>().Property(t => t.FechaModificacion).HasColumnName("FechaModificacion");            
            modelBuilder.Entity<Chofer>().Property(t => t.UsuarioModificacion).HasColumnName("UsuarioModificacion");            
            modelBuilder.Entity<Chofer>().Property(t => t.Activo).HasColumnName("Activo");            
            modelBuilder.Entity<Chofer>().Property(t => t.EsChoferTransportista).HasColumnName("EsChoferTransportista");            
            modelBuilder.Entity<Chofer>().Property(t => t.Domicilio).HasColumnName("Domicilio");            
            modelBuilder.Entity<Chofer>().Property(t => t.Marca).HasColumnName("Marca");                        
            modelBuilder.Entity<Chofer>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Chofer>().ToTable("Chofer");

      
        }

        public IDbSet<Pais> Paises { get; set; }
        public IDbSet<Empresa> Empresas { get; set; }
        public IDbSet<GrupoEmpresa> GrupoEmpresas { get; set; }
        public IDbSet<Chofer> Choferes { get; set; }
    }
}
