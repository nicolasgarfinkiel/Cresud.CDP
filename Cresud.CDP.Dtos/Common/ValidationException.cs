﻿using System;

namespace Cresud.CDP.Dtos.Common
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {

        }
    }
}
