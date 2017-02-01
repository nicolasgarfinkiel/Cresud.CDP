USE [CartaDePorte]
GO

IF OBJECT_ID('[dbo].[vGraficoItemCDP]', 'V') IS NOT NULL
    DROP VIEW [dbo].[vGraficoItemCDP]
GO


CREATE VIEW [dbo].[vGraficoItemCDP]
AS

	SELECT 
    ROW_NUMBER() OVER(ORDER BY afip.IdEmpresa, afip.Fecha ) AS Id,
	afip.Fecha,
	afip.IdEmpresa,
	ISNULL(afip.cntAfip,0) CantidadAfip,
	ISNULL(sap.cntSap,0) CantidadSap 
	FROM 
		(			
 		    select DATEADD(dd, DATEDIFF(dd, 0, FechaDeEmision), 0) Fecha,
			IdEmpresa,
			count(*) cntAfip 
			from solicitudes 
			where  ctg <> '' and ctg is not null and EstadoEnAFIP in (1,4,5,6,8,9) 
			group by IdEmpresa, DATEADD(dd, DATEDIFF(dd, 0, FechaDeEmision), 0) 
		) afip full 
	OUTER JOIN
		(
			select DATEADD(dd, DATEDIFF(dd, 0, FechaDeEmision), 0) Fecha,
			IdEmpresa,
			count(*) cntSap 
			from solicitudes 
			where  ctg <> '' and ctg is not null and EstadoEnSAP in (2) 
			group by IdEmpresa, DATEADD(dd, DATEDIFF(dd, 0, FechaDeEmision), 0) 
		) sap 
	ON afip.Fecha = sap.Fecha AND afip.IdEmpresa = sap.IdEmpresa
		
GO
