using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotel.Models
{
    public class RoomInfo
    {
        public double score;
        public string location_detailed;
        public string clutterCharges;
        public int guestNum;
        public string intro;
        public string roomName;
        public int unitPrice;
        public string hostNickname;
        protected string roomId;
        protected string hostId;

        public RoomInfo(double score, string location_detailed, string clutterCharges, int guestNum, string intro, string roomName, int unitPrice, string hostNickname, string roomId, string hostId)
        {
            this.score = score;
            this.location_detailed = location_detailed;
            this.clutterCharges = clutterCharges;
            this.guestNum = guestNum;
            this.intro = intro;
            this.roomName = roomName;
            this.unitPrice = unitPrice;
            this.hostNickname = hostNickname;
            this.roomId = roomId;
            this.hostId = hostId;
        }
        public string hid()
        {
            return this.hostId;
        }
        public string rid()
        {
            return this.roomId;
        }
        public RoomInfo()
        {
        }
    }
}
