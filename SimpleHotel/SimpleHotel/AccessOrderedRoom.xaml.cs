using SimpleHotel.Models;
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

namespace SimpleHotel
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
            listOfInfo=new ObservableCollection<DisplayOrder>();
            InventoryList.ItemsSource = listOfInfo;
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串 
            string gid = App.usingGuest.gid();
            StringBuilder query = new StringBuilder("select Orders.IssueTime,Orders.OrderStatus,Rooms.LocationDetailed,Rooms.RoomName from Orders,Rooms WHERE Orders.GuestId='");
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

        private void cancelOrder_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串 
            DisplayOrder tmp = (DisplayOrder)(this.InventoryList.SelectedItem);
            StringBuilder query = new StringBuilder("select OrderId,Orders.RoomId from Orders,Rooms where Rooms.RoomId=Orders.RoomId and IssueTime='");
            query.Append(tmp.issuedTime);
            query.Append("' and RoomName='");
            query.Append(tmp.roomName);
            query.Append("' and LocationDetailed='");
            query.Append(tmp.locationDetail);
            query.Append("'");
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query.ToString(), mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            string toDelete=dt.Rows[0]["OrderId"].ToString();
            string shiftRid= dt.Rows[0]["RoomId"].ToString();
            string commandText = "delete from Orders where OrderId='"+toDelete+"'";
            SqlCommand cmd2 = new SqlCommand(commandText, mycon);
            int a=cmd2.ExecuteNonQuery();
            if (a != 1)
            {
                ShowMessageDialog();
            }
            else
            {
                string commandText2 = "update Avail set IsAvailable=1 where RoomId='" + shiftRid + "'";
                SqlCommand cmd3 = new SqlCommand(commandText2, mycon);
                int tps= cmd3.ExecuteNonQuery();
                cmd3.Dispose();
                ShowMessageDialogSuccess();
            }
            mycon.Close();
            cmd2.Dispose();myda.Dispose();
            Frame fr = FindParent<Frame>(this);
            fr.Navigate(typeof(InnerLobby));
        }
        //public string issuedTime;
        //public string locationDetail;
       // public string roomName;
        //public string status;

        private void confirmPay_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串 
            DisplayOrder tmp = (DisplayOrder)(this.InventoryList.SelectedItem);
            StringBuilder query = new StringBuilder("select OrderId,Orders.RoomId from Orders,Rooms where Rooms.RoomId=Orders.RoomId and IssueTime='");
            query.Append(tmp.issuedTime);
            query.Append("' and RoomName='");
            query.Append(tmp.roomName);
            query.Append("' and LocationDetailed='");
            query.Append(tmp.locationDetail);
            query.Append("'");
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query.ToString(), mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            string oid = dt.Rows[0]["OrderId"].ToString();
            string shiftRid = dt.Rows[0]["RoomId"].ToString();
            string commandText = "update Orders set OrderStatus=2 where OrderId='" + oid + "'";
            SqlCommand cmd2 = new SqlCommand(commandText, mycon);
            int a = cmd2.ExecuteNonQuery();
            if (a != 1)
            {
                ShowMessageDialog();
            }
            else
            {
                ShowMessageDialogSuccess();
            }
            mycon.Close();
            cmd2.Dispose(); myda.Dispose();
            Frame fr = FindParent<Frame>(this);
            fr.Navigate(typeof(InnerLobby));
        }

        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("删除异常");
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
