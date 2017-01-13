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
            modelBuilder.Entity<Pais>().ToTable("Pais");

            modelBuilder.Entity<Provincia>().HasKey(t => t.Id);
            modelBuilder.Entity<Provincia>().Property(t => t.Id).HasColumnName("Codigo");
            modelBuilder.Entity<Provincia>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Provincia>().Property(t => t.PaisId).HasColumnName("IdPais");
            modelBuilder.Entity<Provincia>().ToTable("Provincia");

            modelBuilder.Entity<Localidad>().HasKey(t => new { t.Id, t.ProvinciaId });
            modelBuilder.Entity<Localidad>().Property(t => t.Id).HasColumnName("Codigo");
            modelBuilder.Entity<Localidad>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Localidad>().Property(t => t.ProvinciaId).HasColumnName("IdProvincia");
            modelBuilder.Entity<Localidad>().ToTable("Localidad");

            modelBuilder.Entity<GrupoEmpresa>().HasKey(t => t.Id);
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Id).HasColumnName("IdGrupoEmpresa");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<GrupoEmpresa>().Property(t => t.IdApp).HasColumnName("IdApp");
            modelBuilder.Entity<GrupoEmpresa>().HasOptional(e => e.Pais).WithMany().Map(x => x.MapKey("IdPais"));
            modelBuilder.Entity<GrupoEmpresa>().ToTable("GrupoEmpresa");

            modelBuilder.Entity<TipoDocumentoSap>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoDocumentoSap>().Property(t => t.Id).HasColumnName("IdTipoDocumentoSAP");
            modelBuilder.Entity<TipoDocumentoSap>().Property(t => t.SapId).HasColumnName("SAP_Id");
            modelBuilder.Entity<TipoDocumentoSap>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<TipoDocumentoSap>().ToTable("TipoDocumentoSap");

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
            modelBuilder.Entity<Chofer>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Chofer>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Chofer>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Chofer>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Chofer>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Chofer>().Property(t => t.EsChoferTransportista).HasColumnName("EsChoferTransportista");
            modelBuilder.Entity<Chofer>().Property(t => t.Domicilio).HasColumnName("Domicilio");
            modelBuilder.Entity<Chofer>().Property(t => t.Marca).HasColumnName("Marca");
            modelBuilder.Entity<Chofer>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Chofer>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
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
            modelBuilder.Entity<Grano>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Grano>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.EspecieAfip).WithMany().Map(x => x.MapKey("IdEspecieAfip"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.TipoGranoAfip).WithMany().Map(x => x.MapKey("IdTipoGrano"));
            modelBuilder.Entity<Grano>().HasOptional(e => e.CosechaAfip).WithMany().Map(x => x.MapKey("IdCosechaAfip"));
            modelBuilder.Entity<Grano>().ToTable("Grano");

            modelBuilder.Entity<Especie>().HasKey(t => t.Id);
            modelBuilder.Entity<Especie>().Property(t => t.Id).HasColumnName("IdEspecie");
            modelBuilder.Entity<Especie>().Property(t => t.Codigo).HasColumnName("Codigo");
            modelBuilder.Entity<Especie>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Especie>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Especie>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Especie>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<Especie>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Especie>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<Especie>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Especie>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Especie>().ToTable("Especie");

            modelBuilder.Entity<TipoGrano>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoGrano>().Property(t => t.Id).HasColumnName("IdTipoGrano");
            modelBuilder.Entity<TipoGrano>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<TipoGrano>().ToTable("TipoGrano");

            modelBuilder.Entity<Cosecha>().HasKey(t => t.Id);
            modelBuilder.Entity<Cosecha>().Property(t => t.Id).HasColumnName("IdCosecha");
            modelBuilder.Entity<Cosecha>().Property(t => t.Codigo).HasColumnName("Codigo");
            modelBuilder.Entity<Cosecha>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Cosecha>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Cosecha>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<Cosecha>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Cosecha>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<Cosecha>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Cosecha>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));
            modelBuilder.Entity<Cosecha>().ToTable("Cosecha");

            modelBuilder.Entity<Cliente>().HasKey(t => new { t.Id, t.IdSapOrganizacionDeVenta });
            modelBuilder.Entity<Cliente>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<Cliente>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<Cliente>().Property(t => t.NombreFantasia).HasColumnName("NombreFantasia");
            modelBuilder.Entity<Cliente>().Property(t => t.Cuit).HasColumnName("Cuit");
            modelBuilder.Entity<Cliente>().Property(t => t.IdTipoDocumentoSap).HasColumnName("IdTipoDocumentoSAP");
            modelBuilder.Entity<Cliente>().Property(t => t.IdClientePrincipal).HasColumnName("IdClientePrincipal");
            modelBuilder.Entity<Cliente>().Property(t => t.Calle).HasColumnName("Calle");
            modelBuilder.Entity<Cliente>().Property(t => t.Numero).HasColumnName("Numero");
            modelBuilder.Entity<Cliente>().Property(t => t.Dto).HasColumnName("Dto");
            modelBuilder.Entity<Cliente>().Property(t => t.Piso).HasColumnName("Piso");
            modelBuilder.Entity<Cliente>().Property(t => t.Cp).HasColumnName("Cp");
            modelBuilder.Entity<Cliente>().Property(t => t.Poblacion).HasColumnName("Poblacion");
            modelBuilder.Entity<Cliente>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Cliente>().Property(t => t.GrupoComercial).HasColumnName("GrupoComercial");
            modelBuilder.Entity<Cliente>().Property(t => t.ClaveGrupo).HasColumnName("ClaveGrupo");
            modelBuilder.Entity<Cliente>().Property(t => t.Tratamiento).HasColumnName("Tratamiento");
            modelBuilder.Entity<Cliente>().Property(t => t.DescripcionGe).HasColumnName("DescripcionGe");
            modelBuilder.Entity<Cliente>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Cliente>().Property(t => t.EsProspecto).HasColumnName("EsProspecto");
            modelBuilder.Entity<Cliente>().Property(t => t.IdSapOrganizacionDeVenta).HasColumnName("IdSapOrganizacionDeVenta");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");

            modelBuilder.Entity<Establecimiento>().HasKey(t => t.Id);
            modelBuilder.Entity<Establecimiento>().Property(t => t.Id).HasColumnName("IdEstablecimiento");
            modelBuilder.Entity<Establecimiento>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.Direccion).HasColumnName("Direccion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.IdAlmacenSap).HasColumnName("IDAlmacenSAP");
            modelBuilder.Entity<Establecimiento>().Property(t => t.IdCentroSap).HasColumnName("IDCentroSAP");
            modelBuilder.Entity<Establecimiento>().Property(t => t.RecorridoEstablecimiento).HasColumnName("RecorridoEstablecimiento");
            modelBuilder.Entity<Establecimiento>().Property(t => t.IdCEBE).HasColumnName("IDCEBE");
            modelBuilder.Entity<Establecimiento>().Property(t => t.IdExpedicion).HasColumnName("IDExpedicion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.EstablecimientoAfip).HasColumnName("EstablecimientoAfip");
            modelBuilder.Entity<Establecimiento>().Property(t => t.AsociaCartaDePorte).HasColumnName("AsociaCartaDePorte");
            modelBuilder.Entity<Establecimiento>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<Establecimiento>().Property(t => t.LocalidadId).HasColumnName("Localidad");
            modelBuilder.Entity<Establecimiento>().Property(t => t.InterlocutorDestinatarioId).HasColumnName("IdInterlocutorDestinatario");
            modelBuilder.Entity<Establecimiento>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Establecimiento>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Establecimiento>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Establecimiento>().HasOptional(e => e.Provincia).WithMany().Map(x => x.MapKey("Provincia"));
            modelBuilder.Entity<Establecimiento>().ToTable("Establecimiento");

            modelBuilder.Entity<Proveedor>().HasKey(t => t.Id);
            modelBuilder.Entity<Proveedor>().Property(t => t.Id).HasColumnName("IdProveedor");
            modelBuilder.Entity<Proveedor>().Property(t => t.SapId).HasColumnName("Sap_Id");
            modelBuilder.Entity<Proveedor>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Proveedor>().Property(t => t.NumeroDocumento).HasColumnName("NumeroDocumento");
            modelBuilder.Entity<Proveedor>().Property(t => t.Calle).HasColumnName("Calle");
            modelBuilder.Entity<Proveedor>().Property(t => t.Piso).HasColumnName("Piso");
            modelBuilder.Entity<Proveedor>().Property(t => t.Departamento).HasColumnName("Departamento");
            modelBuilder.Entity<Proveedor>().Property(t => t.Numero).HasColumnName("Numero");
            modelBuilder.Entity<Proveedor>().Property(t => t.Cp).HasColumnName("CP");
            modelBuilder.Entity<Proveedor>().Property(t => t.Ciudad).HasColumnName("Ciudad");
            modelBuilder.Entity<Proveedor>().Property(t => t.Pais).HasColumnName("Pais");
            modelBuilder.Entity<Proveedor>().Property(t => t.Domicilio).HasColumnName("Domicilio");
            modelBuilder.Entity<Proveedor>().Property(t => t.EsProspecto).HasColumnName("EsProspecto");
            modelBuilder.Entity<Proveedor>().Property(t => t.IdSapOrganizacionDeVenta).HasColumnName("IdSapOrganizacionDeVenta");
            modelBuilder.Entity<Proveedor>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Proveedor>().Property(t => t.Enabled).HasColumnName("Activo");
            modelBuilder.Entity<Proveedor>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Proveedor>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<Proveedor>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<Proveedor>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Proveedor>().HasOptional(e => e.TipoDocumento).WithMany().Map(x => x.MapKey("IdTipoDocumentoSAP"));
            modelBuilder.Entity<Proveedor>().ToTable("Proveedor");

            modelBuilder.Entity<Solicitud>().HasKey(t => t.Id);
            modelBuilder.Entity<Solicitud>().Property(t => t.Id).HasColumnName("IdSolicitud");
            modelBuilder.Entity<Solicitud>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<Solicitud>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Solicitud>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Solicitud>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Solicitud>().ToTable("Solicitudes");

            modelBuilder.Entity<LoteCartaPorte>().HasKey(t => t.Id);
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Id).HasColumnName("IdLoteCartasDePorte");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Desde).HasColumnName("Desde");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Hasta).HasColumnName("Hasta");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.EstablecimientoOrigen).HasColumnName("EstablecimientoOrigen");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaVencimiento).HasColumnName("FechaVencimiento");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Sucursal).HasColumnName("Sucursal");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.PuntoEmision).HasColumnName("PuntoEmision");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaDesde).HasColumnName("FechaDesde");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaHasta).HasColumnName("FechaHasta");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.HabilitacionNumero ).HasColumnName("HabilitacionNum");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");            
            modelBuilder.Entity<LoteCartaPorte>().Ignore(t => t.UpdateDate);            
            modelBuilder.Entity<LoteCartaPorte>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<LoteCartaPorte>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<LoteCartaPorte>().Ignore(t => t.Enabled);
            modelBuilder.Entity<LoteCartaPorte>().HasOptional(e => e.GrupoEmpresa).WithMany().Map(x => x.MapKey("IdGrupoEmpresa"));            
            modelBuilder.Entity<LoteCartaPorte>().ToTable("LoteCartasDePorte");

            modelBuilder.Entity<CartaDePorte>().HasKey(t => t.Id);
            modelBuilder.Entity<CartaDePorte>().Property(t => t.Id).HasColumnName("IdCartaDePorte");
            modelBuilder.Entity<CartaDePorte>().Property(t => t.NumeroCartaDePorte).HasColumnName("NumeroCartaDePorte");
            modelBuilder.Entity<CartaDePorte>().Property(t => t.NumeroCee).HasColumnName("NumeroCee");            
            modelBuilder.Entity<CartaDePorte>().Property(t => t.Estado).HasColumnName("Estado");
            modelBuilder.Entity<CartaDePorte>().Property(t => t.FechaReserva).HasColumnName("FechaReserva");
            modelBuilder.Entity<CartaDePorte>().Property(t => t.UsuarioReserva).HasColumnName("UsuarioReserva");
            modelBuilder.Entity<CartaDePorte>().Property(t => t.GrupoEmpresaId).HasColumnName("IdGrupoEmpresa");
            modelBuilder.Entity<CartaDePorte>().HasRequired(e => e.Lote).WithMany(d => d.CartasDePorte).Map(x => x.MapKey("IdLoteLoteCartasDePorte"));
            modelBuilder.Entity<CartaDePorte>().ToTable("CartasDePorte");

            modelBuilder.Entity<LogOperacion>().HasKey(t => t.Id);
            modelBuilder.Entity<LogOperacion>().Property(t => t.Id).HasColumnName("IdLogOperaciones");
            modelBuilder.Entity<LogOperacion>().Property(t => t.Tabla).HasColumnName("Tabla");
            modelBuilder.Entity<LogOperacion>().Property(t => t.Accion).HasColumnName("Accion");
            modelBuilder.Entity<LogOperacion>().Property(t => t.ReferenciaId).HasColumnName("Id");
            modelBuilder.Entity<LogOperacion>().Property(t => t.CreateDate).HasColumnName("Fecha");
            modelBuilder.Entity<LogOperacion>().Property(t => t.CreatedBy).HasColumnName("Usuario");
            modelBuilder.Entity<LogOperacion>().Ignore(t => t.UpdateDate);
            modelBuilder.Entity<LogOperacion>().Ignore(t => t.UpdatedBy);
            modelBuilder.Entity<LogOperacion>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<LogOperacion>().Ignore(t => t.Enabled);
            modelBuilder.Entity<LogOperacion>().ToTable("LogOperaciones");
            
        }

        public IDbSet<Pais> Paises { get; set; }
        public IDbSet<Provincia> Provincias { get; set; }
        public IDbSet<Localidad> Localidades { get; set; }
        public IDbSet<Empresa> Empresas { get; set; }
        public IDbSet<GrupoEmpresa> GrupoEmpresas { get; set; }
        public IDbSet<Chofer> Choferes { get; set; }
        public IDbSet<Grano> Granos { get; set; }
        public IDbSet<Especie> Especies { get; set; }
        public IDbSet<TipoGrano> TiposGrano { get; set; }
        public IDbSet<Cosecha> Cosechas { get; set; }
        public IDbSet<Establecimiento> Establecimientos { get; set; }
        public IDbSet<Cliente> Clientes { get; set; }
        public IDbSet<Proveedor> Proveedores { get; set; }
        public IDbSet<Solicitud> Solicitudes { get; set; }
        public IDbSet<LoteCartaPorte> LotesCartaPorte { get; set; }
        public IDbSet<CartaDePorte> CartaDePortes { get; set; }
        public IDbSet<LogOperacion> LogOperaciones { get; set; }
    }
}

