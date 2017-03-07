using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Entities;
using OfficeOpenXml;
using LoteCartaPorte = Cresud.CDP.Entities.LoteCartaPorte;

namespace Cresud.CDP.Admin
{
    public class LotesAdmin : BaseAdmin<int, Entities.LoteCartaPorte, Dtos.LoteCartaPorte, FilterLotesCartaPorte>
    {
        private static string _folder = ConfigurationManager.AppSettings["RutaOriginalCartaDePorte"];

        #region Base

        public override Dtos.LoteCartaPorte GetById(int id)
        {
            var entity = CdpContext.LotesCartaPorte.Single(d => d.Id == id);
            var dto = Mapper.Map<LoteCartaPorte, Dtos.LoteCartaPorte>(entity);

            if (!string.IsNullOrEmpty(dto.EstablecimientoOrigenId))
            {
                var establecimientoId = int.Parse(dto.EstablecimientoOrigenId);
                var establecimiento = CdpContext.Establecimientos.Single(e => e.Id == establecimientoId);
                dto.EstablecimientoOrigen = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(establecimiento);
            }

            return dto;
        }

        public override PagedListResponse<Dtos.LoteCartaPorte> GetByFilter(FilterLotesCartaPorte filter)
        {
            var query = GetQuery(filter).OfType<LoteCartaPorte>();
            var dtos = Mapper.Map<IList<LoteCartaPorte>, IList<Dtos.LoteCartaPorte>>(query.Skip(filter.PageSize * (filter.CurrentPage - 1)).Take(filter.PageSize).ToList());

            dtos.ToList().ForEach(d => d.Disponibles = CdpContext.CartaDePortes.Count(c => c.Estado == 0 && c.Lote.Id == d.Id));

            return new PagedListResponse<Dtos.LoteCartaPorte>
            {
                Count = query.Count(),
                Data = dtos
            };
        }

        public override IQueryable GetQuery(FilterLotesCartaPorte filter)
        {
            var result = CdpContext.LotesCartaPorte
                        .Where(c => c.GrupoEmpresa.Id == filter.IdGrupoEmpresa && c.Desde >= filter.Desde)
                        .OrderBy(c => c.Desde).AsQueryable();

            if (filter.TieneDisponibilidad)
            {
                result = result.Where(r => r.CartasDePorte.Any(c => c.Estado == 0)).AsQueryable();
            }

            if (filter.Vigente)
            {
                result = result.Where(r => r.FechaVencimiento >= DateTime.Now).AsQueryable();
            }

            if (filter.FechaDesde.HasValue)
            {
                result = result.Where(r => r.CreateDate >= filter.FechaDesde.Value).AsQueryable();
            }

            if (filter.FechaHasta.HasValue)
            {
                filter.FechaHasta = filter.FechaHasta.Value.AddDays(1).AddMilliseconds(-1);
                result = result.Where(r => r.CreateDate <= filter.FechaHasta.Value).AsQueryable();
            }

            return result;
        }

        public override Dtos.LoteCartaPorte Create(Dtos.LoteCartaPorte dto)
        {
            Validate(dto);
            var entity = ToEntity(dto);

            CdpContext.LotesCartaPorte.Add(entity);
            SaveFile(entity.Desde.Value);

            CdpContext.SaveChanges();
            CDPSession.Current.File = null;

            return Mapper.Map<LoteCartaPorte, Dtos.LoteCartaPorte>(entity);
        }

        public override LoteCartaPorte ToEntity(Dtos.LoteCartaPorte dto)
        {
            var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);

            var entity = new LoteCartaPorte
            {
                CreateDate = DateTime.Now,
                CreatedBy = UsuarioLogged,
                Enabled = true,
                GrupoEmpresa = grupoEmpresa,
                Cee = dto.Cee,
                Desde = dto.Desde,
                EstablecimientoOrigenId = dto.EstablecimientoOrigenId,
                FechaDesde = dto.FechaDesde,
                FechaHasta = dto.FechaHasta,
                FechaVencimiento = dto.FechaVencimiento,
                HabilitacionNumero = dto.HabilitacionNumero,
                Hasta = dto.Hasta,
                PuntoEmision = dto.PuntoEmision,
                Sucursal = dto.Sucursal,
                CartasDePorte = new List<CartaDePorte>()
            };

            var current = CdpContext.CartaDePortes
                        .Where(c => c.GrupoEmpresaId == grupoEmpresa.Id)
                        .ToList()
                        .Where(c => int.Parse(c.NumeroCartaDePorte) >= dto.Desde && int.Parse(c.NumeroCartaDePorte) <= dto.Hasta)
                        .ToList();

            while (dto.Desde <= dto.Hasta)
            {
                if (current.Any(c => int.Parse(c.NumeroCartaDePorte) == dto.Desde)) continue;

                entity.CartasDePorte.Add(new CartaDePorte
                {
                    Estado = 0,
                    GrupoEmpresaId = grupoEmpresa.Id,
                    Lote = entity,
                    NumeroCartaDePorte = dto.Desde.ToString(),
                    NumeroCee = dto.Cee
                });

                dto.Desde++;
            }

            return entity;
        }

        public override void Delete(int id)
        {
            var dto = GetById(id);
            var entity = CdpContext.LotesCartaPorte.Single(l => l.Id == id);
            dto.Disponibles = CdpContext.CartaDePortes.Count(c => c.Estado == 0 && c.Lote.Id == id);

            if (dto.Cantidad == dto.Disponibles)
            {
                var cdpIds = entity.CartasDePorte.Select(c => c.Id).ToList();

                cdpIds.ForEach(c =>
                {
                    var cdp = entity.CartasDePorte.Single(cc => cc.Id == c);
                    CdpContext.CartaDePortes.Remove(cdp);
                });

                CdpContext.LotesCartaPorte.Remove(entity);
            }
            else
            {
                var maxNroCartaPorte = entity.CartasDePorte.Where(c => c.Estado == 1).Select(c => int.Parse(c.NumeroCartaDePorte)).Max();
                var cdpIds = entity.CartasDePorte.Where(c => c.Estado == 0).Select(c => c.Id).ToList();

                cdpIds.ForEach(c =>
                {
                    var cdp = entity.CartasDePorte.Single(cc => cc.Id == c);
                    CdpContext.CartaDePortes.Remove(cdp);
                });

                entity.Hasta = maxNroCartaPorte;

                var log1 = new LogOperacion
                {
                    Tabla = "CartasDePorte",
                    Accion = "DELETE DISP",
                    ReferenciaId = id,
                    CreateDate = DateTime.Now,
                    CreatedBy = UsuarioLogged
                };

                var log2 = new LogOperacion
                {
                    Tabla = "LoteCartasDePorte",
                    Accion = "DELETE DISP",
                    ReferenciaId = id,
                    CreateDate = DateTime.Now,
                    CreatedBy = UsuarioLogged
                };

                CdpContext.LogOperaciones.Add(log1);
                CdpContext.LogOperaciones.Add(log2);
            }

            CdpContext.SaveChanges();
        }

        public override void Validate(Dtos.LoteCartaPorte dto)
        {
            var pageCount = GetPageCount();

            if (pageCount != dto.Cantidad)
            {
                throw new Exception("La cantidad de cartas de porte no coincide con el valor ingresado");
            }
        }

        #endregion

        public int GetCartasDePorteDisponibles(int grupoEmpresaId)
        {
            return CdpContext.CartaDePortes.Count(c => c.Lote.GrupoEmpresa.Id == grupoEmpresaId &&
                                                       c.Estado == 0 &&
                                                       c.Lote.FechaVencimiento >= DateTime.Now);
        }

        private int GetPageCount()
        {
            var pageCount = 0;
            CDPSession.Current.File.InputStream.Position = 0;

            using (var ms = new MemoryStream())
            {
                CDPSession.Current.File.InputStream.CopyTo(ms);
                var reader = new iTextSharp.text.pdf.PdfReader(ms.ToArray());
                pageCount = reader.NumberOfPages / 4;
                reader.Close();
            }

            return pageCount;
        }

        private void SaveFile(int desde)
        {
            var pageCount = GetPageCount();
            var currentPage = 1;
            CDPSession.Current.File.InputStream.Position = 0;

            using (var ms = new MemoryStream())
            {
                CDPSession.Current.File.InputStream.CopyTo(ms);
                var reader = new iTextSharp.text.pdf.PdfReader(ms.ToArray());

                for (var i = 1; i <= pageCount; i++)
                {
                    var newFile = string.Format("{0}{1}.pdf", _folder, desde);
                    var doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                    var pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(newFile, System.IO.FileMode.Create));

                    doc.Open();
                    for (var j = 1; j <= 4; j++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader, currentPage);
                        //pdfCpy.SetFullCompression();
                        pdfCpy.AddPage(page);
                        currentPage += 1;
                    }

                    desde++;
                    doc.Close();
                    pdfCpy.Close();                                       
                }

                reader.Close();
            }         
        }

        public  ExcelPackage Export(FilterLotesCartaPorte filter)
        {
            var data = GetQuery(filter).OfType<LoteCartaPorte>().ToList();
            var template = new FileInfo(String.Format(@"{0}\Reports\RangosCartaDePorte.xlsx", AppDomain.CurrentDomain.BaseDirectory));
            var pck = new ExcelPackage(template, true);
            var ws = pck.Workbook.Worksheets[1];
            var row = 4;
            var establecimientosId = data.Where(d => !string.IsNullOrEmpty(d.EstablecimientoOrigenId))
                                    .Select(d => d.EstablecimientoOrigenId)
                                    .Distinct()
                                    .ToList()
                                    .Select(int.Parse)
                                    .ToList();

            var establecimientos = CdpContext.Establecimientos.Where(e => establecimientosId.Contains(e.Id)).ToList();

            foreach (var lote in data)
            {
                row++;
                var establecimiento = string.IsNullOrEmpty(lote.EstablecimientoOrigenId) ? string.Empty : 
                                      establecimientos.Single(e => e.Id == int.Parse(lote.EstablecimientoOrigenId)).Descripcion;

                ws.Cells[row, 1].Value  = lote.Id;
                ws.Cells[row, 2].Value = lote.Desde;
                ws.Cells[row, 3].Value = lote.Hasta;
                ws.Cells[row, 4].Value = lote.Cee;
                ws.Cells[row, 5].Value = lote.CreateDate.HasValue ? lote.CreateDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                ws.Cells[row, 6].Value = lote.FechaVencimiento.HasValue ? lote.FechaVencimiento.Value.ToString("dd/MM/yyyy") : string.Empty;
                ws.Cells[row, 7].Value = lote.CreatedBy;
                ws.Cells[row, 8].Value = establecimiento;
            }

            return pck;
        }
    }
}
