using SimpleHotelHost.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SimpleHotelHost
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccessOrderedRoom : Page
    {
        public static ObservableCollection<DisplayOrder> listOfInfo;
        public AccessOrderedRoom()
        {
            //下单时间Orders.IssueTime 状态Orders.orderStatus 位置Rooms.LocationDetailed 房间名Rooms.RoomName
            this.InitializeComponent();
            listOfInfo = new ObservableCollection<DisplayOrder>();
            InventoryList.ItemsSource = listOfInfo;
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串 
            string gid = App.usingHost.hostId;
            StringBuilder query = new StringBuilder("select Orders.IssueTime,Orders.OrderStatus,Rooms.LocationDetailed,Rooms.RoomName from Orders,Rooms WHERE Orders.HostId='");
            query.Append(gid);
            query.Append("' and Orders.RoomId=Rooms.RoomId");
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query.ToString(), mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int maxIndex = dt.Rows.Count;
            for (int i = 0; i < maxIndex; i++)
            {
                DisplayOrder tmp = new DisplayOrder(dt.Rows[i]["IssueTime"].ToString(), dt.Rows[i]["LocationDetailed"].ToString(),
                dt.Rows[i]["RoomName"].ToString(), dt.Rows[i]["OrderStatus"].ToString());
                tmp.translateStatus();
                listOfInfo.Add(tmp);

            }
            mycon.Close();
            myda.Dispose();

        }

    }
}
