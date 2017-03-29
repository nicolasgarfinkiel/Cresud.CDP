using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Cresud.CDP.Admin.ServicesAdmin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;
using Solicitud = Cresud.CDP.Entities.Solicitud;

namespace Cresud.CDP.Admin
{
    public class SolicitudesAdmin : BaseAdmin<int, Entities.Solicitud, Dtos.SolicitudEdit, FilterBase>
    {
        private readonly AfipAdmin _afipAdmin;
        private readonly SapAdmin _sapAdmin;

        public SolicitudesAdmin()
        {
            _afipAdmin = new AfipAdmin();
            _sapAdmin = new SapAdmin();
        }

        #region Base

        public override Dtos.SolicitudEdit GetById(int id)
        {
            var entity = CdpContext.Solicitudes.Single(s => s.Id == id);
            var dto = Mapper.Map<Entities.Solicitud, Dtos.SolicitudEdit>(entity);

            if (entity.ProveedorTitularCartaDePorteId > 0)
            {
                dto.ProveedorTitularCartaDePorte = Mapper.Map<Entities.Proveedor, Dtos.Proveedor>(
                CdpContext.Proveedores.FirstOrDefault(e => e.Id == entity.ProveedorTitularCartaDePorteId.Value));
            }

            if (entity.ClienteIntermediarioId > 0)
            {
                var clienteId = entity.ClienteIntermediarioId.Value.ToString();
                dto.ClienteIntermediario = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteRemitenteComercialId > 0)
            {
                var clienteId = entity.ClienteRemitenteComercialId.Value.ToString();
                dto.ClienteRemitenteComercial = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteCorredorId > 0)
            {
                var clienteId = entity.ClienteCorredorId.Value.ToString();
                dto.ClienteCorredor = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteEntregadorId > 0)
            {
                var clienteId = entity.ClienteEntregadorId.Value.ToString();
                dto.ClienteEntregador = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteDestinatarioId > 0)
            {
                var clienteId = entity.ClienteDestinatarioId.Value.ToString();
                dto.ClienteDestinatario = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteDestinoId > 0)
            {
                var clienteId = entity.ClienteDestinoId.Value.ToString();
                dto.ClienteDestino = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ProveedorTransportistaId > 0)
            {
                dto.ProveedorTransportista = Mapper.Map<Entities.Proveedor, Dtos.Proveedor>(
                CdpContext.Proveedores.FirstOrDefault(e => e.Id == entity.ProveedorTransportistaId.Value));
            }

            if (entity.ChoferTransportistaId > 0)
            {
                dto.ChoferTransportista = Mapper.Map<Entities.Chofer, Dtos.Chofer>(
                CdpContext.Choferes.FirstOrDefault(e => e.Id == entity.ChoferTransportistaId.Value));
            }

            if (entity.ChoferId > 0)
            {
                dto.Chofer = Mapper.Map<Entities.Chofer, Dtos.Chofer>(
                CdpContext.Choferes.FirstOrDefault(e => e.Id == entity.ChoferId.Value));
            }

            if (entity.GranoId > 0)
            {
                dto.Grano = Mapper.Map<Entities.Grano, Dtos.Grano>(
                CdpContext.Granos.FirstOrDefault(e => e.Id == entity.GranoId.Value));
            }

            if (entity.EstablecimientoProcedenciaId > 0)
            {
                dto.EstablecimientoProcedencia = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(
                CdpContext.Establecimientos.FirstOrDefault(e => e.Id == entity.EstablecimientoProcedenciaId.Value));
                dto.EstablecimientoProcedencia.LocalidadDescripcion = Mapper.Map<Entities.Localidad, Dtos.Localidad>(CdpContext.Localidades.Single(e => e.Id == dto.EstablecimientoProcedencia.LocalidadId)).Descripcion;
            }

            if (entity.EstablecimientoDestinoId > 0)
            {
                dto.EstablecimientoDestino = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(
                CdpContext.Establecimientos.FirstOrDefault(e => e.Id == entity.EstablecimientoDestinoId.Value));
                dto.EstablecimientoDestino.LocalidadDescripcion = Mapper.Map<Entities.Localidad, Dtos.Localidad>(CdpContext.Localidades.Single(e => e.Id == dto.EstablecimientoDestino.LocalidadId)).Descripcion;

                dto.EmpresaDestino = new EmpresaAdmin().GetByClienteId(dto.EstablecimientoDestino.InterlocutorDestinatarioId);
            }


            if (entity.EstablecimientoDestinoCambioId > 0)
            {
                dto.EstablecimientoDestino = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(
                CdpContext.Establecimientos.FirstOrDefault(e => e.Id == entity.EstablecimientoDestinoCambioId.Value));
                dto.EstablecimientoDestino.LocalidadDescripcion = Mapper.Map<Entities.Localidad, Dtos.Localidad>(CdpContext.Localidades.Single(e => e.Id == dto.EstablecimientoDestino.LocalidadId)).Descripcion;
            }

            if (entity.ClientePagadorDelFleteId > 0)
            {
                var clienteId = entity.ClientePagadorDelFleteId.Value.ToString();
                dto.ClientePagadorDelFlete = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            return dto;
        }

        public override Dtos.SolicitudEdit Create(Dtos.SolicitudEdit dto)
        {
            dto.SetIds();
            var entity = Mapper.Map<Dtos.SolicitudEdit, Solicitud>(dto);
            entity.CreateDate = DateTime.Now;
            entity.CreatedBy = UsuarioLogged;

            if (!dto.Id.HasValue && (dto.TipoDeCartaId != 4 && dto.TipoDeCartaId != 2 && dto.TipoDeCartaId != 7))
            {
                var establecimiento = CdpContext.Establecimientos.Single(e => e.Id == dto.EstablecimientoProcedenciaId);
                var establecimientoId = establecimiento.Id.ToString();
                var empresa = CdpContext.Empresas.Single(e => e.Id == dto.EmpresaId);

                var cartaDePorte = (
                    from lote in CdpContext.LotesCartaPorte
                    join cdp in CdpContext.CartaDePortes on lote.Id equals cdp.Lote.Id
                    where cdp.GrupoEmpresaId == empresa.GrupoEmpresa.Id &&
                          cdp.Estado == 0 &&
                          lote.FechaVencimiento >= DateTime.Now &&
                          (!establecimiento.AsociaCartaDePorte || lote.EstablecimientoOrigenId == establecimientoId)
                    orderby lote.FechaVencimiento, cdp.NumeroCartaDePorte
                    select cdp
                    ).FirstOrDefault();

                if (cartaDePorte == null)
                    throw new Exception("No Hay Cartas de porte disponibles para el establecimiento seleccionado");

                cartaDePorte.Estado = 1;
                cartaDePorte.FechaReserva = DateTime.Now;
                cartaDePorte.UsuarioReserva = UsuarioLogged;

                entity.NumeroCartaDePorte = cartaDePorte.NumeroCartaDePorte;
                entity.Cee = cartaDePorte.NumeroCee;
            }


            entity.SetDefaultValues();
            CdpContext.Solicitudes.Add(entity);
            CdpContext.SaveChanges();

            if (dto.Enviar)
            {
                if (entity.TipoDeCartaId != 2 &&
                entity.TipoDeCartaId != 4 &&
                entity.TipoDeCartaId != 7 &&
                CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresaId == App.IdGrupoCresud)
                {
                    ReenviarAfip(entity.Id);
                }
                else
                {
                    entity.EstadoEnAFIP = (int)EstadoAfip.CargaManual;
                    entity.ObservacionAfip = "Carga Manual";
                }

                if (entity.TipoDeCartaId == 7)
                {
                    entity.EstadoEnSAP = (int)EstadoSap.Pendiente;
                }
            }

            CdpContext.SaveChanges();
            return Mapper.Map<Solicitud, Dtos.SolicitudEdit>(entity);
        }

        public override Dtos.SolicitudEdit Update(Dtos.SolicitudEdit dto)
        {
            dto.SetIds();
            var entity = CdpContext.Solicitudes.Single(s => s.Id == dto.Id);
            entity.UpdateDate = DateTime.Now;
            entity.UpdatedBy = UsuarioLogged;

            entity.CantHoras = dto.CantHoras;
            entity.CargaPesadaDestino = dto.CargaPesadaDestino;
            entity.ChoferId = dto.ChoferId;
            entity.ChoferTransportistaId = dto.ChoferTransportistaId;
            entity.ClienteCorredorId = dto.ClienteCorredorId;
            entity.ClienteDestinatarioCambioId = dto.ClienteDestinatarioCambioId;
            entity.ClienteDestinatarioId = dto.ClienteDestinatarioId;
            entity.ClienteDestinoId = dto.ClienteDestinoId;
            entity.ClienteEntregadorId = dto.ClienteEntregadorId;
            entity.ClienteIntermediarioId = dto.ClienteIntermediarioId;
            entity.ClientePagadorDelFleteId = dto.ClientePagadorDelFleteId;
            entity.ClienteRemitenteComercialId = dto.ClienteRemitenteComercialId;
            entity.ConformeCondicionalId = dto.ConformeCondicionalId;
            entity.CosechaId = dto.CosechaId;
            entity.DeclaracionDeCalidad = dto.DeclaracionDeCalidad;
            entity.EspecieId = dto.EspecieId;
            entity.EstablecimientoDestinoCambioId = dto.EstablecimientoDestinoCambioId;
            entity.EstablecimientoDestinoId = dto.EstablecimientoDestinoId;
            entity.EstablecimientoProcedenciaId = dto.EstablecimientoProcedenciaId;
            entity.EstadoEnAFIP = dto.EstadoEnAFIP;
            entity.EstadoEnSAP = dto.EstadoEnSAP;
            entity.EstadoFlete = dto.EstadoFlete;
            entity.FechaDeCarga = dto.FechaDeCarga;
            entity.FechaDeEmision = dto.FechaDeEmision;
            entity.FechaDeVencimiento = dto.FechaDeVencimiento;
            entity.GranoId = dto.GranoId;
            entity.KilogramosEstimados = dto.KilogramosEstimados;
            entity.KmRecorridos = dto.KmRecorridos;
            entity.LoteDeMaterial = dto.LoteDeMaterial;
            entity.NumeroContrato = dto.NumeroContrato;
            entity.Observaciones = dto.Observaciones;
            entity.PHumedad = dto.PHumedad;
            entity.POtros = dto.POtros;
            entity.PatenteAcoplado = dto.PatenteAcoplado;
            entity.PatenteCamion = dto.PatenteCamion;
            entity.PesoBruto = dto.PesoBruto;
            entity.PesoTara = dto.PesoTara;
            entity.ProveedorTitularCartaDePorteId = dto.ProveedorTitularCartaDePorteId;
            entity.ProveedorTransportistaId = dto.ProveedorTransportistaId;
            entity.RemitenteComercialComoCanjeador = dto.RemitenteComercialComoCanjeador;
            entity.SapContrato = dto.SapContrato;
            entity.SinContrato = dto.SinContrato;
            entity.TarifaReal = dto.TarifaReal;
            entity.TarifaReferencia = dto.TarifaReferencia;

            if (dto.Enviar)
            {
                if (entity.TipoDeCartaId != 2 &&
                entity.TipoDeCartaId != 4 &&
                entity.TipoDeCartaId != 7 &&
                CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresaId == App.IdGrupoCresud)
                {
                    ReenviarAfip(entity.Id);
                }
                else
                {
                    entity.EstadoEnAFIP = (int)EstadoAfip.CargaManual;
                    entity.ObservacionAfip = "Carga Manual";
                }

                if (entity.TipoDeCartaId == 7)
                {
                    entity.EstadoEnSAP = (int)EstadoSap.Pendiente;
                }
            }

            entity.SetDefaultValues();
            CdpContext.SaveChanges();

            return Mapper.Map<Solicitud, Dtos.SolicitudEdit>(entity);
        }

        public void UpdateSimple(Solicitud solicitud)
        {
            var entity = CdpContext.Solicitudes.Single(s => s.Id == solicitud.Id);
            entity.UpdateDate = DateTime.Now;
            entity.UpdatedBy = UsuarioLogged;
            entity.Ctg = solicitud.Ctg;
            entity.EstadoEnAFIP = solicitud.EstadoEnAFIP;
            entity.EstadoEnSAP = solicitud.EstadoEnSAP;
            entity.SetDefaultValues();

            CdpContext.SaveChanges();
        }

        public override Solicitud ToEntity(Dtos.SolicitudEdit dto)
        {
            return null;
        }

        public override void Validate(Dtos.SolicitudEdit dto)
        {
            if (!dto.Id.HasValue && (dto.TipoDeCartaId != 4 && dto.TipoDeCartaId != 2 && dto.TipoDeCartaId != 7))
            {

            }
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            var result = CdpContext.Choferes.Where(c => c.GrupoEmpresa.Id == filter.IdGrupoEmpresa).OrderBy(c => c.Nombre).ThenBy(c => c.Apellido).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r =>
                    (r.Nombre != null && r.Nombre.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Apellido != null && r.Apellido.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Cuit != null && r.Cuit.ToLower().Contains(filter.MultiColumnSearchText))).AsQueryable();
            }

            return result;
        }

        #endregion

        public Result ReenviarSap(int id)
        {
            var result = new Result { Messages = new List<string>() };
            var solicitud = CdpContext.Solicitudes.Single(s => s.Id == id);
            var solicitudEdit = GetById(id);

            solicitud.EstadoEnSAP = (int)_sapAdmin.PrefacturaSap(solicitudEdit, false, false);
            CdpContext.SaveChanges();

            return result;
        }

        public Result ReenviarAfip(int id)
        {
            var result = new Result { Messages = new List<string>() };
            var solicitudEdit = GetById(id);
            var solicitud = CdpContext.Solicitudes.Single(s => s.Id == id);
            var auth = CdpContext.AfipAuth.FirstOrDefault();

            try
            {
                solicitud.EstadoEnAFIP = (int)EstadoAfip.Enviado;

                var wsResult = _afipAdmin.SolicitarCtgInicial(solicitudEdit, auth);

                if (wsResult.arrayErrores.Length > 0)
                {
                    solicitud.EstadoEnAFIP = (int)EstadoAfip.SinProcesar;
                    result.HasErrors = true;
                    result.Messages = wsResult.arrayErrores.Select(NormalizarMensajeErrorAfip).ToList();
                }

                if (!string.IsNullOrEmpty(wsResult.observacion) && wsResult.observacion.Contains("CTG otorgado"))
                {
                    solicitud.EstadoEnAFIP = (int)EstadoAfip.Otorgado;
                }

                if (wsResult.datosSolicitarCTGResponse != null && wsResult.datosSolicitarCTGResponse.arrayControles != null && wsResult.datosSolicitarCTGResponse.arrayControles.Length > 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Reenvio manual. CONTROLES AFIP: ");

                    wsResult.datosSolicitarCTGResponse.arrayControles.ToList()
                        .ForEach(c =>
                            sb.AppendLine(string.Format("{0}: {1}", c.tipo, c.descripcion))
                         );

                    solicitud.EstadoEnAFIP = (int)EstadoAfip.SinProcesar;
                    solicitud.ObservacionAfip = sb.ToString();
                }


                //TODO: Envair mail
                // EnvioMailDAO.Instance.sendMail("<b>Envio de Solicitud a Afip</b> <br/><br/>" + solicitudGuardada.ObservacionAfip + "<br/>" + "Carta De Porte: " + solicitudGuardada.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardada.UsuarioCreacion);

                if (wsResult.datosSolicitarCTGResponse != null && wsResult.datosSolicitarCTGResponse.datosSolicitarCTG != null)
                {
                    solicitud.Ctg = wsResult.datosSolicitarCTGResponse.datosSolicitarCTG.ctg.ToString();
                    solicitud.EstadoEnAFIP = (int)EstadoAfip.Otorgado;

                    if (!String.IsNullOrEmpty(wsResult.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta))
                    {
                        var fecha = wsResult.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta.Split('/');
                        var fechaVigencia = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                        solicitud.FechaDeVencimiento = fechaVigencia;
                        solicitud.TarifaReferencia = wsResult.datosSolicitarCTGResponse.datosSolicitarCTG.tarifaReferencia;
                    }
                }


                CdpContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var norm = NormalizarMensajeErrorAfip(ex.Message);
                throw new Exception(norm);
            }

            return result;
        }

        public string NormalizarMensajeErrorAfip(string texto)
        {
            var mensaje = texto;

            if (texto.Contains("The connection was closed unexpectedly"))
                mensaje = "AFIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde. (" + texto + ")";

            if (texto.Contains("Service Temporarily Unavailable"))
                mensaje = "FIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde. (" + texto + ")";

            if (texto.Contains("JDBC Connection"))
                mensaje = "AFIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde. (" + texto + ")";

            if (texto.Contains("character string buffer too small"))
                mensaje = "Verifique el formato de los datos de patentes. (" + texto + ")";

            return mensaje;

        }
    }
}



