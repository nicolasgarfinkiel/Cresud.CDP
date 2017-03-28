using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Cresud.CDP.Entities;

namespace Cresud.CDP.EFRepositories
{
    public class CDPContext : DbContext
    {
        public CDPContext()
            : base(ConfigurationManager.ConnectionStrings["CDP"].ConnectionString)
        {
            Database.SetInitializer<CDPContext>(null);
            this.Database.CommandTimeout = 500;
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

            modelBuilder.Entity<TipoCarta>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoCarta>().Property(t => t.Id).HasColumnName("IdTipoDeCarta");
            modelBuilder.Entity<TipoCarta>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<TipoCarta>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<TipoCarta>().ToTable("TipoDeCarta");

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
            modelBuilder.Entity<Solicitud>().Property(t => t.TipoDeCartaId).HasColumnName("IdTipoDeCarta");
            modelBuilder.Entity<Solicitud>().Property(t => t.ObservacionAfip).HasColumnName("ObservacionAfip");
            modelBuilder.Entity<Solicitud>().Property(t => t.NumeroCartaDePorte).HasColumnName("NumeroCartaDePorte");
            modelBuilder.Entity<Solicitud>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<Solicitud>().Property(t => t.Ctg).HasColumnName("Ctg");
            modelBuilder.Entity<Solicitud>().Property(t => t.FechaDeEmision).HasColumnName("FechaDeEmision");
            modelBuilder.Entity<Solicitud>().Property(t => t.FechaDeCarga).HasColumnName("FechaDeCarga");
            modelBuilder.Entity<Solicitud>().Property(t => t.FechaDeVencimiento).HasColumnName("FechaDeVencimiento");
            modelBuilder.Entity<Solicitud>().Property(t => t.ProveedorTitularCartaDePorteId).HasColumnName("idProveedorTitularCartaDePorte");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteIntermediarioId).HasColumnName("IdClienteIntermediario");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteRemitenteComercialId).HasColumnName("IdClienteRemitenteComercial");
            modelBuilder.Entity<Solicitud>().Property(t => t.RemitenteComercialComoCanjeador).HasColumnName("RemitenteComercialComoCanjeador");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteCorredorId).HasColumnName("IdClienteCorredor");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteEntregadorId).HasColumnName("IdClienteEntregador");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteDestinatarioId).HasColumnName("IdClienteDestinatario");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteDestinoId).HasColumnName("IdClienteDestino");
            modelBuilder.Entity<Solicitud>().Property(t => t.ProveedorTransportistaId).HasColumnName("IdProveedorTransportista");
            modelBuilder.Entity<Solicitud>().Property(t => t.ChoferId).HasColumnName("IdChofer");
            modelBuilder.Entity<Solicitud>().Property(t => t.CosechaId).HasColumnName("IdCosecha");
            modelBuilder.Entity<Solicitud>().Property(t => t.EspecieId).HasColumnName("IdEspecie");
            modelBuilder.Entity<Solicitud>().Property(t => t.NumeroContrato).HasColumnName("NumeroContrato");
            modelBuilder.Entity<Solicitud>().Property(t => t.SapContrato).HasColumnName("SapContrato");
            modelBuilder.Entity<Solicitud>().Property(t => t.SinContrato).HasColumnName("SinContrato");
            modelBuilder.Entity<Solicitud>().Property(t => t.CargaPesadaDestino).HasColumnName("CargaPesadaDestino");
            modelBuilder.Entity<Solicitud>().Property(t => t.KilogramosEstimados).HasColumnName("KilogramosEstimados");
            modelBuilder.Entity<Solicitud>().Property(t => t.DeclaracionDeCalidad).HasColumnName("DeclaracionDeCalidad");
            modelBuilder.Entity<Solicitud>().Property(t => t.ConformeCondicionalId).HasColumnName("IdConformeCondicional");
            modelBuilder.Entity<Solicitud>().Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            modelBuilder.Entity<Solicitud>().Property(t => t.PesoTara).HasColumnName("PesoTara");
            modelBuilder.Entity<Solicitud>().Property(t => t.Observaciones).HasColumnName("Observaciones");
            modelBuilder.Entity<Solicitud>().Property(t => t.LoteDeMaterial).HasColumnName("LoteDeMaterial");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstablecimientoProcedenciaId).HasColumnName("IdEstablecimientoProcedencia");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstablecimientoDestinoId).HasColumnName("IdEstablecimientoDestino");
            modelBuilder.Entity<Solicitud>().Property(t => t.PatenteCamion).HasColumnName("PatenteCamion");
            modelBuilder.Entity<Solicitud>().Property(t => t.PatenteAcoplado).HasColumnName("PatenteAcoplado");
            modelBuilder.Entity<Solicitud>().Property(t => t.KmRecorridos).HasColumnName("KmRecorridos");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstadoFlete).HasColumnName("EstadoFlete");
            modelBuilder.Entity<Solicitud>().Property(t => t.CantHoras).HasColumnName("CantHoras");
            modelBuilder.Entity<Solicitud>().Property(t => t.TarifaReferencia).HasColumnName("TarifaReferencia");
            modelBuilder.Entity<Solicitud>().Property(t => t.TarifaReal).HasColumnName("TarifaReal");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClientePagadorDelFleteId).HasColumnName("IdClientePagadorDelFlete");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstadoEnSAP).HasColumnName("EstadoEnSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstadoEnAFIP).HasColumnName("EstadoEnAFIP");
            modelBuilder.Entity<Solicitud>().Property(t => t.GranoId).HasColumnName("IdGrano");
            modelBuilder.Entity<Solicitud>().Property(t => t.CodigoAnulacionAfip).HasColumnName("CodigoAnulacionAfip");
            modelBuilder.Entity<Solicitud>().Property(t => t.FechaAnulacionAfip).HasColumnName("FechaAnulacionAfip");
            modelBuilder.Entity<Solicitud>().Property(t => t.CodigoRespuestaEnvioSAP).HasColumnName("CodigoRespuestaEnvioSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.MensajeRespuestaEnvioSAP).HasColumnName("MensajeRespuestaEnvioSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.CodigoRespuestaAnulacionSAP).HasColumnName("CodigoRespuestaAnulacionSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.MensajeRespuestaAnulacionSAP).HasColumnName("MensajeRespuestaAnulacionSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.CodigoRespuestaAnulacionSAP).HasColumnName("CodigoRespuestaAnulacionSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.MensajeRespuestaAnulacionSAP).HasColumnName("MensajeRespuestaAnulacionSAP");
            modelBuilder.Entity<Solicitud>().Property(t => t.EstablecimientoDestinoCambioId).HasColumnName("IdEstablecimientoDestinoCambio");
            modelBuilder.Entity<Solicitud>().Property(t => t.ClienteDestinatarioCambioId).HasColumnName("IdClienteDestinatarioCambio");
            modelBuilder.Entity<Solicitud>().Property(t => t.ChoferTransportistaId).HasColumnName("IdChoferTransportista");
            modelBuilder.Entity<Solicitud>().Property(t => t.PHumedad).HasColumnName("PHumedad");
            modelBuilder.Entity<Solicitud>().Property(t => t.POtros).HasColumnName("POtros");
            modelBuilder.Entity<Solicitud>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<Solicitud>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<Solicitud>().Ignore(t => t.Enabled);
            modelBuilder.Entity<Solicitud>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<Solicitud>().ToTable("Solicitudes");


            modelBuilder.Entity<SolicitudReport>().HasKey(t => t.Id);
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Id).HasColumnName("IdSolicitud");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ObservacionAfip).HasColumnName("ObservacionAfip");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.NumeroCartaDePorte).HasColumnName("NumeroCartaDePorte");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Ctg).HasColumnName("Ctg");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.FechaDeEmision).HasColumnName("FechaDeEmision");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.FechaDeCarga).HasColumnName("FechaDeCarga");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.FechaDeVencimiento).HasColumnName("FechaDeVencimiento");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ProveedorTitularCartaDePorteId).HasColumnName("idProveedorTitularCartaDePorte");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClienteIntermediarioId).HasColumnName("IdClienteIntermediario");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClienteRemitenteComercialId).HasColumnName("IdClienteRemitenteComercial");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClienteCorredorId).HasColumnName("IdClienteCorredor");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClienteEntregadorId).HasColumnName("IdClienteEntregador");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClienteDestinatarioId).HasColumnName("IdClienteDestinatario");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ProveedorTransportistaId).HasColumnName("IdProveedorTransportista");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ChoferId).HasColumnName("IdChofer");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CosechaId).HasColumnName("IdCosecha");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.NumeroContrato).HasColumnName("NumeroContrato");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CargaPesadaDestino).HasColumnName("CargaPesadaDestino");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.KilogramosEstimados).HasColumnName("KilogramosEstimados");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.PesoTara).HasColumnName("PesoTara");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Observaciones).HasColumnName("Observaciones");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstablecimientoProcedenciaId).HasColumnName("IdEstablecimientoProcedencia");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstablecimientoDestinoId).HasColumnName("IdEstablecimientoDestino");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.PatenteCamion).HasColumnName("PatenteCamion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.PatenteAcoplado).HasColumnName("PatenteAcoplado");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.KmRecorridos).HasColumnName("KmRecorridos");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstadoFlete).HasColumnName("EstadoFlete");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CantHoras).HasColumnName("CantHoras");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.TarifaReferencia).HasColumnName("TarifaReferencia");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.TarifaReal).HasColumnName("TarifaReal");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstadoEnSAP).HasColumnName("EstadoEnSAP");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstadoEnAFIP).HasColumnName("EstadoEnAFIP");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.GranoId).HasColumnName("IdGrano");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CodigoAnulacionAfip).HasColumnName("CodigoAnulacionAfip");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.FechaAnulacionAfip).HasColumnName("FechaAnulacionAfip");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CodigoRespuestaEnvioSAP).HasColumnName("CodigoRespuestaEnvioSAP");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CodigoRespuestaAnulacionSAP).HasColumnName("CodigoRespuestaAnulacionSAP");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.PHumedad).HasColumnName("PHumedad");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.POtros).HasColumnName("POtros");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.TipoCarta).HasColumnName("TipoCarta");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.TitularCDP).HasColumnName("TitularCDP");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Intermediario).HasColumnName("Intermediario");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteRemitenteComecial).HasColumnName("CteRemitenteComecial");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EsCanjeador).HasColumnName("EsCanjeador");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteCorredor).HasColumnName("CteCorredor");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Entregador).HasColumnName("Entregador");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Destinatario).HasColumnName("Destinatario");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Destino).HasColumnName("Destino");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Transportista).HasColumnName("Transportista");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CTransportista).HasColumnName("CTransportista");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Chofer).HasColumnName("Chofer");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Grano).HasColumnName("Grano");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ConformeCondicional).HasColumnName("ConformeCondicional");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstProcedencia).HasColumnName("EstProcedencia");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstDestino).HasColumnName("EstDestino");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CtePagador).HasColumnName("CtePagador");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstDestinoCambio).HasColumnName("EstDestinoCambio");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteDestinatarioCambio).HasColumnName("CteDestinatarioCambio");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CosechaDescripcion).HasColumnName("CosechaDescripcion");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.Asociacartadeporte).HasColumnName("asociacartadeporte");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ProvTitularCDPNumeroDocumento).HasColumnName("ProvTitularCDPNumeroDocumento");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteIntermediarioCuit).HasColumnName("CteIntermediarioCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteRemitenteComecialCuit).HasColumnName("CteRemitenteComecialCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteCorredorCuit).HasColumnName("CteCorredorCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteEntregadorCuit).HasColumnName("CteEntregadorCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteDestinatarioCuit).HasColumnName("CteDestinatarioCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CteDestinoCuit).HasColumnName("CteDestinoCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.TransportistaNumeroDocumento).HasColumnName("TransportistaNumeroDocumento");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.CTransportistaCuit).HasColumnName("CTransportistaCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ChoferCuit).HasColumnName("ChoferCuit");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EspecieCodigo).HasColumnName("EspecieCodigo");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.IdTipoGrano).HasColumnName("IdTipoGrano");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstProcedenciaEstablecimientoAfip).HasColumnName("EstProcedenciaEstablecimientoAfip");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstProcedenciaLocalidad).HasColumnName("EstProcedenciaLocalidad");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstDestinoEstablecimientoAfip).HasColumnName("EstDestinoEstablecimientoAfip");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EstDestinoLocalidad).HasColumnName("EstDestinoLocalidad");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.ClientePagadorIdSapOrganizacionDeVenta).HasColumnName("ClientePagadorIdSapOrganizacionDeVenta");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.MensajeRespuestaEnvioSap).HasColumnName("MensajeRespuestaEnvioSap");
            modelBuilder.Entity<SolicitudReport>().Property(t => t.EmpresaProveedorTitularSapId).HasColumnName("EmpresaProveedorTitularSap_Id");
            modelBuilder.Entity<SolicitudReport>().Ignore(t => t.Enabled);
            modelBuilder.Entity<SolicitudReport>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<SolicitudReport>().ToTable("vReporteCDP");


            modelBuilder.Entity<SolicitudRecibida>().HasKey(t => t.Id);
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.Id).HasColumnName("IdSolicitudRecibida");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.TipoCartaId).HasColumnName("IdTipoDeCarta");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.NumeroCartaDePorte).HasColumnName("NumeroCartaDePorte");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.Ctg).HasColumnName("Ctg");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.FechaDeCarga).HasColumnName("FechaDeCarga");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ProveedorTitularCartaDePorteCuit).HasColumnName("idProveedorTitularCartaDePorte");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteIntermediarioCuit).HasColumnName("IdClienteIntermediario");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteRemitenteComercialCuit).HasColumnName("IdClienteRemitenteComercial");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteCorredorCuit).HasColumnName("IdClienteCorredor");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteEntregadorCuit).HasColumnName("IdClienteEntregador");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteDestinatarioCuit).HasColumnName("IdClienteDestinatario");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ClienteDestinoCuit).HasColumnName("IdClienteDestino");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ProveedorTransportistaCuit).HasColumnName("IdProveedorTransportista");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.ChoferCuit).HasColumnName("IdChofer");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.NumeroContrato).HasColumnName("NumeroContrato");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.CargaPesadaDestino).HasColumnName("CargaPesadaDestino");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.KilogramosEstimados).HasColumnName("KilogramosEstimados");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.PesoTara).HasColumnName("PesoTara");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EstablecimientoProcedenciaCodigo).HasColumnName("CodigoEstablecimientoProcedencia");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EstablecimientoProcedenciaLocalidadId).HasColumnName("IdLocalidadEstablecimientoProcedencia");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.KmRecorridos).HasColumnName("KmRecorridos");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.PatenteCamion).HasColumnName("PatenteCamion");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.PatenteAcoplado).HasColumnName("PatenteAcoplado");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.TarifaReal).HasColumnName("TarifaReal");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.FchaDescarga).HasColumnName("FechaDeDescarga");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.FechaArribo).HasColumnName("FechaDeArribo");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.PesoNetoDescarga).HasColumnName("PesoNetoDescarga");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EstablecimientoDestinoCambioCuit).HasColumnName("CuitEstablecimientoDestinoCambio");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EstablecimientoDestinoCambioLocalidadId).HasColumnName("IdLocalidadEstablecimientoDestinoCambio");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.EstablecimientoDestinoCambioCodigo).HasColumnName("CodigoEstablecimientoDestinoCambio");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.TarifaReferencia).HasColumnName("TarifaReferencia");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.CreateDate).HasColumnName("FechaCreacion");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.CreatedBy).HasColumnName("UsuarioCreacion");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.UpdateDate).HasColumnName("FechaModificacion");
            modelBuilder.Entity<SolicitudRecibida>().Property(t => t.UpdatedBy).HasColumnName("UsuarioModificacion");
            modelBuilder.Entity<SolicitudRecibida>().Ignore(t => t.Enabled);
            modelBuilder.Entity<SolicitudRecibida>().Ignore(t => t.DeletedBy);
            modelBuilder.Entity<SolicitudRecibida>().HasOptional(e => e.Grano).WithMany().Map(x => x.MapKey("IdGrano"));
            modelBuilder.Entity<SolicitudRecibida>().ToTable("SolicitudesRecibidas");



            modelBuilder.Entity<LoteCartaPorte>().HasKey(t => t.Id);
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Id).HasColumnName("IdLoteCartasDePorte");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Desde).HasColumnName("Desde");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Hasta).HasColumnName("Hasta");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.EstablecimientoOrigenId).HasColumnName("EstablecimientoOrigen");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaVencimiento).HasColumnName("FechaVencimiento");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.Sucursal).HasColumnName("Sucursal");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.PuntoEmision).HasColumnName("PuntoEmision");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaDesde).HasColumnName("FechaDesde");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.FechaHasta).HasColumnName("FechaHasta");
            modelBuilder.Entity<LoteCartaPorte>().Property(t => t.HabilitacionNumero).HasColumnName("HabilitacionNum");
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


            modelBuilder.Entity<ClienteRemitenteComercial>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteRemitenteComercial>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<ClienteRemitenteComercial>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<ClienteRemitenteComercial>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<ClienteRemitenteComercial>().ToTable("vClienteRemitenteComercial");

            modelBuilder.Entity<ClienteCorredor>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteCorredor>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<ClienteCorredor>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<ClienteCorredor>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<ClienteCorredor>().ToTable("vClienteCorredor");

            modelBuilder.Entity<ClienteEntregador>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteEntregador>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<ClienteEntregador>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<ClienteEntregador>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<ClienteEntregador>().ToTable("vClienteEntregador");

            modelBuilder.Entity<ClienteDestinatario>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteDestinatario>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<ClienteDestinatario>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<ClienteDestinatario>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<ClienteDestinatario>().ToTable("vClienteDestinatario");

            modelBuilder.Entity<ClienteIntermediario>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteIntermediario>().Property(t => t.Id).HasColumnName("IdCliente");
            modelBuilder.Entity<ClienteIntermediario>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<ClienteIntermediario>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<ClienteIntermediario>().ToTable("vClienteIntermediario");

            modelBuilder.Entity<CartaDePorteGraficoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<CartaDePorteGraficoItem>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<CartaDePorteGraficoItem>().Property(t => t.EmpresaId).HasColumnName("IdEmpresa");
            modelBuilder.Entity<CartaDePorteGraficoItem>().Property(t => t.Fecha).HasColumnName("Fecha");
            modelBuilder.Entity<CartaDePorteGraficoItem>().Property(t => t.CantidadAfip).HasColumnName("CantidadAfip");
            modelBuilder.Entity<CartaDePorteGraficoItem>().Property(t => t.CantidadSap).HasColumnName("CantidadSap");
            modelBuilder.Entity<CartaDePorteGraficoItem>().ToTable("vGraficoItemCDP");

            modelBuilder.Entity<LogSap>().HasKey(t => t.Id);
            modelBuilder.Entity<LogSap>().Property(t => t.Id).HasColumnName("IdLogSap");
            modelBuilder.Entity<LogSap>().Property(t => t.IdDocumento).HasColumnName("IDoc");
            modelBuilder.Entity<LogSap>().Property(t => t.Origen).HasColumnName("Origen");
            modelBuilder.Entity<LogSap>().Property(t => t.NroDocumentoRE).HasColumnName("NroDocumentoRE");
            modelBuilder.Entity<LogSap>().Property(t => t.NroDocumentoSap).HasColumnName("NroDocumentoSap");
            modelBuilder.Entity<LogSap>().Property(t => t.TipoMensaje).HasColumnName("TipoMensaje");
            modelBuilder.Entity<LogSap>().Property(t => t.TextoMensaje).HasColumnName("TextoMensaje");
            modelBuilder.Entity<LogSap>().Property(t => t.NroEnvio).HasColumnName("NroEnvio");
            modelBuilder.Entity<LogSap>().Property(t => t.FechaCreacion).HasColumnName("FechaCreacion");
            modelBuilder.Entity<LogSap>().ToTable("LogSap");

            modelBuilder.Entity<RemitoParaguay>().HasKey(t => t.Id);
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Id).HasColumnName("IdSolicitud");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Cee).HasColumnName("Cee");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.FechaCreacion).HasColumnName("FechaCreacion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.FechaVencimiento).HasColumnName("FechaVencimiento");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.NumeroRemision).HasColumnName("NumeroRemision");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.FechaDeEmision).HasColumnName("FechaDeEmision");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.RazonSocial).HasColumnName("RazonSocial");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Cuit).HasColumnName("Cuit");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Direccion).HasColumnName("Direccion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.MotivoTraslado).HasColumnName("MotivoTraslado");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.CteDeVta).HasColumnName("CteDeVta");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.TranspRazonSocial).HasColumnName("TranspRazonSocial");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.TransportistaCuit).HasColumnName("TransportistaCuit");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.EPDireccion).HasColumnName("EPDireccion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.LocPartida).HasColumnName("LocPartida");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.ProvPartida).HasColumnName("ProvPartida");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.EDDireccion).HasColumnName("EDDireccion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.LocLlegada).HasColumnName("LocLlegada");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.ProvLlegada).HasColumnName("ProvLlegada");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.KmRecorridos).HasColumnName("KmRecorridos");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.PatenteCamion).HasColumnName("PatenteCamion");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.PatenteAcoplado).HasColumnName("PatenteAcoplado");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.ChoferRazonSocial).HasColumnName("ChoferRazonSocial");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.ChoferCuit).HasColumnName("ChoferCuit");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.ChoferDomicilio).HasColumnName("ChoferDomicilio");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.MarcaVehiculo).HasColumnName("MarcaVehiculo");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.Kg).HasColumnName("Kg");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.DescripcionDetallada).HasColumnName("DescripcionDetallada");
            modelBuilder.Entity<RemitoParaguay>().Property(t => t.HabilitacionNum).HasColumnName("HabilitacionNum");
            modelBuilder.Entity<RemitoParaguay>().ToTable("vRemitoParaguay");


           modelBuilder.Entity<AfipAuth>().HasKey(t => t.Id);
            modelBuilder.Entity<AfipAuth>().Property(t => t.Id).HasColumnName("IdAfipAuth");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.Token).HasColumnName("Token");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.Sign).HasColumnName("Sign");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.CuitRepresentado).HasColumnName("CuitRepresentado");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.GenerationTime).HasColumnName("GenerationTime");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.ExpirationTime).HasColumnName("ExpirationTime");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.Service).HasColumnName("Service");            
            modelBuilder.Entity<AfipAuth>().Property(t => t.UniqueId).HasColumnName("UniqueID");
            modelBuilder.Entity<AfipAuth>().ToTable("AfipAuth");
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
        public IDbSet<SolicitudReport> SolicitudesReport { get; set; }
        public IDbSet<LoteCartaPorte> LotesCartaPorte { get; set; }
        public IDbSet<CartaDePorte> CartaDePortes { get; set; }
        public IDbSet<LogOperacion> LogOperaciones { get; set; }
        public IDbSet<TipoCarta> TipoCartas { get; set; }
        public IDbSet<ClienteRemitenteComercial> ClientesRemitenteComercial { get; set; }
        public IDbSet<ClienteCorredor> ClientesCorredor { get; set; }
        public IDbSet<ClienteEntregador> ClientesEntregador { get; set; }
        public IDbSet<ClienteDestinatario> ClientesDestinatario { get; set; }
        public IDbSet<ClienteIntermediario> ClientesIntermediarios { get; set; }
        public IDbSet<SolicitudRecibida> SolicitudesRecibidas { get; set; }
        public IDbSet<CartaDePorteGraficoItem> ItemsGrafico { get; set; }
        public IDbSet<LogSap> LogsSap { get; set; }
        public IDbSet<RemitoParaguay> RemitosParaguay { get; set; }
        public IDbSet<AfipAuth> AfipAuth { get; set; }
    }
}

