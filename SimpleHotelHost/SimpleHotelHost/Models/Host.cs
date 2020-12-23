using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelHost.Models
{
    public class Host
    {
        public string hostId { get; set; }
        public string nickName { get; set; }
        public string realName { get; set; }
        public string password { get; set; }

        public Host(string hostId, string nickName, string realName, string password)
        {
            this.hostId = hostId;
            this.nickName = nickName;
            this.realName = realName;
            this.password = password;
        }

        public Host()
        {
        }
    }
}
