using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cresud.SecurityServiceTest
{
    public class Group
    {
        public int Id { get; set; }
        
        /// <remarks/>
        public int IdApplication { get; set; }

        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public string Description { get; set; }
    }
}
