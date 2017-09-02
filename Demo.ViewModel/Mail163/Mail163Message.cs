using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace Demo.Library.Mail163
{
    public class Mail163Message
    {
        public string Theme { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Content { get; set; }

    }
}
