using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer
{
    public class QQGroup
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public int GroupFriendCount { get; set; }
        public int GroupOnlineFriendCount { get; set; }
        public List<QQFriend> GroupFriends { get; set; }
    }
}
