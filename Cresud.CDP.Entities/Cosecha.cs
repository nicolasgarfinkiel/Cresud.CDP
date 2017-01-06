namespace Cresud.CDP.Entities
{
    public class Cosecha : EntityBase
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa { get; set; }
    }
}


