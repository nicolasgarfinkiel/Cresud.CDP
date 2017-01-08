using System;

namespace Cresud.CDP.Entities
{
    public class Cliente
    {
        public string Id { get; set; }
        public int IdSapOrganizacionDeVenta { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Cuit { get; set; }
        public int IdTipoDocumentoSap { get; set; }
        public int? IdClientePrincipal { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Dto { get; set; }
        public string Piso { get; set; }
        public string Cp { get; set; }
        public string Poblacion { get; set; }
        public string GrupoComercial { get; set; }
        public string ClaveGrupo { get; set; }
        public string Tratamiento { get; set; }
        public string DescripcionGe { get; set; }
        public bool EsProspecto { get; set; }        
        public DateTime? CreateDate { get; set; }
        public bool Enabled { get; set; }
    }
}

