using System;
using System.Configuration;
using System.Net;
using Cresud.CDP.Admin.CTGService_v3;
using Cresud.CDP.Dtos;
using Cresud.CDP.Entities;
using Org.BouncyCastle.Math.EC;

namespace Cresud.CDP.Admin.ServicesAdmin
{
    public class AfipAdmin
    {
        private static string _ctgServiceUrl = ConfigurationSettings.AppSettings["CTGServiceURL"];

        public solicitarCTGReturnType SolicitarCtgInicial(SolicitudEdit solicitud, AfipAuth afipAuth)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            var service = new CTGService_v30 {Proxy = WebProxy, Timeout = 3600000, Url = _ctgServiceUrl};

            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {                
                var retornarAfipHomo = new solicitarCTGReturnType();
                var response = new datosSolicitarCTGResponseType();
                var datos = new datosSolicitarCTGType();
                
                retornarAfipHomo.arrayErrores = new string[0];
                retornarAfipHomo.observacion = "CTG otorgado";
                retornarAfipHomo.datosSolicitarCTGResponse = response;
                retornarAfipHomo.datosSolicitarCTGResponse.datosSolicitarCTG = datos;
                var rnd = new Random();
                retornarAfipHomo.datosSolicitarCTGResponse.datosSolicitarCTG.ctg = Convert.ToInt64(rnd.Next(100000000, 999999999));

                return retornarAfipHomo;
            }

            var request = new solicitarCTGInicialRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = long.Parse(afipAuth.CuitRepresentado);

            request.datosSolicitarCTGInicial = new datosSolicitarCTGInicialType();

            request.datosSolicitarCTGInicial.cartaPorte = (long)Convert.ToDouble(solicitud.NumeroCartaDePorte);
            request.datosSolicitarCTGInicial.codigoEspecie = solicitud.Grano == null ? 0 : solicitud.Grano.EspecieAfipCodigo;

            if (solicitud.ClienteDestino != null && (!String.IsNullOrEmpty(solicitud.ClienteDestino.Cuit)))
                request.datosSolicitarCTGInicial.cuitDestino = (long)Convert.ToDouble(solicitud.ClienteDestino.Cuit);

            if (solicitud.ClienteDestinatario != null && (!String.IsNullOrEmpty(solicitud.ClienteDestinatario.Cuit)))
                request.datosSolicitarCTGInicial.cuitDestinatario = (long)Convert.ToDouble(solicitud.ClienteDestinatario.Cuit);

            request.datosSolicitarCTGInicial.codigoLocalidadOrigen = solicitud.EstablecimientoProcedencia == null ? 0 : solicitud.EstablecimientoProcedencia.LocalidadId;
            request.datosSolicitarCTGInicial.codigoLocalidadDestino = solicitud.EstablecimientoDestino == null ? 0 : solicitud.EstablecimientoDestino.LocalidadId;
            request.datosSolicitarCTGInicial.codigoCosecha = solicitud.Grano == null ? string.Empty : solicitud.Grano.CosechaAfipCodigo;

            if (solicitud.CargaPesadaDestino.HasValue && solicitud.CargaPesadaDestino.Value)
            {
                if (solicitud.KilogramosEstimados != null)
                    request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.KilogramosEstimados;
            }
            else if (solicitud.PesoNeto != null)
                request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.PesoNeto.Value;

            request.datosSolicitarCTGInicial.patente = solicitud.PatenteCamion;
            request.datosSolicitarCTGInicial.cantHorasSpecified = true;
            request.datosSolicitarCTGInicial.cantHoras = Convert.ToInt32(solicitud.CantHoras);

            if (solicitud.ClientePagadorDelFlete != null &&  int.Parse(solicitud.ClientePagadorDelFlete.Id) > 0 && new EmpresaAdmin().GetByClienteId(int.Parse(solicitud.ClientePagadorDelFlete.Id)) != null)
            {
                if (solicitud.ProveedorTransportista != null)
                {
                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ProveedorTransportista.NumeroDocumento); //27054888649;
                }
            }
            else
            {
                if (solicitud.ChoferTransportista != null)
                {
                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ChoferTransportista.Cuit); //27054888649;
                }
            }

            //Cambio solicitado por Maximiliano y Pedro
            //Si existe intermediario lo manda y no el comercial
            if (solicitud.ClienteIntermediario != null && !String.IsNullOrEmpty(solicitud.ClienteIntermediario.Cuit))
            {
                request.datosSolicitarCTGInicial.remitenteComercialComoCanjeador = (solicitud.RemitenteComercialComoCanjeador.HasValue && solicitud.RemitenteComercialComoCanjeador.Value ? "SI" : "NO");
                request.datosSolicitarCTGInicial.cuitCanjeadorSpecified = true;
                request.datosSolicitarCTGInicial.cuitCanjeador = (long)Convert.ToDouble(solicitud.ClienteIntermediario.Cuit);// 33504619589;
            }
            else if (solicitud.ClienteRemitenteComercial != null && !String.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.Cuit))
            {
                request.datosSolicitarCTGInicial.remitenteComercialComoCanjeador = (solicitud.RemitenteComercialComoCanjeador.HasValue && solicitud.RemitenteComercialComoCanjeador.Value ? "SI" : "NO");
                request.datosSolicitarCTGInicial.cuitCanjeadorSpecified = true;
                request.datosSolicitarCTGInicial.cuitCanjeador = (long)Convert.ToDouble(solicitud.ClienteRemitenteComercial.Cuit);// 33504619589;
            }

            //Si la procedencia es las vertientes debe 
            if (solicitud.EstablecimientoProcedencia.Descripcion != null && solicitud.EstablecimientoProcedencia.Descripcion.Contains("Las Vertientes") && solicitud.EstablecimientoProcedencia != null && solicitud.EstablecimientoProcedencia.Id == 72)
                request.datosSolicitarCTGInicial.remitenteComercialcomoProductor = "SI";

            request.datosSolicitarCTGInicial.kmARecorrer = (uint)Convert.ToDouble(solicitud.KmRecorridos);
            //PS 27052015
            request.datosSolicitarCTGInicial.cuitCorredorSpecified = false;
            if (solicitud.ClienteCorredor != null && !String.IsNullOrEmpty(solicitud.ClienteCorredor.Cuit))
            {
                request.datosSolicitarCTGInicial.cuitCorredor = long.Parse(solicitud.ClienteCorredor.Cuit);
                request.datosSolicitarCTGInicial.cuitCorredorSpecified = true;
            }
            
            return service.solicitarCTGInicial(request);
        }

        public operacionCTGReturnType ConfirmarArribo(SolicitudEdit solicitud, AfipAuth auth, string consumoPropio, long? establecimientoAfip)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            var service = new CTGService_v30 { Proxy = WebProxy, Timeout = 3600000, Url = _ctgServiceUrl };

            var request = new confirmarArriboRequestType();
            request.auth = new authType();
            request.auth.token = auth.Token;
            request.auth.sign = auth.Sign;
            request.auth.cuitRepresentado = long.Parse(auth.CuitRepresentado);
            request.datosConfirmarArribo = new datosConfirmarArriboType();
            request.datosConfirmarArribo.ctg = Convert.ToInt64(solicitud.Ctg);
            request.datosConfirmarArribo.cantKilosCartaPorte = (long)solicitud.KilogramosEstimados;
            request.datosConfirmarArribo.cartaPorte = Convert.ToInt64(solicitud.NumeroCartaDePorte);
            request.datosConfirmarArribo.consumoPropio = consumoPropio;            

            if (solicitud.ClientePagadorDelFlete != null && int.Parse(solicitud.ClientePagadorDelFlete.Id) > 0 && new EmpresaAdmin().GetByClienteId(int.Parse(solicitud.ClientePagadorDelFlete.Id)) != null)
            {
                if (solicitud.ProveedorTransportista != null)
                {
                    request.datosConfirmarArribo.cuitTransportista = (long)Convert.ToDouble(solicitud.ProveedorTransportista.NumeroDocumento); //27054888649;
                }
            }
            else
            {
                if (solicitud.ChoferTransportista != null)
                {
                    request.datosConfirmarArribo.cuitTransportista = (long)Convert.ToDouble(solicitud.ChoferTransportista.Cuit); //27054888649;
                }
            }

            if (establecimientoAfip.HasValue)
            {
                request.datosConfirmarArribo.establecimiento = establecimientoAfip.Value;
                request.datosConfirmarArribo.establecimientoSpecified = true;
            }

            return service.confirmarArribo(request);
        }

        public operacionCTGReturnType AnularCtg(long numeroCartaDePorte, long ctg, AfipAuth auth)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            var service = new CTGService_v30 { Proxy = WebProxy, Timeout = 3600000, Url = _ctgServiceUrl };

            var request = new anularCTGRequestType();
            request.auth = new authType();
            request.auth.token = auth.Token;
            request.auth.sign = auth.Sign;
            request.auth.cuitRepresentado = long.Parse(auth.CuitRepresentado);
            request.datosAnularCTG = new datosCTGType();
            request.datosAnularCTG.cartaPorte = numeroCartaDePorte;
            request.datosAnularCTG.ctg = ctg;

            return service.anularCTG(request);
        }

        public operacionCTGReturnType RegresarOrigenCtgRechazado(SolicitudEdit solicitu, AfipAuth auth)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            var service = new CTGService_v30 { Proxy = WebProxy, Timeout = 3600000, Url = _ctgServiceUrl };

            var request = new regresarAOrigenCTGRechazadoRequestType();
            request.auth = new authType();
            request.auth.token = auth.Token;
            request.auth.sign = auth.Sign;
            request.auth.cuitRepresentado = long.Parse(auth.CuitRepresentado);

            request.datosRegresarAOrigenCTGRechazado = new datosRegresarAOrigenCTGRechazadoType();
            request.datosRegresarAOrigenCTGRechazado.cartaPorte = Convert.ToInt64(solicitu.NumeroCartaDePorte);
            request.datosRegresarAOrigenCTGRechazado.ctg = Convert.ToInt64(solicitu.Ctg);
            request.datosRegresarAOrigenCTGRechazado.kmARecorrer = (uint)solicitu.KmRecorridos;

            return service.regresarAOrigenCTGRechazado(request);
        }
        

        #region NetworkCredential
        public NetworkCredential NetWorkCredential
        {
            get
            {
                return new NetworkCredential(ConfigurationSettings.AppSettings["UserProxy"], ConfigurationSettings.AppSettings["PassProxy"], ConfigurationSettings.AppSettings["Domain"]);
            }
        }

        public WebProxy WebProxy
        {
            get
            {
                var proxy = new WebProxy(ConfigurationSettings.AppSettings["Proxy"]);
                proxy.Credentials = NetWorkCredential;
                return proxy;
            }
        }
        #endregion      
    }
}
