namespace Cresud.CDP.Dtos
{
    public class Grano
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }        
        public string IdMaterialSap { get; set; }
        public string SujetoALote { get; set; }
        public int EspecieAfipId { get; set;  }
        public int TipoGranoAfipId { get; set; }
        public int CosechaAfipId { get; set; }
        public int GrupoEmpresaId { get; set; }
    }
}





