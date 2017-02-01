using System;

namespace Cresud.CDP.Dtos
{
    public class CartaDePorteGraficoItem
    {
        public long EmpresaId { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadAfip { get; set; }
        public int CantidadSap { get; set; }
    }
}
