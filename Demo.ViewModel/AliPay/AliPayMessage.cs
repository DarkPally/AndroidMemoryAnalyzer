using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace Demo.Library.AliPay
{
    public class AliPayMessage
    {
        public string Json { get; set; }
        public string Content { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public string Money { get; set; }
    }
}
