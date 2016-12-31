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
            });

         
        }
    }
}

