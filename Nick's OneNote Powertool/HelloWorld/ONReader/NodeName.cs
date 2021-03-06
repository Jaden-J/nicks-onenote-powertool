﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    class NodeName : Attribute
    {
        public String Name {get; set;}
        public NodeName(String name)
        {
            Name = name;
        }
    }
}
