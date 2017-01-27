USE [CartaDePorte]
GO

IF OBJECT_ID('[dbo].[vReporteCDP]', 'V') IS NOT NULL
    DROP VIEW [dbo].[vReporteCDP]
GO


CREATE VIEW [dbo].[vReporteCDP]
AS

Select Distinct 
Sol.IdSolicitud, 
TC.Descripcion TipoCarta, 
REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(Sol.ObservacionAfip)),CHAR(9),' '),CHAR(10),' '),CHAR(13),' ') ObservacionAfip, 
Sol.NumeroCartaDePorte, 
Sol.Cee, 
Sol.Ctg, 
Sol.FechaDeEmision,
Sol.FechaDeCarga, 
Sol.FechaDeVencimiento, 
ProvTitularCDP.Nombre TitularCDP, 
CteIntermediario.RazonSocial Intermediario, 
CteRemitenteComecial.RazonSocial CteRemitenteComecial, 
Sol.RemitenteComercialComoCanjeador EsCanjeador, 
CteCorredor.RazonSocial CteCorredor,
CteEntregador.RazonSocial Entregador, 
CteDestinatario.RazonSocial Destinatario, 
CteDestino.RazonSocial Destino, 
Transportista.Nombre Transportista, 
CTransportista.Nombre + ' ' + CTransportista.Apellido CTransportista, 
Chofer.Nombre + ' ' + Chofer.Apellido Chofer, 
Grano.Descripcion Grano, 
Sol.NumeroContrato, 
Sol.CargaPesadaDestino, 
Sol.KilogramosEstimados, 
Sol.IdConformeCondicional ConformeCondicional, 
Sol.PesoBruto, 
Sol.PesoTara, 
Sol.PesoNeto, 
REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(Sol.Observaciones)),CHAR(9),' '),CHAR(10),' '),CHAR(13),' ') Observaciones, 
LTRIM(RTRIM(EstProcedencia.Descripcion)) EstProcedencia, 
LTRIM(RTRIM(EstDestino.Descripcion)) EstDestino, 
Chofer.Camion PatenteCamion, 
Chofer.Acoplado PatenteAcoplado, 
Sol.KmRecorridos, 
Sol.EstadoFlete, 
Sol.CantHoras, 
Sol.TarifaReferencia, 
Sol.TarifaReal, 
CtePagador.RazonSocial CtePagador, 
Sol.EstadoEnAFIP, 
Sol.EstadoEnSAP, 
EstDestinoCambio.Descripcion EstDestinoCambio,
CteDestinatarioCambio.RazonSocial CteDestinatarioCambio, 
Sol.CodigoAnulacionAfip, 
Sol.FechaAnulacionAfip, 
Sol.CodigoRespuestaEnvioSAP,
Sol.CodigoRespuestaAnulacionSAP, 
Sol.FechaCreacion, 
Sol.UsuarioCreacion, 
Sol.FechaModificacion, 
Sol.UsuarioModificacion, 
Sol.IdEmpresa,
Sol.IdGrano, 
Sol.idProveedorTitularCartaDePorte, 
Sol.IdClienteIntermediario, 
Sol.IdClienteRemitenteComercial, 
Sol.IdClienteCorredor, 
Sol.IdClienteEntregador, 
Sol.IdClienteDestinatario, 
Sol.IdProveedorTransportista, 
Sol.IdChofer, 
Sol.IdEstablecimientoProcedencia,
Sol.IdEstablecimientoDestino, 
Sol.PHumedad, 
Sol.POtros, 
Cosecha.IdCosecha, 
Cosecha.Descripcion CosechaDescripcion,
/*NUEVOS CAMPOS*/
EstProcedencia.asociacartadeporte,
ProvTitularCDP.NumeroDocumento as ProvTitularCDPNumeroDocumento,
CteIntermediario.Cuit as CteIntermediarioCuit,
CteRemitenteComecial.Cuit as CteRemitenteComecialCuit,
CteCorredor.Cuit as CteCorredorCuit,
CteEntregador.Cuit as CteEntregadorCuit,
CteDestinatario.Cuit as CteDestinatarioCuit,
CteDestino.Cuit as CteDestinoCuit,
Transportista.NumeroDocumento as  TransportistaNumeroDocumento,
CTransportista.Cuit as CTransportistaCuit,
Chofer.Cuit as ChoferCuit,
Especie.Codigo as EspecieCodigo,
Grano.IdTipoGrano,
EstProcedencia.EstablecimientoAfip as EstProcedenciaEstablecimientoAfip,
EstProcedencia.Localidad as EstProcedenciaLocalidad,
EstDestino.EstablecimientoAfip as EstDestinoEstablecimientoAfip,
EstDestino.Localidad as EstDestinoLocalidad
From Solicitudes Sol
Left Join TipoDeCarta TC On Sol.IdTipoDeCarta = TC.IdTipoDeCarta
Left Join Proveedor ProvTitularCDP On Sol.idProveedorTitularCartaDePorte = ProvTitularCDP.IdProveedor
Left Join Cliente CteIntermediario On Sol.IdClienteIntermediario = CteIntermediario.IdCliente
Left Join Cliente CteRemitenteComecial On Sol.IdClienteRemitenteComercial = CteRemitenteComecial.IdCliente
Left Join Cliente CteCorredor On Sol.IdClienteCorredor = CteCorredor.IdCliente
Left Join Cliente CteEntregador On Sol.IdClienteEntregador = CteEntregador.IdCliente
Left Join Cliente CteDestinatario On Sol.IdClienteDestinatario = CteDestinatario.IdCliente
Left Join Cliente CteDestino On Sol.IdClienteDestino = CteDestino.IdCliente
Left Join Proveedor Transportista On Sol.IdProveedorTransportista = Transportista.IdProveedor
Left join Chofer CTransportista On Sol.IdChoferTransportista = CTransportista.IdChofer
Left join Chofer Chofer On Sol.IdChofer = Chofer.IdChofer
Left Join Grano Grano On Sol.IdGrano = Grano.IdGrano
Left Join Cosecha Cosecha On Grano.IdCosechaAfip = Cosecha.IdCosecha
Left Join Especie Especie On Grano.IdEspecieAfip = Especie.IdEspecie
Left Join Establecimiento EstProcedencia On Sol.IdEstablecimientoProcedencia = EstProcedencia.IdEstablecimiento
Left Join Establecimiento EstDestino On Sol.IdEstablecimientoDestino = EstDestino.IdEstablecimiento
Left Join Cliente CtePagador On Sol.IdClientePagadorDelFlete = CtePagador.IdCliente
Left Join Establecimiento EstDestinoCambio On Sol.IdEstablecimientoDestinoCambio = EstDestinoCambio.IdEstablecimiento
Left Join Cliente CteDestinatarioCambio On Sol.IdClienteDestinatarioCambio = CteDestinatarioCambio.IdCliente



GO


