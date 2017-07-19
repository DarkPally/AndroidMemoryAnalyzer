using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public abstract class SearchNode
    {
        [XmlElement("SearchParam")]
        public SearchParam SearchParam { get; set; }

        [XmlElement("NextSearchItems")]
        public List<SearchItem> NextSearchItems { get; set; }
    }
    public class SearchRootItem : SearchNode
    {

    }

    public class SearchItem : SearchNode
    {
        [XmlIgnore()]
        public SearchNode Parent { get; set; }
       
    }
}
