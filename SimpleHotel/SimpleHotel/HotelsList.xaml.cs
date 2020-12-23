using SimpleHotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
    
    public sealed partial class HotelsList : Page
    {
        
        public static ObservableCollection<RoomInfo> listOfInfo;
        public static string query;
        public HotelsList()
        {
            this.InitializeComponent();
            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter.GetType().Equals(typeof(string)))
            {
               query= e.Parameter.ToString();
            }
            listOfInfo = new ObservableCollection<RoomInfo>();
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            InventoryList.ItemsSource = listOfInfo;
            int max = dt.Rows.Count;
            for(int i = 0; i < max; i++)
            {
                listOfInfo.Add(
                    new RoomInfo(double.Parse(dt.Rows[i]["Score"].ToString()), dt.Rows[i]["LocationDetailed"].ToString(),
                    dt.Rows[i]["ClutterCharges"].ToString(),
                    int.Parse(dt.Rows[i]["GuestNum"].ToString()),
                    dt.Rows[i]["Intro"].ToString(),
                    dt.Rows[i]["RoomName"].ToString(),
                    int.Parse(dt.Rows[i]["UnitPrice"].ToString()),
                    dt.Rows[i]["Nickname"].ToString(),
                    dt.Rows[i]["RoomId"].ToString(),
                    dt.Rows[i]["HostId"].ToString())
                    ) ;
            }
            //int a = 0;
            //RoomInfo(double score, string location_detailed, string clutterCharges, 
            //string guestNum, string intro, string roomName, int unitPrice, int hostNickname, string roomId)
            myda.Dispose();
            mycon.Close();

        }

        private void commitOrder_Click(object sender, RoutedEventArgs e)
        {
            
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();



            //以下为写入数据库
            string oid = Guid.NewGuid().ToString();
            string dateIn= DateTime.Now.ToString();
            string gid = App.usingGuest.gid();
            var hidRaw = this.InventoryList.SelectedItem as RoomInfo;
            string hid = hidRaw.hid();
            string cind = "";
            string coutd = "";
            string rid = hidRaw.rid();
            int status = 1;
            //数据部分
            
            string availQuery = @"select * from Avail where Avail.IsAvailable=0 and Avail.RoomId='"+rid+"'";
            SqlCommand availResult = new SqlCommand(availQuery,mycon);
            object availableOrNot = availResult.ExecuteScalar();
            if (null == availableOrNot) {
                ;
            }
            else
            {
                this.ShowMessageDialogNotAvailable();
                Frame jumpDest = FindParent<Frame>(this);
                jumpDest.Navigate(typeof(HotelsList),query);
                return;

            }
            availResult.Dispose();
            //查看可用情况↑
            ShowMessageDialog();
            String kommand = @"insert into Orders values('"+
                oid+"','"+
                dateIn + "','" +
                gid + "','" +
                hid + "','" +
                cind + "','" +
                coutd + "','" +
                rid + "','" +
                status + "')";
            SqlCommand kmd = new SqlCommand(kommand, mycon);
            int commandRtn = kmd.ExecuteNonQuery();
            kmd.Dispose();
            //改可用状态
            string  alterAvailKommand = @"update Avail set Avail.IsAvailable=0 where Avail.RoomId='" + rid + "'";
            SqlCommand avai = new SqlCommand(alterAvailKommand,mycon);
            avai.ExecuteNonQuery();
            avai.Dispose();

            mycon.Close();
            Frame frame = FindParent<Frame>(this);
            frame.Navigate(typeof(InnerLobby));
        }
        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("您成功为自己创建了一个订单。\n请在订单管理部分查看。");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => { }));
            await msgDialog.ShowAsync();
        }

        private async void ShowMessageDialogNotAvailable()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("很遗憾，这间房间被人选走了");
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
