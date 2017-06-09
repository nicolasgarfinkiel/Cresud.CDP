using System.Data;
using Cresud.CDP.Dtos;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public static class DataSetConverter
    {
        public static DataSet GetDataSet(RemitoParaguay remito)
        {
            var result = new DataSet();
            var table = result.Tables.Add("RemitoCrescaDS");

            table.Columns.Add(new DataColumn("idsolicitud"));
            table.Columns.Add(new DataColumn("Descripcion"));
            table.Columns.Add(new DataColumn("Cee"));
            table.Columns.Add(new DataColumn("TranspRazonSocial"));
            table.Columns.Add(new DataColumn("TransportistaCUIT"));
            table.Columns.Add(new DataColumn("FechaCreacion"));
            table.Columns.Add(new DataColumn("FechaVencimiento"));
            table.Columns.Add(new DataColumn("NumeroRemision"));
            table.Columns.Add(new DataColumn("FechaDeEmision"));
            table.Columns.Add(new DataColumn("RazonSocial"));
            table.Columns.Add(new DataColumn("CUIT"));
            table.Columns.Add(new DataColumn("Direccion"));
            table.Columns.Add(new DataColumn("MotivoTraslado"));
            table.Columns.Add(new DataColumn("CteDeVta"));
            table.Columns.Add(new DataColumn("EPDireccion"));
            table.Columns.Add(new DataColumn("LocPartida"));
            table.Columns.Add(new DataColumn("ProvPartida"));
            table.Columns.Add(new DataColumn("EDDireccion"));
            table.Columns.Add(new DataColumn("LocLlegada"));
            table.Columns.Add(new DataColumn("ProvLlegada"));
            table.Columns.Add(new DataColumn("KmRecorridos"));
            table.Columns.Add(new DataColumn("PatenteCamion"));
            table.Columns.Add(new DataColumn("PatenteAcoplado"));
            table.Columns.Add(new DataColumn("ChoferRazonSocial"));
            table.Columns.Add(new DataColumn("ChoferCUIT"));
            table.Columns.Add(new DataColumn("ChoferDomicilio"));
            table.Columns.Add(new DataColumn("MarcaVehiculo"));
            table.Columns.Add(new DataColumn("Cantidad"));
            table.Columns.Add(new DataColumn("KG"));
            table.Columns.Add(new DataColumn("DescripcionDetallada"));
            table.Columns.Add(new DataColumn("HabilitacionNum"));

            var row = table.NewRow();

            row["idsolicitud"] = remito.Id.ToString();
            row["Descripcion"] = remito.Descripcion;
            row["Cee"] = remito.Cee;
            row["FechaCreacion"] = remito.FechaCreacion.Value.ToShortDateString();
            row["FechaVencimiento"] = remito.FechaVencimiento.HasValue ? remito.FechaVencimiento.Value.ToShortDateString() : string.Empty;
            row["NumeroRemision"] = remito.NumeroRemision;
            row["FechaDeEmision"] = remito.FechaDeEmision.HasValue ? remito.FechaDeEmision.Value.ToShortDateString() : string.Empty;
            row["RazonSocial"] = remito.RazonSocial;
            row["CUIT"] = remito.Cuit;
            row["Direccion"] = remito.Direccion;
            row["MotivoTraslado"] = remito.MotivoTraslado;
            row["CteDeVta"] = remito.CteDeVta;
            row["EPDireccion"] = remito.EPDireccion;
            row["LocPartida"] = remito.LocPartida;
            row["ProvPartida"] = remito.ProvPartida;
            row["EDDireccion"] = remito.EDDireccion;
            row["LocLlegada"] = remito.LocLlegada;
            row["ProvLlegada"] = remito.ProvLlegada;
            row["KmRecorridos"] = remito.KmRecorridos;
            row["PatenteCamion"] = remito.PatenteCamion;
            row["PatenteAcoplado"] = remito.PatenteAcoplado;
            row["ChoferRazonSocial"] = remito.ChoferRazonSocial;
            row["ChoferCUIT"] = remito.ChoferCuit;
            row["ChoferDomicilio"] = remito.ChoferDomicilio;
            row["MarcaVehiculo"] = remito.MarcaVehiculo;
            row["Cantidad"] = remito.Cantidad;
            row["KG"] = remito.Kg;
            row["DescripcionDetallada"] = remito.DescripcionDetallada;
            row["TranspRazonSocial"] = remito.TranspRazonSocial;
            row["TransportistaCUIT"] = remito.TransportistaCuit;
            row["HabilitacionNum"] = remito.HabilitacionNum;

            table.Rows.Add(row);            

            return result;
        }

        public static DataSet GetDataSet(SolicitudEdit solicitud)
        {
            var result = new DataSet();
            var table = result.Tables.Add("SolicitudDS");

            table.Columns.Add(new DataColumn("SolicitudId"));
            table.Columns.Add(new DataColumn("NumeroCartaDePorte"));
            table.Columns.Add(new DataColumn("FechaDeCarga"));

            table.Columns.Add(new DataColumn("Grano_Tipo"));
            table.Columns.Add(new DataColumn("Grano_Peso"));
            table.Columns.Add(new DataColumn("Grano_Observaciones"));

            table.Columns.Add(new DataColumn("Procedencia_Direccion"));
            table.Columns.Add(new DataColumn("Procedencia_Localidad"));
            table.Columns.Add(new DataColumn("Procedencia_Provincia"));

            table.Columns.Add(new DataColumn("Transporte_Nombre"));
            table.Columns.Add(new DataColumn("Transporte_Cuit"));
            table.Columns.Add(new DataColumn("Transporte_Domicilio"));
            table.Columns.Add(new DataColumn("Transporte_Camion"));
            table.Columns.Add(new DataColumn("Transporte_KmRecorridos"));            
            table.Columns.Add(new DataColumn("Transporte_Acoplado"));

            table.Columns.Add(new DataColumn("Chofer_Nombre"));
            table.Columns.Add(new DataColumn("Chofer_Cuit"));

            table.Columns.Add(new DataColumn("Destinatario_Nombre"));
            table.Columns.Add(new DataColumn("Destinatario_Cuit"));
            table.Columns.Add(new DataColumn("Destinatario_Domicilio"));

            table.Columns.Add(new DataColumn("Destino_Direccion"));
            table.Columns.Add(new DataColumn("Destino_Localidad"));
            table.Columns.Add(new DataColumn("Destino_Provincia"));

            var row = table.NewRow();

            row["SolicitudId"] = solicitud.Id.ToString();
            row["NumeroCartaDePorte"] = solicitud.NumeroCartaDePorte;
            row["FechaDeCarga"] = solicitud.FechaDeCarga.HasValue ? solicitud.FechaDeCarga.Value.ToShortDateString() : string.Empty;

            //Ver remitente comercial            

            if (solicitud.Grano != null)
            {
                row["Grano_Tipo"] = string.Format("{0} / {1}", solicitud.Grano.Descripcion, solicitud.Grano.TipoGranoAfipDescripcion);
                row["Grano_Peso"] = solicitud.PesoNeto;
                row["Grano_Observaciones"] = solicitud.Observaciones;
            }

            if (solicitud.EstablecimientoProcedencia != null)
            {
                row["Procedencia_Direccion"] = solicitud.EstablecimientoProcedencia.Direccion;
                row["Procedencia_Localidad"] = solicitud.EstablecimientoProcedencia.LocalidadDescripcion;
                row["Procedencia_Provincia"] = solicitud.EstablecimientoProcedencia.ProvinciaDescripcion;
            }

            if (solicitud.ClientePagadorDelFlete != null)
            {
                row["Transporte_Nombre"] = solicitud.ClientePagadorDelFlete.RazonSocial;
                row["Transporte_Cuit"] = solicitud.ClientePagadorDelFlete.Cuit;
                row["Transporte_Domicilio"] = string.Format("Calle: {0}. Número: {1}. Piso: {2}. C.P.: {3}.",
                    solicitud.ClientePagadorDelFlete.Calle, solicitud.ClientePagadorDelFlete.Numero,
                    solicitud.ClientePagadorDelFlete.Piso, solicitud.ClientePagadorDelFlete.Cp);
            }

            row["Transporte_Camion"] = solicitud.PatenteCamion;
            row["Transporte_Acoplado"] = solicitud.PatenteAcoplado;
            row["Transporte_KmRecorridos"] = solicitud.KmRecorridos;

            if (solicitud.Chofer != null)
            {
                row["Chofer_Nombre"] = solicitud.Chofer.Nombre;
                row["Chofer_Cuit"] = solicitud.Chofer.Cuit;
            }

            if (solicitud.ClienteDestinatario != null)
            {
                row["Destinatario_Nombre"] = solicitud.ClienteDestinatario.RazonSocial;
                row["Destinatario_Cuit"] = solicitud.ClienteDestinatario.Cuit;
                row["Destinatario_Domicilio"] = string.Format("Calle: {0}. Número: {1}. Piso: {2}. C.P.: {3}.",
                    solicitud.ClienteDestinatario.Calle, solicitud.ClienteDestinatario.Numero,
                    solicitud.ClienteDestinatario.Piso, solicitud.ClienteDestinatario.Cp);
            }

            if (solicitud.EstablecimientoProcedencia != null)
            {
                row["Destino_Direccion"] = solicitud.EstablecimientoDestino.Direccion;
                row["Destino_Localidad"] = solicitud.EstablecimientoDestino.LocalidadDescripcion;
                row["Destino_Provincia"] = solicitud.EstablecimientoDestino.ProvinciaDescripcion;
            }
                       
            table.Rows.Add(row);

            return result;
        }
    }
}