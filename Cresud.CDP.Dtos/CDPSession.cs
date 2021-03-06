﻿using Cresud.CDP.Infrastructure;

namespace Cresud.CDP.Dtos
{
    public class CDPSession : SessionInfo<CDPSession>
    {
        public Usuario Usuario { get; set; }

        public System.Web.HttpPostedFileBase File { get; set; }
    }
}