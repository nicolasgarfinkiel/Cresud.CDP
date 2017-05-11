USE [CartaDePorte]
GO

IF OBJECT_ID('[dbo].[vSolicitudBandejaSalida]', 'V') IS NOT NULL
    DROP VIEW [dbo].[vSolicitudBandejaSalida]
GO



CREATE VIEW [dbo].[vSolicitudBandejaSalida]  
AS


Select Distinct 
Sol.IdSolicitud, 
TC.Descripcion TipoCarta, 
REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(Sol.ObservacionAfip)),CHAR(9),' '),CHAR(10),' '),CHAR(13),' ') ObservacionAfip, 
Sol.NumeroCartaDePorte, 
Sol.Ctg, 
Sol.FechaDeEmision,
Sol.FechaDeVencimiento, 
ProvTitularCDP.Nombre TitularCDP, 
LTRIM(RTRIM(EstProcedencia.Descripcion)) EstProcedencia, 
LTRIM(RTRIM(EstDestino.Descripcion)) EstDestino, 
Sol.EstadoEnAFIP, 
Sol.EstadoEnSAP, 
Sol.CodigoAnulacionAfip, 
Sol.CodigoRespuestaEnvioSAP,
Sol.FechaCreacion, 
Sol.UsuarioCreacion, 
Sol.FechaModificacion, 
Sol.UsuarioModificacion, 
Sol.IdEmpresa,
Sol.MensajeRespuestaEnvioSAP
From [dbo].Solicitudes Sol
Left Join [dbo].TipoDeCarta TC On Sol.IdTipoDeCarta = TC.IdTipoDeCarta
Left Join [dbo].Proveedor ProvTitularCDP On Sol.idProveedorTitularCartaDePorte = ProvTitularCDP.IdProveedor
Left Join [dbo].Establecimiento EstProcedencia On Sol.IdEstablecimientoProcedencia = EstProcedencia.IdEstablecimiento
Left Join [dbo].Establecimiento EstDestino On Sol.IdEstablecimientoDestino = EstDestino.IdEstablecimiento

GO
