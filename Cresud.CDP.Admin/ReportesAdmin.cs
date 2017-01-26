﻿using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using OfficeOpenXml;
using SolicitudReport = Cresud.CDP.Entities.SolicitudReport;

namespace Cresud.CDP.Admin
{
    public class ReportesAdmin : BaseAdmin<int, Entities.SolicitudReport, Dtos.SolicitudReport, FilterBase>
    {
        #region Base

        public override SolicitudReport ToEntity(Dtos.SolicitudReport dto)
        {
            throw new NotImplementedException();
        }

        public override void Validate(Dtos.SolicitudReport dto)
        {
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            throw new NotImplementedException();
        }

        #endregion

        public ExcelPackage Export(Dtos.Filters.FilterCartasDePorteExport filter)
        {
            #region Data

            filter.FechaHasta = filter.FechaHasta.Value.AddDays(1).AddMilliseconds(-1);

            var data = CdpContext.SolicitudesReport.Where(s => s.EmpresaId == filter.EmpresaId &&
                                s.FechaDeEmision >= filter.FechaDesde && s.FechaDeEmision <= filter.FechaHasta)
                                .AsQueryable();

            if (filter.GranoId.HasValue)
            {
                data = data.Where(d => d.GranoId == filter.GranoId).AsQueryable();
            }

            if (filter.ProveedorTitularCartaDePorteId.HasValue)
            {
                data = data.Where(d => d.ProveedorTitularCartaDePorteId == filter.ProveedorTitularCartaDePorteId).AsQueryable();
            }

            if (filter.IntermediarioId.HasValue)
            {
                data = data.Where(d => d.ClienteIntermediarioId == filter.IntermediarioId).AsQueryable();
            }

            if (filter.ClienteRemitenteComercialId.HasValue)
            {
                data = data.Where(d => d.ClienteRemitenteComercialId == filter.ClienteRemitenteComercialId).AsQueryable();
            }

            if (filter.ClienteCorredorId.HasValue)
            {
                data = data.Where(d => d.ClienteCorredorId == filter.ClienteCorredorId).AsQueryable();
            }

            if (filter.ClienteEntregadorId.HasValue)
            {
                data = data.Where(d => d.ClienteEntregadorId == filter.ClienteEntregadorId).AsQueryable();
            }

            if (filter.ClienteDestinatarioId.HasValue)
            {
                data = data.Where(d => d.ClienteDestinatarioId == filter.ClienteDestinatarioId).AsQueryable();
            }

            if (filter.ProveedorTransportistaId.HasValue)
            {
                data = data.Where(d => d.ProveedorTransportistaId == filter.ProveedorTransportistaId).AsQueryable();
            }

            if (filter.ChoferId.HasValue)
            {
                data = data.Where(d => d.ChoferId == filter.ChoferId).AsQueryable();
            }

            if (filter.EstablecimientoProcedenciaId.HasValue)
            {
                data = data.Where(d => d.EstablecimientoProcedenciaId == filter.EstablecimientoProcedenciaId).AsQueryable();
            }

            if (filter.EstablecimientoDestinoId.HasValue)
            {
                data = data.Where(d => d.EstablecimientoDestinoId == filter.EstablecimientoDestinoId).AsQueryable();
            }

            if (filter.CosechaId.HasValue)
            {
                data = data.Where(d => d.CosechaId == filter.CosechaId).AsQueryable();
            }

            var dataList = data.ToList();
                                                  
            #endregion

            var template = new FileInfo(String.Format(@"{0}\Reports\CartasDePorte.xlsx", AppDomain.CurrentDomain.BaseDirectory));
            var pck = new ExcelPackage(template, true);
            var ws = pck.Workbook.Worksheets[1];
            var row = 2;

            #region Header

            var logoName = string.Empty;
            
            if (App.IdGrupoCresud == filter.IdGrupoEmpresa)
            {
                logoName = "LogoCresud";

                ws.Cells[row, 1].Value = "ClienteDestinatario";
                ws.Cells[row, 2].Value = "ClienteDestino";
                ws.Cells[row, 3].Value = "ProveedorTransportista";
                ws.Cells[row, 4].Value = "ChoferTransportista";
                ws.Cells[row, 5].Value = "Chofer";
                ws.Cells[row, 6].Value = "Cosecha";
                ws.Cells[row, 7].Value = "Grano";
                ws.Cells[row, 8].Value = "NumeroContrato";
                ws.Cells[row, 9].Value = "CargaPesadaDestino";
                ws.Cells[row, 10].Value = "KilogramosEstimados";
                ws.Cells[row, 11].Value = "ConformeCondicional";
                ws.Cells[row, 12].Value = "PesoBruto";
                ws.Cells[row, 13].Value = "PesoTara";
                ws.Cells[row, 14].Value = "PesoNeto";
                ws.Cells[row, 15].Value = "Observaciones";
                ws.Cells[row, 16].Value = "EstablecimientoProcedencia";
                ws.Cells[row, 17].Value = "EstablecimientoDestino";
                ws.Cells[row, 18].Value = "PatenteCamion";
                ws.Cells[row, 19].Value = "PatenteAcoplado";
                ws.Cells[row, 20].Value = "KmRecorridos";
                ws.Cells[row, 21].Value = "EstadoFlete";
                ws.Cells[row, 22].Value = "CantHoras";
                ws.Cells[row, 23].Value = "TarifaReferencia";
                ws.Cells[row, 24].Value = "TarifaReal";
                ws.Cells[row, 25].Value = "ClientePagadorDelFlete";
                ws.Cells[row, 26].Value = "EstadoEnAFIP";
                ws.Cells[row, 27].Value = "EstadoEnSAP";
                ws.Cells[row, 28].Value = "EstablecimientoDestinoCambio";
                ws.Cells[row, 29].Value = "ClienteDestinatarioCambio";
                ws.Cells[row, 30].Value = "CodigoAnulacionAfip";
                ws.Cells[row, 31].Value = "FechaAnulacionAfip";
                ws.Cells[row, 32].Value = "CodigoRespuestaEnvioSAP";
                ws.Cells[row, 33].Value = "CodigoRespuestaAnulacionSAP";
                ws.Cells[row, 34].Value = "FechaCreacion";
                ws.Cells[row, 35].Value = "UsuarioCreacion";
                ws.Cells[row, 36].Value = "FechaModificacion";
                ws.Cells[row, 37].Value = "UsuarioModificacion";
            }
            else
            {
                logoName = "LogoCresca";

                ws.Cells[row, 1].Value = "IdSolicitud";
                ws.Cells[row, 2].Value = "TipoDeCarta";
                ws.Cells[row, 3].Value = "NumeroCartaDePorte";
                ws.Cells[row, 4].Value = "Cee";
                ws.Cells[row, 5].Value = "FechaDeEmision";
                ws.Cells[row, 6].Value = "FechaDeCarga";
                ws.Cells[row, 7].Value = "FechaDeVencimiento";
                ws.Cells[row, 8].Value = "ProveedorTitularCartaDePorte";
                ws.Cells[row, 9].Value = "ClienteIntermediario";
                ws.Cells[row, 10].Value = "ClienteRemitenteComercial";
                ws.Cells[row, 11].Value = "RemitenteComercialComoCanjeador";
                ws.Cells[row, 12].Value = "ClienteCorredor";
                ws.Cells[row, 13].Value = "ClienteEntregador";
                ws.Cells[row, 14].Value = "ClienteDestinatario";
                ws.Cells[row, 15].Value = "ClienteDestino";
                ws.Cells[row, 16].Value = "ProveedorTransportista";
                ws.Cells[row, 17].Value = "ChoferTransportista";
                ws.Cells[row, 18].Value = "Chofer";
                ws.Cells[row, 19].Value = "Cosecha";
                ws.Cells[row, 20].Value = "Grano";
                ws.Cells[row, 21].Value = "NumeroContrato";
                ws.Cells[row, 22].Value = "CargaPesadaDestino";
                ws.Cells[row, 23].Value = "KilogramosEstimados";
                ws.Cells[row, 24].Value = "ConformeCondicional";
                ws.Cells[row, 25].Value = "PesoBruto";
                ws.Cells[row, 26].Value = "PesoTara";
                ws.Cells[row, 27].Value = "PesoNeto";
                ws.Cells[row, 28].Value = "Observaciones";
                ws.Cells[row, 29].Value = "EstablecimientoProcedencia";
                ws.Cells[row, 30].Value = "EstablecimientoDestino";
                ws.Cells[row, 31].Value = "PatenteCamion";
                ws.Cells[row, 32].Value = "PatenteAcoplado";
                ws.Cells[row, 33].Value = "KmRecorridos";
                ws.Cells[row, 34].Value = "EstadoFlete";
                ws.Cells[row, 35].Value = "CantHoras";
                ws.Cells[row, 36].Value = "TarifaReferencia";
                ws.Cells[row, 37].Value = "TarifaReal";
                ws.Cells[row, 38].Value = "ClientePagadorDelFlete";
                ws.Cells[row, 39].Value = "EstadoEnSAP";
                ws.Cells[row, 40].Value = "EstablecimientoDestinoCambio";
                ws.Cells[row, 41].Value = "ClienteDestinatarioCambio";
                ws.Cells[row, 42].Value = "CodigoRespuestaEnvioSAP";
                ws.Cells[row, 43].Value = "CodigoRespuestaAnulacionSAP";
                ws.Cells[row, 44].Value = "Humedad";
                ws.Cells[row, 45].Value = "Otros";
                ws.Cells[row, 46].Value = "FechaCreacion";
                ws.Cells[row, 47].Value = "UsuarioCreacion";
                ws.Cells[row, 48].Value = "FechaModificacion";
                ws.Cells[row, 49].Value = "UsuarioModificacion";
            }

            var logo = new FileInfo(String.Format(@"{0}Content\images\logos\{1}.png", AppDomain.CurrentDomain.BaseDirectory, logoName));
            var picture = ws.Drawings.AddPicture("Logo", logo);
            picture.SetPosition(0, 0);

            #endregion

            #region Body

            foreach (var item in dataList)
            {
                row++;

                if (App.IdGrupoCresud == filter.IdGrupoEmpresa)
                {
                    ws.Cells[row, 1].Value = item.Destinatario;
                    ws.Cells[row, 2].Value = item.Destino;
                    ws.Cells[row, 3].Value =  item.CTransportista;
                    ws.Cells[row, 4].Value = item.Transportista;
                    ws.Cells[row, 5].Value = item.Chofer;
                    ws.Cells[row, 6].Value = item.CosechaDescripcion;
                    ws.Cells[row, 7].Value = item.Grano;
                    ws.Cells[row, 8].Value = item.NumeroContrato;
                    ws.Cells[row, 9].Value = item.CargaPesadaDestino;
                    ws.Cells[row, 10].Value = item.KilogramosEstimados;
                    ws.Cells[row, 11].Value = item.ConformeCondicional;
                    ws.Cells[row, 12].Value = item.PesoBruto;
                    ws.Cells[row, 13].Value = item.PesoTara;
                    ws.Cells[row, 14].Value = item.PesoNeto;
                    ws.Cells[row, 15].Value = item.Observaciones;
                    ws.Cells[row, 16].Value = item.EstProcedencia;
                    ws.Cells[row, 17].Value = item.EstDestino;
                    ws.Cells[row, 18].Value = item.PatenteCamion;
                    ws.Cells[row, 19].Value = item.PatenteAcoplado;
                    ws.Cells[row, 20].Value = item.KmRecorridos;
                    ws.Cells[row, 21].Value = item.EstadoFlete;
                    ws.Cells[row, 22].Value = item.CantHoras;
                    ws.Cells[row, 23].Value = item.TarifaReferencia;
                    ws.Cells[row, 24].Value = item.TarifaReal;
                    ws.Cells[row, 25].Value = item.CtePagador;
                    ws.Cells[row, 26].Value = item.EstadoEnAFIP;
                    ws.Cells[row, 27].Value = item.EstadoEnSAP;
                    ws.Cells[row, 28].Value = item.EstDestinoCambio;
                    ws.Cells[row, 29].Value = item.CteDestinatarioCambio;
                    ws.Cells[row, 30].Value = item.CodigoAnulacionAfip;
                    ws.Cells[row, 31].Value = item.FechaAnulacionAfip;
                    ws.Cells[row, 32].Value = item.CodigoRespuestaEnvioSAP;
                    ws.Cells[row, 33].Value = item.CodigoRespuestaAnulacionSAP;
                    ws.Cells[row, 34].Value = item.CreateDate;
                    ws.Cells[row, 35].Value = item.CreatedBy;
                    ws.Cells[row, 36].Value = item.UpdateDate;
                    ws.Cells[row, 37].Value = item.UpdatedBy;
                }
                else
                {
                    ws.Cells[row, 1].Value = item.Id;
                    ws.Cells[row, 2].Value = item.TipoCarta;
                    ws.Cells[row, 3].Value = item.NumeroCartaDePorte;
                    ws.Cells[row, 4].Value = item.Cee;
                    ws.Cells[row, 5].Value = item.FechaDeEmision;
                    ws.Cells[row, 6].Value = item.FechaDeCarga;
                    ws.Cells[row, 7].Value = item.FechaDeVencimiento;
                    ws.Cells[row, 8].Value = item.TitularCDP;
                    ws.Cells[row, 9].Value = item.Intermediario;
                    ws.Cells[row, 10].Value = item.CteRemitenteComecial;
                    ws.Cells[row, 11].Value = item.EsCanjeador.HasValue && item.EsCanjeador.Value ? "Si" : "No";
                    ws.Cells[row, 12].Value = item.CteCorredor;
                    ws.Cells[row, 13].Value = item.Entregador;
                    ws.Cells[row, 14].Value = item.Destinatario;
                    ws.Cells[row, 15].Value = item.Destino;
                    ws.Cells[row, 16].Value = item.CTransportista;
                    ws.Cells[row, 17].Value = item.Transportista;
                    ws.Cells[row, 18].Value = item.Chofer;
                    ws.Cells[row, 19].Value = item.CosechaDescripcion;
                    ws.Cells[row, 20].Value = item.Grano;
                    ws.Cells[row, 21].Value = item.NumeroContrato;
                    ws.Cells[row, 22].Value = item.CargaPesadaDestino;
                    ws.Cells[row, 23].Value = item.KilogramosEstimados;
                    ws.Cells[row, 24].Value = item.ConformeCondicional;
                    ws.Cells[row, 25].Value = item.PesoBruto;
                    ws.Cells[row, 26].Value = item.PesoTara;
                    ws.Cells[row, 27].Value = item.PesoNeto;
                    ws.Cells[row, 28].Value = item.Observaciones;
                    ws.Cells[row, 29].Value = item.EstProcedencia;
                    ws.Cells[row, 30].Value = item.EstDestino;
                    ws.Cells[row, 31].Value = item.PatenteCamion;
                    ws.Cells[row, 32].Value = item.PatenteAcoplado;
                    ws.Cells[row, 33].Value = item.KmRecorridos;
                    ws.Cells[row, 34].Value = item.EstadoFlete;
                    ws.Cells[row, 35].Value = item.CantHoras;
                    ws.Cells[row, 36].Value = item.TarifaReferencia;
                    ws.Cells[row, 37].Value = item.TarifaReal;
                    ws.Cells[row, 38].Value = item.CtePagador;
                    ws.Cells[row, 39].Value = item.EstadoEnSAP;
                    ws.Cells[row, 40].Value = item.EstDestinoCambio;
                    ws.Cells[row, 41].Value = item.CteDestinatarioCambio;
                    ws.Cells[row, 42].Value = item.CodigoRespuestaEnvioSAP;
                    ws.Cells[row, 43].Value = item.CodigoRespuestaAnulacionSAP;
                    ws.Cells[row, 44].Value = item.PHumedad;
                    ws.Cells[row, 45].Value = item.POtros;
                    ws.Cells[row, 46].Value = item.CreateDate;
                    ws.Cells[row, 47].Value = item.CreatedBy;
                    ws.Cells[row, 48].Value = item.UpdateDate;
                    ws.Cells[row, 49].Value = item.UpdatedBy;
                }
            }

            #endregion

            return pck;
        }
    }
}
