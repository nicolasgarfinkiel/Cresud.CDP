using System.Collections.Generic;

namespace Cresud.CDP.Dtos
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public IList<Empresa> Empresas { get; set; }
        public Empresa CurrentEmpresa { get; set; }
    }
}
