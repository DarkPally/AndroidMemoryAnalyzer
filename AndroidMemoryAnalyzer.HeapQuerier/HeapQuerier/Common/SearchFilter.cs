using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public enum OperotorType
    {
        [EnumDescription("==")]
        Equal,
        [EnumDescription(">=")]
        GreaterEqual,
        [EnumDescription("<=")]
        LessEqual,
        [EnumDescription(">")]
        Greater,
        [EnumDescription("<")]
        Less,
        [EnumDescription("startswith")]
        Startswith,
        [EnumDescription("contains")]
        Contains,
    };
    public class SearchFilter
    {
        [XmlAttribute("PropertyName")]
        public string Name { get; set; }

        [XmlAttribute("Operator")]
        public string Operator { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}
