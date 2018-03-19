﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.ModelClasses.DbSchema
{
    public class SchemaBaseClass
    {
        [JsonProperty(Order = 1)]
        string Name { get; set; }

        [JsonProperty(Order = 2)]
        string Description { get; set; }

    }
}
