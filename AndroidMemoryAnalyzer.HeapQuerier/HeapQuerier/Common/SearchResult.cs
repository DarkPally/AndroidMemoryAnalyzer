using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public class SearchResult
    {
        public object Host { get; set; }
        public List<SearchResultItem> Items { get; set; }
    }
    public class SearchResultItem
    {
        public string Name { get; set; }
        public string CustomName { get; set; }
        public object value { get; set; }
    }
}
