namespace Cresud.CDP.Entities
{
    public enum EstadoSap
    {
        Pendiente = 0,
        EnProceso = 1,
        FinalizadoOk = 2,
        FinalizadoConError = 3,
        Anulada = 4,
        PedidoAnulacion = 5,
        EnProcesoAnulacion = 6,
        NoEnviadaASap = 7,
        PrimerEnvioTerceros = 8,
        EnEsperaPorProspecto = 9
    }
}
