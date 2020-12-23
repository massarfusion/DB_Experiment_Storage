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
    public sealed partial class AccessRoomsManage : Page
    {
        public static ObservableCollection<DisplayOrder> listOfInfo;
        public AccessRoomsManage()
        {
            this.InitializeComponent();
            
            //下单时间Orders.IssueTime 状态Orders.orderStatus 位置Rooms.LocationDetailed 房间名Rooms.RoomName
            
            listOfInfo = new ObservableCollection<DisplayOrder>();
            InventoryList.ItemsSource = listOfInfo;
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串 
            string gid = App.usingHost.hostId;
            StringBuilder query = new StringBuilder("select Rooms.UnitPrice,Rooms.Score,Rooms.LocationDetailed,Rooms.RoomName from Rooms WHERE Rooms.HostId='");
            query.Append(gid);
            query.Append("'");
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query.ToString(), mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int maxIndex = dt.Rows.Count;
            for (int i = 0; i < maxIndex; i++)
            {
                DisplayOrder tmp = new DisplayOrder(dt.Rows[i]["UnitPrice"].ToString(), dt.Rows[i]["LocationDetailed"].ToString(),
                dt.Rows[i]["RoomName"].ToString(), dt.Rows[i]["Score"].ToString());
                tmp.translateStatus();
                listOfInfo.Add(tmp);

            }
            mycon.Close();
            myda.Dispose();
        }

        private void withdrawRoom_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串 
            DisplayOrder tmp = (DisplayOrder)(this.InventoryList.SelectedItem);
            StringBuilder query = new StringBuilder("select OrderId,Rooms.RoomId,OrderStatus from Orders,Rooms where Rooms.RoomId=Orders.RoomId and ");
            query.Append("RoomName='");
            query.Append(tmp.roomName);
            query.Append("' and LocationDetailed='");
            query.Append(tmp.locationDetail);
            query.Append("'");
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query.ToString(), mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt) ;
            
            if (dt.Rows.Count != 0)
            {
                ShowMessageDialog();
            }
            else
            {
                StringBuilder query2 = new StringBuilder("select Rooms.RoomId  from Rooms where ");
                query2.Append("RoomName='");
                query2.Append(tmp.roomName);
                query2.Append("' and LocationDetailed='");
                query2.Append(tmp.locationDetail);
                query2.Append("'");
                SqlDataAdapter myda2 = new SqlDataAdapter(query2.ToString(), mycon);
                DataTable dt2 = new DataTable();
                myda2.Fill(dt2);
                string shiftRid = dt2.Rows[0]["RoomId"].ToString();
                string kmdDeleteAvail="delete from Avail where RoomId='"+shiftRid+"'";
                SqlCommand cmdDelAvail = new SqlCommand(kmdDeleteAvail, mycon);
                int kmd=cmdDelAvail.ExecuteNonQuery();

                string commandText2 = "delete from Rooms where Rooms.RoomId='"+shiftRid+"'";
                SqlCommand cmd3 = new SqlCommand(commandText2, mycon);
                int tps = cmd3.ExecuteNonQuery();
                cmd3.Dispose();

                ShowMessageDialogSuccess();
            }
            mycon.Close();
            myda.Dispose();
            Frame fr = FindParent<Frame>(this);
            fr.Navigate(typeof(LobbyPage),App.usingHost);

        }
        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("该房间还有未完成的订单");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => { }));
            await msgDialog.ShowAsync();
        }

        private async void ShowMessageDialogSuccess()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("操作成功");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => { }));
            await msgDialog.ShowAsync();
        }
        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }


    }
}
