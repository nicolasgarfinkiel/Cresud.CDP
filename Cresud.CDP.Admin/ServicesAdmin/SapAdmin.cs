using System.Collections.Generic;
using System.Configuration;
using Cresud.CDP.Admin.SapPrefacturasService;
using Cresud.CDP.Dtos;
using Cresud.CDP.Entities;


namespace Cresud.CDP.Admin.ServicesAdmin
{
    public class SapAdmin
    {
        private static string _url = ConfigurationSettings.AppSettings["XIUrl"];
        private static string _user = ConfigurationSettings.AppSettings["XIUser"];
        private static string _pass = ConfigurationSettings.AppSettings["XIPassword"];

        public EstadoSap PrefacturaSap(SolicitudEdit solicitud, bool anula, bool cambioDestino)
        {
            #region Init

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => { return true; };

            var service = new re_prefacturas_out_async_MIBinding
            {
                Credentials = new System.Net.NetworkCredential(_user, _pass),
                Url = _url
            };

            var prefactura = new prefact_DT();
            var cabecera = new prefact_DTSAPSDLiquidacionCabecera();
            var cabeceraTextos = new prefact_DTSAPSDLiquidacionCabeceraTextos();
            var condicion = new prefact_DTSAPSDLiquidacionCondicion();
            var posicion = new prefact_DTSAPSDLiquidacionPosicion();
            var xcabecera = new List<prefact_DTSAPSDLiquidacionCabecera>();

            #endregion          

            #region SAPSDLiquidacionCabecera

            cabecera.Id_Tipo_Documento = "ZGKB";
            cabecera.Id_Liquidacion = solicitud.NumeroCartaDePorte;
            cabecera.SAP_Id_Operacion = solicitud.NumeroCartaDePorte;

            if (solicitud.FechaDeEmision.HasValue)
            {
                cabecera.SAP_Fecha_Emision = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
                cabecera.SAP_Fecha_Factura = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
                cabecera.SAP_Fecha_Prestacion = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
                cabecera.SAP_Fecha_valor = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
            }

            // TOMAR EN CUENTA QUE SEGUN TIPO DE CARTA DE PORTE VA A IR EL REMITENTE COMERCIAL EN LUGAR
            // DEL TITULAR DE LA CARTA DE PORTE
            // SI EL TITULAR NO ES EMPRESA VA EMPRESA y NO PROVEEDOR. (SOLO PARA SAP)
            var tipoCarta = new GeneralAdmin().GetTipoCartaById(solicitud.TipoDeCartaId.Value);

            switch (tipoCarta.Descripcion)
            {
                case "Venta de granos propios":
                    if (solicitud.ProveedorTitularCartaDePorte != null && (!string.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.SapId)))
                    {
                        var empresaTitular = new EmpresaAdmin().GetBySapId(solicitud.ProveedorTitularCartaDePorte.SapId);

                        if (empresaTitular != null && !string.IsNullOrEmpty(empresaTitular.IdSapOrganizacionDeVenta))
                        {
                            cabecera.SAP_Id_Sociedad = empresaTitular.IdSapOrganizacionDeVenta;
                        }

                        if (solicitud.ClienteIntermediario != null && !string.IsNullOrEmpty(solicitud.ClienteIntermediario.Id) && int.Parse(solicitud.ClienteIntermediario.Id) > 0)
                            cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteIntermediario.Id;
                        else if (solicitud.ClienteRemitenteComercial != null && !string.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.Id) && int.Parse(solicitud.ClienteRemitenteComercial.Id) > 0)
                            cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteRemitenteComercial.Id;
                        else if (solicitud.ClienteDestinatario != null && !string.IsNullOrEmpty(solicitud.ClienteDestinatario.Id) && int.Parse(solicitud.ClienteDestinatario.Id) > 0)
                            cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatario.Id;
                    }
                    break;
                case "Venta de granos de terceros":
                    if (solicitud.ClienteRemitenteComercial != null)
                    {
                        var empresa = new EmpresaAdmin().GetByClienteId(int.Parse(solicitud.ClienteRemitenteComercial.Id));

                        if (empresa != null)
                            cabecera.SAP_Id_Sociedad = empresa.IdSapOrganizacionDeVenta;
                    }

                    break;
                case "Compra de granos que transportamos":
                    break;
                case "Compra de granos":
                    break;
                case "Traslado de granos":
                case "Canje":
                    if (solicitud.ProveedorTitularCartaDePorte != null && (!string.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.SapId)))
                    {
                        var empresaTitular = new EmpresaAdmin().GetBySapId(solicitud.ProveedorTitularCartaDePorte.SapId);
                        
                        if (empresaTitular != null)
                        {
                            cabecera.SAP_Id_Sociedad = empresaTitular.IdSapOrganizacionDeVenta;
                        }
                    }
                    break;             
                case "Terceros por venta  de Granos de producción propia":
                    if (solicitud.ClienteRemitenteComercial != null && int.Parse(solicitud.ClienteRemitenteComercial.Id) > 0)
                    {
                        var empresa = new EmpresaAdmin().GetByClienteId(int.Parse(solicitud.ClienteRemitenteComercial.Id));

                        if (empresa != null)
                            cabecera.SAP_Id_Sociedad = empresa.IdSapOrganizacionDeVenta;
                    }

                    break;
                    
            }

            if (!string.Equals(tipoCarta.Descripcion, "Venta de granos propios") && solicitud.ClienteDestinatario != null)
                cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatario.Id;

            cabecera.SAP_Id_We_Destintario = solicitud.ClienteDestinatario == null ? string.Empty : solicitud.ClienteDestino.Id.PadLeft(10, '0');

            if (cambioDestino && solicitud.EstablecimientoDestinoCambio != null && solicitud.EstablecimientoDestinoCambio.InterlocutorDestinatario != null)
            {
                cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatarioCambio.Id;
                cabecera.SAP_Id_We_Destintario = solicitud.EstablecimientoDestinoCambio.InterlocutorDestinatario.Id.PadLeft(10, '0');
            }



            if (solicitud.ProveedorTransportista != null && solicitud.ProveedorTransportista.Id > 0
                && solicitud.EstadoFlete.HasValue && solicitud.EstadoFlete.Value.ToString() != "FletePagado")
                cabecera.SAP_Id_Tr_Transportista = solicitud.ProveedorTransportista.SapId;

            cabecera.SAP_Id_SubOperación = solicitud.NumeroContrato.ToString();
            cabecera.SAP_Id_Punto_de_Venta = "01";

            if (anula)
            {
                var codigoReferencia = solicitud.CodigoRespuestaEnvioSAP;
                if (string.IsNullOrEmpty(solicitud.CodigoRespuestaEnvioSAP))
                {
                    codigoReferencia = "9999999991";
                }

                cabecera.SAP_Id_Doc_Referencia = codigoReferencia;
                cabecera.SAP_Indicador_Doc_Refencia = "X";
            }

            cabecera.SAP_Id_Moneda = CDPSession.Current.Usuario.CurrentEmpresa.IdSapMoneda;

            cabecera.SAP_Tipo_de_Cambio = 1.000000M;
            cabecera.SAP_Id_Condicion_de_pago = "CTDO";
            cabecera.SAP_Id_Zs_Cedente = (solicitud.ClienteCorredor != null && int.Parse(solicitud.ClienteCorredor.Id) > 0) ? solicitud.ClienteCorredor.Id.PadLeft(10, '0') : string.Empty;

            // Definiciones
            if (tipoCarta.Descripcion.Equals("Traslado de granos"))
            {                
                cabecera.Id_Tipo_Documento = "ZTGR";
                cabecera.SAP_Id_SubOperación = solicitud.EstablecimientoProcedencia.IdCentroSap + "/" + solicitud.EstablecimientoProcedencia.IdAlmacenSap;
                cabecera.SAP_Id_Centro = solicitud.EstablecimientoProcedencia.IdCentroSap;
                cabecera.SAP_Id_SubOperación = solicitud.EstablecimientoProcedencia.IdAlmacenSap;
            }

            if (tipoCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
                solicitud.EstadoEnSAP == (int)EstadoSap.PrimerEnvioTerceros)
            {
                cabecera.Id_Tipo_Documento = "ZGKR";
                cabecera.SAP_Id_Ag_Solicitante = solicitud.EstablecimientoProcedencia.InterlocutorDestinatario.Id.PadLeft(10, '0');
                cabecera.SAP_Id_We_Destintario = solicitud.EstablecimientoProcedencia.InterlocutorDestinatario.Id.PadLeft(10, '0');
            }

            if (tipoCarta.Descripcion.Equals("Venta de granos propios"))
            {
                var sapIdAgSolicitante = string.Empty;

                //JIRA MDS-1282
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Inicio Pedido de cambio Normativa AFIP (Acopio)");
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Si figura Cresud en Remitente Comercial, el stock se debe consignar en el Destinatario.");
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Si figura Cresud en Intermediario, el stock se debe consignar en el Remitente Comercial.");
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 1.IdSolicitud {0}", solicitud.IdSolicitud);
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 1.Tipo {0}", solicitud.TipoDeCarta.Descripcion);

                //Si figura Cresud en Remitente Comercial, el stock se debe consignar en el Destinatario
                //Si figura Cresud en Intermediario, el stock se debe consignar en el Remitente Comercial
                //ES CRESUD => Lo cambio por le ClienteRemitenteComercial
                //Si el Remitente Comercial es null tiene que poner el Destinatario
                if (solicitud.ClienteRemitenteComercial != null && int.Parse(solicitud.ClienteRemitenteComercial.Id) > 0)
                {
                    var clienteId = int.Parse(solicitud.ClienteRemitenteComercial.Id);

                    if (clienteId == 1000005)
                    {
                        sapIdAgSolicitante = clienteId.ToString();
                        cabecera.SAP_Id_Ag_Solicitante = sapIdAgSolicitante.PadLeft(10, '0');
                    }
                    else if (clienteId == 1000005)
                    {
                        if (solicitud.ClienteRemitenteComercial == null)
                            sapIdAgSolicitante = solicitud.ClienteDestinatario.Id;
                        else
                            sapIdAgSolicitante = solicitud.ClienteRemitenteComercial.Id;

                        cabecera.SAP_Id_Ag_Solicitante = sapIdAgSolicitante.PadLeft(10, '0');
                    }
                }

                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.IdSolicitud {0}", solicitud.IdSolicitud);
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.Tipo {0}", solicitud.TipoDeCarta.Descripcion);
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.SapIdAgSolicitante es CRESUD {0}", cabecera.SAP_Id_Ag_Solicitante);
                //CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Fin Pedido de cambio Normativa AFIP (Acopio)");

                if (cambioDestino)
                {
                    cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatarioCambio == null ? string.Empty : solicitud.ClienteDestinatarioCambio.Id;
                }
            }
            xcabecera.Add(cabecera);
            prefactura.SAPSDLiquidacionCabecera = xcabecera.ToArray();

            #endregion

            #region SAPSDLiquidacionCabeceraTextos

            var xcabeceraTextos = new List<prefact_DTSAPSDLiquidacionCabeceraTextos>();
            cabeceraTextos.Texto = solicitud.Observaciones;
            cabeceraTextos.Id = "0019";
            xcabeceraTextos.Add(cabeceraTextos);
            prefactura.SAPSDLiquidacionCabeceraTextos = xcabeceraTextos.ToArray();

            #endregion

            #region SAPSDLiquidacionCondicion

            var xcondicion = new List<prefact_DTSAPSDLiquidacionCondicion>();

            if (solicitud.EstadoFlete.Value.ToString() != "FletePagado")
            {
                var totalImporteCondicion = solicitud.TarifaReal.HasValue ? solicitud.TarifaReal.Value : 0M;
                decimal totalKilogramos = 0;

                if (solicitud.CargaPesadaDestino.HasValue && solicitud.CargaPesadaDestino.Value)
                    totalKilogramos = solicitud.KilogramosEstimados.Value;
                else
                    totalKilogramos = solicitud.PesoNeto.Value;

                totalImporteCondicion = (solicitud.TarifaReal.Value * totalKilogramos) / 1000M;

                // Si el pagador del flete no es empresa enviar valor CERO.
                condicion.Importe_Condicion = 0;

                if (solicitud.ClientePagadorDelFlete != null && int.Parse(solicitud.ClientePagadorDelFlete.Id) > 0)
                {
                    var empresa = new EmpresaAdmin().GetByClienteId(int.Parse(solicitud.ClientePagadorDelFlete.Id));

                    if (empresa != null)
                        condicion.Importe_Condicion = totalImporteCondicion;
                }

                condicion.SAP_Id_Clase_de_Condicion = "Z001";
                //condicion.Porcentaje_Condicion = 0;
                //condicion.Porcentaje_No_Gravado = 0;

                condicion.SAP_Id_Moneda_Condicion = CDPSession.Current.Usuario.CurrentEmpresa.IdSapMoneda;

                condicion.Id_Posicion = "1";
                condicion.Id_Liquidacion = solicitud.NumeroCartaDePorte;

                xcondicion.Add(condicion);
            }

            prefactura.SAPSDLiquidacionCondicion = xcondicion.ToArray();

            #endregion

            #region SAPSDLiquidacionPosicion

            var xcuotas = new List<prefact_DTSAPSDLiquidacionCuotas>();
            prefactura.SAPSDLiquidacionCuotas = xcuotas.ToArray();
            var xposicion = new List<prefact_DTSAPSDLiquidacionPosicion>();

            posicion.Id_Liquidacion = solicitud.NumeroCartaDePorte;
            posicion.SAP_Id_Material = solicitud.Grano == null ? string.Empty : solicitud.Grano.IdMaterialSap;
            posicion.Id_Posicion = "1";

            if (solicitud.EstablecimientoProcedencia != null)
            {
                posicion.SAP_Id_CEBE = solicitud.EstablecimientoProcedencia.IdCEBE;
                posicion.SAP_id_Centro = solicitud.EstablecimientoProcedencia.IdCentroSap;
                posicion.SAP_id_Almacen = solicitud.EstablecimientoProcedencia.IdAlmacenSap;
                posicion.SAP_id_Expedicion = solicitud.EstablecimientoProcedencia.IdExpedicion;
            }

            posicion.SAP_Utilizacion = string.Empty;
            posicion.Id_Unidad = string.Empty;
            posicion.Agrupar_Cta_Cte = string.Empty;

            posicion.Referencia = string.Empty;

            if (tipoCarta.Descripcion.Equals("Traslado de granos"))
            {
                posicion.SAP_Id_CEBE = solicitud.EstablecimientoDestino.IdCEBE;
                posicion.SAP_id_Centro = solicitud.EstablecimientoDestino.IdCentroSap;
                posicion.SAP_id_Almacen = solicitud.EstablecimientoDestino.IdAlmacenSap;
                posicion.SAP_id_Expedicion = solicitud.EstablecimientoDestino.IdExpedicion;

                if (solicitud.CargaPesadaDestino.HasValue && solicitud.CargaPesadaDestino.Value)
                    posicion.Cantidad = solicitud.KilogramosEstimados.HasValue ? solicitud.KilogramosEstimados.Value :  0;
                else
                    posicion.Cantidad = solicitud.PesoNeto.Value;
            }
            else
            {
                if (solicitud.CargaPesadaDestino.HasValue && solicitud.CargaPesadaDestino.Value)
                    posicion.Peso = solicitud.KilogramosEstimados.HasValue ? solicitud.KilogramosEstimados.Value : 0;
                else
                    posicion.Peso = solicitud.PesoNeto.Value;

                posicion.Cantidad = 1;
            }

            xposicion.Add(posicion);
            prefactura.SAPSDLiquidacionPosicion = xposicion.ToArray();
            #endregion

            var xposicionTextos = new List<prefact_DTSAPSDLiquidacionPosicionTextos>();
            prefactura.SAPSDLiquidacionPosicionTextos = xposicionTextos.ToArray();

            var xreferencias = new List<prefact_DTSAPSDLiquidacionReferencia>();
            prefactura.SAPSDLiquidacionReferencia = xreferencias.ToArray();            

            try
            {
                service.re_prefacturas_out_async_MI(prefactura);
            }
            catch (System.Exception e)
            {
            }

            var result = default(EstadoSap);

            if (!anula)
            {
                if (tipoCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
                    solicitud.EstadoEnSAP == (int)EstadoSap.PrimerEnvioTerceros)
                    result = EstadoSap.PrimerEnvioTerceros;
                else
                    result = EstadoSap.EnProceso;
            }
            else
                result = EstadoSap.EnProcesoAnulacion;


            return result;
        }    
    }
}
