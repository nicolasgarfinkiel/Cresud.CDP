namespace Cresud.CDP.Entities
{
    public class LogOperacion: EntityBase
    {
        public int Id { get; set; }
        public string Tabla { get; set; }
        public string Accion { get; set; }
        public int ReferenciaId { get; set; }        
    }
}
