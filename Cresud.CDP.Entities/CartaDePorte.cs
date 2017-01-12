using System;

namespace Cresud.CDP.Entities
{
    public class CartaDePorte
    {
        public int Id { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public string NumeroCee { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaReserva { get; set; }
        public string UsuarioReserva { get; set; }
        public int GrupoEmpresaId { get; set; }
        public virtual LoteCartaPorte Lote { get; set; }

    }
}

