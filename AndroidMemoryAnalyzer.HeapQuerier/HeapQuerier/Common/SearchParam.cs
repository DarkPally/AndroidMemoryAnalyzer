using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public enum SearchSourceType
    {
        [EnumDescription("Class")]
        Class,
        [EnumDescription("Object")]
        Object,
        [EnumDescription("Parent")]
        Parent
    };

    public class SearchParam
    {

        [XmlAttribute("SourceType")]
        public string SourceType { get; set; }

        [XmlArray("Filters")]
        [XmlArrayItem("Filter")]
        public List<SearchFilter> Filters { get; set; }

        [XmlAttribute("Limit")]
        public int Limit { get; set; }

        [XmlAttribute("Offset")]
        public int Offset { get; set; }

        [XmlArray("Orders")]
        [XmlArrayItem("Order")]
        public List<SearchOrder> Orders { get; set; }

        [XmlArray("TargetFormats")]
        [XmlArrayItem("TargetFormat")]
        public List<SearchTargetFormat> TargetFormats { get; set; }
    }
}
