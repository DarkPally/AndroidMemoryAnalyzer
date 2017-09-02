using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace Demo.Library.Mail189
{
    public class Mail189Message
    {
        public string Theme { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }

    }
}
