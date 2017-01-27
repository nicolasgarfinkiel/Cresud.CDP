using System;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Entities
{
  
							  
            row.Cells.Add(AddTitleCell("Nro. Carta de Porte", 20));
            row.Cells.Add(AddTitleCell("Número de CEE", 20));
            row.Cells.Add(AddTitleCell("Número de CTG", 20));
            row.Cells.Add(AddTitleCell("Fecha de Carga", 20));
            row.Cells.Add(AddTitleCell("CUIT Titular Carta de Porte", 20));
            row.Cells.Add(AddTitleCell("CUIT Intermediario", 20));
            row.Cells.Add(AddTitleCell("CUIT del Remitente Comercial", 20));
            row.Cells.Add(AddTitleCell("CUIT Corredor", 20));
            row.Cells.Add(AddTitleCell("CUIT Representante Entregador", 20));
            row.Cells.Add(AddTitleCell("CUIT Destinario", 20));
            row.Cells.Add(AddTitleCell("CUIT Establecimiento Destino", 20));
            row.Cells.Add(AddTitleCell("CUIT Transportista", 20));
            row.Cells.Add(AddTitleCell("CUIT/CUIL del Chofer / Conductor", 20));
            row.Cells.Add(AddTitleCell("Cosecha", 20));
            row.Cells.Add(AddTitleCell("Código de Especie", 20));
            row.Cells.Add(AddTitleCell("Tipo de Grano", 20));
            row.Cells.Add(AddTitleCell("Contrato / Boleta Compra - Venta", 20));
            row.Cells.Add(AddTitleCell("Tipo de Pesado", 20));
            row.Cells.Add(AddTitleCell("Peso Neto de Carga/Peso Total Despachado (Kg)", 20));

            row.Cells.Add(AddTitleCell("Código de Establecimiento de Procedencia", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Procedencia", 20));
            row.Cells.Add(AddTitleCell("Código de Establecimiento Destino", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Destino", 20));
            row.Cells.Add(AddTitleCell("Km a Recorrer", 20));
            row.Cells.Add(AddTitleCell("Patente del Camión", 20));
            row.Cells.Add(AddTitleCell("Acoplado Patente", 20));
            row.Cells.Add(AddTitleCell("Tarifa por Tonelada", 20));

            row.Cells.Add(AddTitleCell("Fecha de Descarga", 20));
            row.Cells.Add(AddTitleCell("Fecha de Arribo a Destino/Redestino", 20));
            row.Cells.Add(AddTitleCell("Peso Neto de Descarga", 20));
            row.Cells.Add(AddTitleCell("CUIT Establecimiento Redestino", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Redestino", 20));
            row.Cells.Add(AddTitleCell("Código de Establecimiento de Redestino", 20));

            row.Cells.Add(AddTitleCell("Flete-Tarifa de Referencia", 20));
	




    public class SolicitudRecibida : EntityBase
    {
        public int EmpresaId { get; set; }
        public int? TipoCartaId { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public string Cee { get; set; }
        public string Ctg { get; set; }
        public DateTime? FechaDeCarga { get; set; }
        public string ProveedorTitularCartaDePorteCuit { get; set; }
        public string ClienteIntermediarioCuit { get; set; }
        public string ClienteRemitenteComercialCuit { get; set; }
        public string ClienteCorredorCuit { get; set; }
        public string ClienteEntregadorCuit { get; set; }
        public string ClienteDestinatarioCuit { get; set; }
        public string ClienteDestinoCuit { get; set; }
        public string ProveedorTransportistaCuit { get; set; }
        public string ChoferCuit { get; set; }
        public virtual Grano Grano { get; set; }
        public string NumeroContrato { get; set; }
        public bool? CargaPesadaDestino { get; set; }
        public decimal?  KilogramosEstimados { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoTara { get; set; }



        public decimal? PesoNeto
        {
            get { return PesoBruto - PesoTara; }
        }
    }
}


