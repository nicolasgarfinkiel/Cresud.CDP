﻿using System.Runtime.Serialization;

namespace Cresud.CDP.Security.Service.Dtos
{
    [DataContract(Name = "Permission", Namespace = "")]
    public class Permission
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public int IdApplication { get; set; }

        [DataMember(IsRequired = true)]
        public string Description { get; set; }
    }
}
