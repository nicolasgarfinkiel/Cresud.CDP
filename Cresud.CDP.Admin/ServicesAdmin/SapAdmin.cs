using System.Collections.Generic;
using System.Configuration;
using Cresud.CDP.Admin.SapPrefacturasService;
using Cresud.CDP.Dtos;


namespace Cresud.CDP.Admin.ServicesAdmin
{
    public class SapAdmin
    {
        private static string _url = ConfigurationSettings.AppSettings["XIUrl"];
        private static string _user = ConfigurationSettings.AppSettings["XIUser"];
        private static string _pass = ConfigurationSettings.AppSettings["XIPassword"];

        public int PrefacturaSap(SolicitudEdit solicitud, bool anula, bool cambioDestino)
        {
            return 0;
            //#region Init

            //System.Net.ServicePointManager.ServerCertificateValidationCallback =
            //    (sender, certificate, chain, sslPolicyErrors) => { return true; };

            //var service = new re_prefacturas_out_async_MIBinding
            //{
            //    Credentials = new System.Net.NetworkCredential(_user, _pass),
            //    Url = _url
            //};

            //var prefactura = new prefact_DT();
            //var cabecera = new prefact_DTSAPSDLiquidacionCabecera();
            //var cabeceraTextos = new prefact_DTSAPSDLiquidacionCabeceraTextos();
            //var condicion = new prefact_DTSAPSDLiquidacionCondicion();
            //var posicion = new prefact_DTSAPSDLiquidacionPosicion();
            //var xcabecera = new List<prefact_DTSAPSDLiquidacionCabecera>();

            //#endregion          

            //#region SAPSDLiquidacionCabecera

            //cabecera.Id_Tipo_Documento = "ZGKB";
            //cabecera.Id_Liquidacion = solicitud.NumeroCartaDePorte;
            //cabecera.SAP_Id_Operacion = solicitud.NumeroCartaDePorte;

            //if (solicitud.FechaDeEmision.HasValue)
            //{
            //    cabecera.SAP_Fecha_Emision = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
            //    cabecera.SAP_Fecha_Factura = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
            //    cabecera.SAP_Fecha_Prestacion = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
            //    cabecera.SAP_Fecha_valor = solicitud.FechaDeEmision.Value.Year + "" + solicitud.FechaDeEmision.Value.Month.ToString().PadLeft(2, '0') + "" + solicitud.FechaDeEmision.Value.Day.ToString().PadLeft(2, '0');
            //}

            //// TOMAR EN CUENTA QUE SEGUN TIPO DE CARTA DE PORTE VA A IR EL REMITENTE COMERCIAL EN LUGAR
            //// DEL TITULAR DE LA CARTA DE PORTE
            //// SI EL TITULAR NO ES EMPRESA VA EMPRESA y NO PROVEEDOR. (SOLO PARA SAP)

            //switch (solicitud.TipoDeCarta.Descripcion)
            //{
            //    case "Venta de granos propios":
            //        if (solicitud.ProveedorTitularCartaDePorte != null && (!String.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.Sap_Id)))
            //        {
            //            Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solicitud.ProveedorTitularCartaDePorte.Sap_Id);
            //            if (empresaTitular != null && !String.IsNullOrEmpty(empresaTitular.IdSapOrganizacionDeVenta))
            //            {
            //                cabecera.SAP_Id_Sociedad = empresaTitular.IdSapOrganizacionDeVenta;
            //            }

            //            if (solicitud.ClienteIntermediario != null && solicitud.ClienteIntermediario.IdCliente > 0)
            //                cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteIntermediario.IdCliente.ToString();
            //            else if (solicitud.ClienteRemitenteComercial != null && solicitud.ClienteRemitenteComercial.IdCliente > 0)
            //                cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteRemitenteComercial.IdCliente.ToString();
            //            else if (solicitud.ClienteDestinatario != null && solicitud.ClienteDestinatario.IdCliente > 0)
            //                cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatario.IdCliente.ToString();
            //        }
            //        break;
            //    case "Venta de granos de terceros":
            //        if (solicitud.ClienteRemitenteComercial != null)
            //        {
            //            if (solicitud.ClienteRemitenteComercial.EsEmpresa() && !String.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.getEmpresa().IdSapOrganizacionDeVenta))
            //                cabecera.SAP_Id_Sociedad = solicitud.ClienteRemitenteComercial.getEmpresa().IdSapOrganizacionDeVenta;
            //        }

            //        break;
            //    case "Compra de granos que transportamos":
            //        break;
            //    case "Compra de granos":
            //        break;
            //    case "Traslado de granos":
            //        if (solicitud.ProveedorTitularCartaDePorte != null && (!String.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.Sap_Id)))
            //        {
            //            Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solicitud.ProveedorTitularCartaDePorte.Sap_Id);
            //            if (empresaTitular != null && !String.IsNullOrEmpty(empresaTitular.IdSapOrganizacionDeVenta))
            //            {
            //                cabecera.SAP_Id_Sociedad = empresaTitular.IdSapOrganizacionDeVenta;
            //            }
            //        }
            //        break;
            //    case "Canje":
            //        if (solicitud.ProveedorTitularCartaDePorte != null && (!String.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.Sap_Id)))
            //        {
            //            Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solicitud.ProveedorTitularCartaDePorte.Sap_Id);
            //            if (empresaTitular != null && !String.IsNullOrEmpty(empresaTitular.IdSapOrganizacionDeVenta))
            //            {
            //                cabecera.SAP_Id_Sociedad = empresaTitular.IdSapOrganizacionDeVenta;
            //            }
            //        }
            //        break;
            //    case "Terceros por venta  de Granos de producción propia":
            //        if (solicitud.ClienteRemitenteComercial != null && solicitud.ClienteRemitenteComercial.IdCliente > 0)
            //        {
            //            if (solicitud.ClienteRemitenteComercial.EsEmpresa() && !String.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.getEmpresa().IdSapOrganizacionDeVenta))
            //                cabecera.SAP_Id_Sociedad = solicitud.ClienteRemitenteComercial.getEmpresa().IdSapOrganizacionDeVenta;
            //        }

            //        break;
            //    default:
            //        break;
            //}

            //if (solicitud.TipoDeCarta.Descripcion != "Venta de granos propios")
            //    cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatario.IdCliente.ToString();

            //cabecera.SAP_Id_We_Destintario = solicitud.ClienteDestino.IdCliente.ToString().PadLeft(10, '0');

            //if (cambioDestino && solicitud.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario != null)
            //{
            //    cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatarioCambio.IdCliente.ToString();
            //    cabecera.SAP_Id_We_Destintario = solicitud.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario.IdCliente.ToString().PadLeft(10, '0');
            //}



            //if (solicitud.ProveedorTransportista != null && solicitud.ProveedorTransportista.IdProveedor > 0
            //    && solicitud.EstadoFlete.Value.ToString() != "FletePagado")
            //    cabecera.SAP_Id_Tr_Transportista = solicitud.ProveedorTransportista.Sap_Id;

            //cabecera.SAP_Id_SubOperación = solicitud.NumeroContrato.ToString();
            //cabecera.SAP_Id_Punto_de_Venta = "01";

            //if (anula)
            //{
            //    String CodigoReferencia = solicitud.CodigoRespuestaEnvioSAP;
            //    if (String.IsNullOrEmpty(solicitud.CodigoRespuestaEnvioSAP))
            //    {
            //        CodigoReferencia = "9999999991";
            //    }

            //    cabecera.SAP_Id_Doc_Referencia = CodigoReferencia;
            //    cabecera.SAP_Indicador_Doc_Refencia = "X";
            //}

            //cabecera.SAP_Id_Moneda = App.Usuario.Empresa.IdSapMoneda;

            //cabecera.SAP_Tipo_de_Cambio = 1.000000M;
            //cabecera.SAP_Id_Condicion_de_pago = "CTDO";
            //cabecera.SAP_Id_Zs_Cedente = (solicitud.ClienteCorredor != null && solicitud.ClienteCorredor.IdCliente > 0) ? solicitud.ClienteCorredor.IdCliente.ToString().PadLeft(10, '0') : string.Empty;

            //// Definiciones
            //if (solicitud.TipoDeCarta.Descripcion.Equals("Traslado de granos"))
            //{
            //    //cabecera.Id_Tipo_Documento = (!anula) ? "ZTGR" : "ZGKR"; // SI es anulacion va con "ZGKR"
            //    cabecera.Id_Tipo_Documento = "ZTGR";
            //    cabecera.SAP_Id_SubOperación = solicitud.IdEstablecimientoProcedencia.IDCentroSAP + "/" + solicitud.IdEstablecimientoProcedencia.IDAlmacenSAP;
            //    cabecera.SAP_Id_Centro = solicitud.IdEstablecimientoProcedencia.IDCentroSAP.ToString();
            //    cabecera.SAP_Id_SubOperación = solicitud.IdEstablecimientoProcedencia.IDAlmacenSAP.ToString();

            //}

            //if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
            //    solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
            //{
            //    cabecera.Id_Tipo_Documento = "ZGKR";
            //    cabecera.SAP_Id_Ag_Solicitante = solicitud.IdEstablecimientoProcedencia.IdInterlocutorDestinatario.IdCliente.ToString().PadLeft(10, '0');
            //    cabecera.SAP_Id_We_Destintario = solicitud.IdEstablecimientoProcedencia.IdInterlocutorDestinatario.IdCliente.ToString().PadLeft(10, '0');
            //}

            //if (solicitud.TipoDeCarta.Descripcion.Equals("Venta de granos propios"))
            //{
            //    String SapIdAgSolicitante = string.Empty;

            //    //JIRA MDS-1282
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Inicio Pedido de cambio Normativa AFIP (Acopio)");
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Si figura Cresud en Remitente Comercial, el stock se debe consignar en el Destinatario.");
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Si figura Cresud en Intermediario, el stock se debe consignar en el Remitente Comercial.");
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 1.IdSolicitud {0}", solicitud.IdSolicitud);
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 1.Tipo {0}", solicitud.TipoDeCarta.Descripcion);

            //    //Si figura Cresud en Remitente Comercial, el stock se debe consignar en el Destinatario
            //    //Si figura Cresud en Intermediario, el stock se debe consignar en el Remitente Comercial
            //    //ES CRESUD => Lo cambio por le ClienteRemitenteComercial
            //    //Si el Remitente Comercial es null tiene que poner el Destinatario
            //    if (solicitud.ClienteRemitenteComercial != null && solicitud.ClienteRemitenteComercial.IdCliente > 0)
            //    {
            //        if (solicitud.ClienteRemitenteComercial.IdCliente == 1000005)
            //        {
            //            SapIdAgSolicitante = solicitud.ClienteDestinatario.IdCliente.ToString();
            //            cabecera.SAP_Id_Ag_Solicitante = SapIdAgSolicitante.ToString().PadLeft(10, '0');
            //        }
            //        else if (solicitud.ClienteIntermediario.IdCliente == 1000005)
            //        {
            //            if (solicitud.ClienteRemitenteComercial == null)
            //                SapIdAgSolicitante = solicitud.ClienteDestinatario.IdCliente.ToString();
            //            else
            //                SapIdAgSolicitante = solicitud.ClienteRemitenteComercial.IdCliente.ToString();

            //            cabecera.SAP_Id_Ag_Solicitante = SapIdAgSolicitante.ToString().PadLeft(10, '0');
            //        }
            //    }

            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.IdSolicitud {0}", solicitud.IdSolicitud);
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.Tipo {0}", solicitud.TipoDeCarta.Descripcion);
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 2.SapIdAgSolicitante es CRESUD {0}", cabecera.SAP_Id_Ag_Solicitante);
            //    CartaDePorte.Common.Tools.Logger.InfoFormat("201600408 Fin Pedido de cambio Normativa AFIP (Acopio)");

            //    if (cambioDestino)
            //    {
            //        cabecera.SAP_Id_Ag_Solicitante = solicitud.ClienteDestinatarioCambio.IdCliente.ToString();
            //    }
            //}
            //xcabecera.Add(cabecera);
            //prefactura.SAPSDLiquidacionCabecera = xcabecera.ToArray();

            //#endregion

            //#region SAPSDLiquidacionCabeceraTextos

            //var xcabeceraTextos = new List<prefact_DTSAPSDLiquidacionCabeceraTextos>();
            //cabeceraTextos.Texto = solicitud.Observaciones;
            //cabeceraTextos.Id = "0019";
            //xcabeceraTextos.Add(cabeceraTextos);
            //prefactura.SAPSDLiquidacionCabeceraTextos = xcabeceraTextos.ToArray();

            //#endregion

            //#region SAPSDLiquidacionCondicion

            //var xcondicion = new List<prefact_DTSAPSDLiquidacionCondicion>();

            //if (solicitud.EstadoFlete.Value.ToString() != "FletePagado")
            //{
            //    Decimal totalImporteCondicion = solicitud.TarifaReal;
            //    Decimal totalKilogramos = 0;

            //    if (solicitud.CargaPesadaDestino)
            //        totalKilogramos = solicitud.KilogramosEstimados;
            //    else
            //        totalKilogramos = solicitud.PesoNeto.Value;

            //    totalImporteCondicion = (Convert.ToDecimal(solicitud.TarifaReal) * totalKilogramos) / 1000M;

            //    // Si el pagador del flete no es empresa enviar valor CERO.
            //    condicion.Importe_Condicion = 0;

            //    if (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0)
            //    {
            //        if (solicitud.ClientePagadorDelFlete.EsEmpresa())
            //            condicion.Importe_Condicion = totalImporteCondicion;
            //    }

            //    condicion.SAP_Id_Clase_de_Condicion = "Z001";
            //    //condicion.Porcentaje_Condicion = 0;
            //    //condicion.Porcentaje_No_Gravado = 0;

            //    condicion.SAP_Id_Moneda_Condicion = App.Usuario.Empresa.IdSapMoneda;

            //    condicion.Id_Posicion = "1";
            //    condicion.Id_Liquidacion = solicitud.NumeroCartaDePorte;

            //    xcondicion.Add(condicion);
            //}

            //prefactura.SAPSDLiquidacionCondicion = xcondicion.ToArray();

            //#endregion

            //#region SAPSDLiquidacionPosicion

            //var xcuotas = new List<prefact_DTSAPSDLiquidacionCuotas>();
            //prefactura.SAPSDLiquidacionCuotas = xcuotas.ToArray();
            //var xposicion = new List<prefact_DTSAPSDLiquidacionPosicion>();

            //posicion.Id_Liquidacion = solicitud.NumeroCartaDePorte;
            //posicion.SAP_Id_Material = solicitud.Grano.IdMaterialSap;
            //posicion.Id_Posicion = "1";

            //posicion.SAP_Id_CEBE = solicitud.IdEstablecimientoProcedencia.IDCEBE;
            //posicion.SAP_id_Centro = solicitud.IdEstablecimientoProcedencia.IDCentroSAP.ToString();
            //posicion.SAP_id_Almacen = solicitud.IdEstablecimientoProcedencia.IDAlmacenSAP.ToString();
            //posicion.SAP_id_Expedicion = solicitud.IdEstablecimientoProcedencia.IDExpedicion;
            //posicion.SAP_Utilizacion = string.Empty;
            //posicion.Id_Unidad = string.Empty;
            //posicion.Agrupar_Cta_Cte = string.Empty;

            //posicion.Referencia = string.Empty;

            //if (solicitud.TipoDeCarta.Descripcion.Equals("Traslado de granos"))
            //{
            //    posicion.SAP_Id_CEBE = solicitud.IdEstablecimientoDestino.IDCEBE;
            //    posicion.SAP_id_Centro = solicitud.IdEstablecimientoDestino.IDCentroSAP.ToString();
            //    posicion.SAP_id_Almacen = solicitud.IdEstablecimientoDestino.IDAlmacenSAP.ToString();
            //    posicion.SAP_id_Expedicion = solicitud.IdEstablecimientoDestino.IDExpedicion;

            //    if (solicitud.CargaPesadaDestino)
            //        posicion.Cantidad = solicitud.KilogramosEstimados;
            //    else
            //        posicion.Cantidad = solicitud.PesoNeto.Value;
            //}
            //else
            //{
            //    if (solicitud.CargaPesadaDestino)
            //        posicion.Peso = solicitud.KilogramosEstimados;
            //    else
            //        posicion.Peso = solicitud.PesoNeto.Value;

            //    posicion.Cantidad = 1;
            //}

            //xposicion.Add(posicion);
            //prefactura.SAPSDLiquidacionPosicion = xposicion.ToArray();
            //#endregion

            //var xposicionTextos = new List<prefact_DTSAPSDLiquidacionPosicionTextos>();
            //prefactura.SAPSDLiquidacionPosicionTextos = xposicionTextos.ToArray();

            //var xreferencias = new List<prefact_DTSAPSDLiquidacionReferencia>();
            //prefactura.SAPSDLiquidacionReferencia = xreferencias.ToArray();            

            //try
            //{
            //    service.re_prefacturas_out_async_MI(prefactura);
            //}
            //catch (System.Exception e)
            //{
            //}


            //if (!anula)
            //{
            //    if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
            //        solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
            //        solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.PrimerEnvioTerceros;
            //    else
            //        solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.EnProceso;
            //}
            //else
            //    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.EnProcesoAnulacion;

            //SolicitudDAO.Instance.SaveOrUpdate(solicitud);
        }
    }
}
