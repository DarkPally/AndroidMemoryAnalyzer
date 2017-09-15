using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace Demo.Library.BaiduPan
{
    public class BaiduPanMessage
    {
        public string ID { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
