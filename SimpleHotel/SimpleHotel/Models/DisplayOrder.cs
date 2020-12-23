using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotel.Models
{
    //位置状态房间名下单时间
    public class DisplayOrder
    {
        public string issuedTime;
        public string locationDetail;
        public string roomName;
        public string status;

        public DisplayOrder(string issuedTime, string locationDetail, string roomName, string status)
        {
            this.issuedTime = issuedTime;
            this.locationDetail = locationDetail;
            this.roomName = roomName;
            this.status = status;
        }

        public void translateStatus()
        {
            //订单状态：1经营者手动为客户锁定尚未付款2客户已经付款尚未使用完毕(即锁定)3已取消4已完成（客人退房并接收押金）
            if ("1".Equals(status)) {
                status = "已锁定请尽快付款";
            }
            else if ("2".Equals(status))
            {
                status = "已经付款尚未使用完毕";
            }
            else if ("3".Equals(status))
            {
                status = "取消";
            }
            else if ("4".Equals(status))
            {
                status = "已经完成";
            }
            else
            {
                ;
            }
        }
    }
}
