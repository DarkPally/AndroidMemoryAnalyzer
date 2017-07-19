using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace Demo.Library.QQ
{
    public class QQMessage
    {
        //nickName
        public string NickName { get; set; }

        //senderuin
        public string SenderUIN { get; set; }

        //selfuin
        public string SelfUIN { get; set; }

        //frienduin
        public string FriendUIN { get; set; }//群

        //msg
        public string Msg { get; set; }

        //time
        public long Time { get; set; }


        public DateTime DateTime 
        { 
            get 
            {
                var x = new DateTime(1970, 1, 1).AddTicks( Time);
                return x;
            }
        }

    }
}
