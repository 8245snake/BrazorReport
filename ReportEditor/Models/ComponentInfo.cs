using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ComponentInfo
    {
        public string ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
