using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotel.Models
{
    class Order
    {
        protected string orderId;
        public DateTime issueTime;
        public string guestId;
        public string hostId;
        public string roomId;
        public int status;

        public Order(DateTime issueTime, string guestId, string hostId, string roomId)
        {
            this.issueTime = issueTime;
            this.guestId = guestId;
            this.hostId = hostId;
            this.roomId = roomId;
            this.generateId();
            this.status = 1;
        }

        public void generateId() {
            if (string.IsNullOrEmpty(orderId)) {
                orderId=Guid.NewGuid().ToString();
            }
            else
            {
                ;
            }
        }
    }
}
