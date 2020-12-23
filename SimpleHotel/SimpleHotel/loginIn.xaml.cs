using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using SimpleHotel.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SimpleHotel
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class loginIn : Page
    {
        public loginIn()
        {
            this.InitializeComponent();

        }
        private void loginAttempt_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            String queryStr = "select * from Guest Where Realname='"+this.realNameInput.Text+ @"' and Nickname='"+this.nicknameInput.Text+"'";
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(queryStr, con);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int num = dt.Rows.Count;
            if (num == 0)
            {
                this.ShowMessageDialog();
            }
            else {
                string nick = dt.Rows[0]["Nickname"].ToString();
                string gid = dt.Rows[0]["GuestId"].ToString();
                string lid = dt.Rows[0]["LanguageId"].ToString();
                string rnm = dt.Rows[0]["RealName"].ToString();
                Guest gst = new Guest(nick,rnm,gid,lid);
                //Frame.Navigate(typeof(ResultPage), user);
                Frame prt = FindParent<Frame>(this);
                prt.Navigate(typeof(LobbyPage), gst);

            }

            myda.Dispose();
            mycon.Close();
            return;
        }
        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("登录参数错误") { Title = "应该是输入错误" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => {; }));
            await msgDialog.ShowAsync();
        }
        private void fastLogin_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            String queryStr = "select * from Guest Where GuestId='"+this.guestId.Text+"'";
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(queryStr, con);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int num = dt.Rows.Count;
            if (num == 0)
            {
                this.ShowMessageDialog();
            }
            else
            {
                string nick = dt.Rows[0]["Nickname"].ToString();
                string gid = dt.Rows[0]["GuestId"].ToString();
                string lid = dt.Rows[0]["LanguageId"].ToString();
                string rnm = dt.Rows[0]["RealName"].ToString();
                Guest gst = new Guest(nick, rnm, gid, lid);
                //Frame.Navigate(typeof(ResultPage), user);
                Frame prt = FindParent<Frame>(this);
                prt.Navigate(typeof(LobbyPage), gst);

            }

            myda.Dispose();
            mycon.Close();
            mycon.Dispose();
            return;
        }
        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }//findparent
    }//public sealed partial class loginIn : Page
}
