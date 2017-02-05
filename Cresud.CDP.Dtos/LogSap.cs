using System;

namespace Cresud.CDP.Dtos
{
    public class LogSap
    {
        public int Id { get; set; }
        public string IdDocumento { get; set; }
        public string Origen { get; set; }
        public string NroDocumentoRE { get; set; }
        public string NroDocumentoSap { get; set; }
        public string TipoMensaje { get; set; }
        public string TextoMensaje { get; set; }
        public int? NroEnvio { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
