using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public enum OrderType
    {
        [EnumDescription("asc")]
        Ascend,
        [EnumDescription("desc")]
        Descend,
    };
    public class SearchOrder
    {
        [XmlAttribute("PropertyName")]
        public string FieldName { get; set; }

        [XmlAttribute("Direction")]
        public string Direction { get; set; }//asc , desc
    }
}
