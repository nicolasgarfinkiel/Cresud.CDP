namespace Cresud.CDP.Dtos
{
    public class SolicitudEdit: Solicitud
    {
        public Proveedor ProveedorTitularCartaDePorte { get; set; }
        public Cliente ClienteIntermediario { get; set; }
        public Cliente ClienteRemitenteComercial { get; set; }
        public Cliente ClienteCorredor { get; set; }
        public Cliente ClienteEntregador { get; set; }
        public Cliente ClienteDestinatario { get; set; }
        public Cliente ClienteDestino { get; set; }
        public Proveedor ProveedorTransportista { get; set; }
        public Chofer ChoferTransportista { get; set; }
        public Chofer Chofer { get; set; }
        public Grano Grano { get; set; }
        public Establecimiento EstablecimientoProcedencia { get; set; }
        public Establecimiento EstablecimientoDestino { get; set; }
        public Establecimiento EstablecimientoDestinoCambio { get; set; }
        public Cliente ClientePagadorDelFlete { get; set; }
        public Cliente ClienteDestinatarioCambio { get; set; }
        public Empresa EmpresaDestino { get; set; }        

        public bool Enviar { get; set; }
        public bool Manual { get; set; }

        public void SetIds()
        {
            if (ProveedorTitularCartaDePorte != null)
            {
                ProveedorTitularCartaDePorteId = ProveedorTitularCartaDePorte.Id;
            }

            if (ClienteIntermediario != null)
            {
                ClienteIntermediarioId = int.Parse(ClienteIntermediario.Id);
            }

            if (ClienteRemitenteComercial != null)
            {
                ClienteRemitenteComercialId = int.Parse(ClienteRemitenteComercial.Id);
            }

            if (ClienteCorredor != null)
            {
                ClienteCorredorId = int.Parse(ClienteCorredor.Id);
            }

            if (ClienteEntregador != null)
            {
                ClienteEntregadorId = int.Parse(ClienteEntregador.Id);
            }

            if (ClienteDestinatario != null)
            {
                ClienteDestinatarioId = int.Parse(ClienteDestinatario.Id);
            }

            if (ClienteDestino != null)
            {
                ClienteDestinoId = int.Parse(ClienteDestino.Id);
            }

            if (ProveedorTransportista != null)
            {
                ProveedorTransportistaId = ProveedorTransportista.Id;
            }

            if (ChoferTransportista != null)
            {
                ChoferTransportistaId = ChoferTransportista.Id;
            }

            if (Chofer != null)
            {
                ChoferId = Chofer.Id;
            }

            if (Grano != null)
            {
                GranoId = Grano.Id;
            }

            if (EstablecimientoProcedencia != null)
            {
                EstablecimientoProcedenciaId = EstablecimientoProcedencia.Id;
            }

            if (EstablecimientoDestino != null)
            {
                EstablecimientoDestinoId = EstablecimientoDestino.Id;
            }

            if (EstablecimientoDestinoCambio != null)
            {
                EstablecimientoDestinoCambioId = EstablecimientoDestinoCambio.Id;
            }

            if (ClientePagadorDelFlete != null)
            {
                ClientePagadorDelFleteId = int.Parse(ClientePagadorDelFlete.Id);
            }

            if (ClienteDestinatarioCambio != null)
            {
                ClienteDestinatarioCambioId = int.Parse(ClienteDestinatarioCambio.Id);
            }

        }

    }
}

