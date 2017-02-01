using System;

namespace Cresud.CDP.Entities
{
    public class CartaDePorteGraficoItem
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadAfip { get; set; }
        public int CantidadSap { get; set; }
    }
}
