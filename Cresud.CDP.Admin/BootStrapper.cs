using System.Collections.Generic;
using AutoMapper;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public static class BootStrapper
    {
        public static void BootStrap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Empresa, Dtos.Empresa>();
                cfg.CreateMap<GrupoEmpresa, Dtos.GrupoEmpresa>();
                cfg.CreateMap<Pais, Dtos.Pais>();
                cfg.CreateMap<Provincia, Dtos.Provincia>();
                cfg.CreateMap<Localidad, Dtos.Localidad>();
                cfg.CreateMap<Chofer, Dtos.Chofer>();
                cfg.CreateMap<Especie, Dtos.Especie>();
                cfg.CreateMap<Cosecha, Dtos.Cosecha>();
                cfg.CreateMap<TipoGrano, Dtos.TipoGrano>();
                cfg.CreateMap<Grano, Dtos.Grano>();
                cfg.CreateMap<Establecimiento, Dtos.Establecimiento>();
                cfg.CreateMap<Cliente, Dtos.Cliente>();
            });

         
        }
    }
}

