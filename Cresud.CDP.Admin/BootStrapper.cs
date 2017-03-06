using AutoMapper;
using Cresud.CDP.Dtos;
using CartaDePorteGraficoItem = Cresud.CDP.Entities.CartaDePorteGraficoItem;
using Chofer = Cresud.CDP.Entities.Chofer;
using Cliente = Cresud.CDP.Entities.Cliente;
using ClienteCorredor = Cresud.CDP.Entities.ClienteCorredor;
using ClienteDestinatario = Cresud.CDP.Entities.ClienteDestinatario;
using ClienteEntregador = Cresud.CDP.Entities.ClienteEntregador;
using ClienteIntermediario = Cresud.CDP.Entities.ClienteIntermediario;
using ClienteRemitenteComercial = Cresud.CDP.Entities.ClienteRemitenteComercial;
using Cosecha = Cresud.CDP.Entities.Cosecha;
using Empresa = Cresud.CDP.Entities.Empresa;
using Especie = Cresud.CDP.Entities.Especie;
using Establecimiento = Cresud.CDP.Entities.Establecimiento;
using Grano = Cresud.CDP.Entities.Grano;
using GrupoEmpresa = Cresud.CDP.Entities.GrupoEmpresa;
using Localidad = Cresud.CDP.Entities.Localidad;
using LogSap = Cresud.CDP.Entities.LogSap;
using LoteCartaPorte = Cresud.CDP.Entities.LoteCartaPorte;
using Pais = Cresud.CDP.Entities.Pais;
using Proveedor = Cresud.CDP.Entities.Proveedor;
using Provincia = Cresud.CDP.Entities.Provincia;
using Solicitud = Cresud.CDP.Entities.Solicitud;
using SolicitudRecibida = Cresud.CDP.Entities.SolicitudRecibida;
using SolicitudReport = Cresud.CDP.Entities.SolicitudReport;
using TipoCarta = Cresud.CDP.Entities.TipoCarta;
using TipoGrano = Cresud.CDP.Entities.TipoGrano;

namespace Cresud.CDP.Admin
{
    public static class BootStrapper
    {
        public static void BootStrap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Empresa, Dtos.Empresa>();
                cfg.CreateMap<GrupoEmpresa, Dtos.GrupoEmpresa>();
                cfg.CreateMap<Pais, Dtos.Pais>();
                cfg.CreateMap<Provincia, Dtos.Provincia>();
                cfg.CreateMap<Localidad, Dtos.Localidad>();
                cfg.CreateMap<Chofer, Dtos.Chofer>();
                cfg.CreateMap<Especie, Dtos.Especie>();
                cfg.CreateMap<Cosecha, Dtos.Cosecha>();
                cfg.CreateMap<TipoGrano, Dtos.TipoGrano>();
                cfg.CreateMap<Grano, Dtos.Grano>();
                cfg.CreateMap<Establecimiento, Dtos.Establecimiento>();
                cfg.CreateMap<Cliente, Dtos.Cliente>();
                cfg.CreateMap<LoteCartaPorte, Dtos.LoteCartaPorte>();
                cfg.CreateMap<Solicitud, Dtos.Solicitud>();
                cfg.CreateMap<Solicitud, Dtos.SolicitudEdit>();
                cfg.CreateMap<SolicitudEdit, Solicitud>();
                cfg.CreateMap<TipoCarta, Dtos.TipoCarta>();
                cfg.CreateMap<SolicitudReport, Dtos.SolicitudReport>();
                cfg.CreateMap<ClienteRemitenteComercial, Dtos.ClienteRemitenteComercial>();
                cfg.CreateMap<ClienteCorredor, Dtos.ClienteCorredor>();
                cfg.CreateMap<ClienteEntregador, Dtos.ClienteEntregador>();
                cfg.CreateMap<ClienteDestinatario, Dtos.ClienteDestinatario>();
                cfg.CreateMap<ClienteIntermediario, Dtos.ClienteIntermediario>();
                cfg.CreateMap<Proveedor, Dtos.Proveedor>();
                cfg.CreateMap<SolicitudRecibida, Dtos.SolicitudRecibida>();
                cfg.CreateMap<CartaDePorteGraficoItem, Dtos.CartaDePorteGraficoItem>();
                cfg.CreateMap<LogSap, Dtos.LogSap>();
            });         
        }
    }
}

