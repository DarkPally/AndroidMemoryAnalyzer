using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public enum TargetFormatValueType
    {
        [EnumDescription("ref")]
        Reference,
        [EnumDescription("int")]
        Int,
        [EnumDescription("string")]
        String,
    };
    public class SearchTargetFormat
    {
        [XmlAttribute("PropertyName")]
        public string Name { get; set; }

        [XmlAttribute("CustomName")]
        public string CustomName { get; set; }

        [XmlAttribute("ValueType")]
        public string ValueType { get; set; }

        [XmlAttribute("IsOutput")]
        public bool IsOutput { get; set; }
    }
}
