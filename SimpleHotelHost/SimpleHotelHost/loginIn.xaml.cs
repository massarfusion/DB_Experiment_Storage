using SimpleHotelHost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SimpleHotelHost
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
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串  
            String queryStr = "select * from Host Where Password='" + this.pwdInput.Text + @"' and Nickname='" + this.nicknameInput.Text + "'";
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
                string hid = dt.Rows[0]["HostId"].ToString();
                string pwd = dt.Rows[0]["Password"].ToString();
                string rnm = dt.Rows[0]["RealName"].ToString();
                Host host = new Host(hid,nick,rnm,pwd);
                //Frame.Navigate(typeof(ResultPage), user);
                Frame prt = FindParent<Frame>(this);
                prt.Navigate(typeof(LobbyPage), host);

            }

            myda.Dispose();
            mycon.Close();
            return;
        }

        private void fastLogin_Click(object sender, RoutedEventArgs e)
        {
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串  
            String queryStr = "select * from Host Where HostId='" + this.hostId.Text + "'";
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
                string hid = dt.Rows[0]["HostId"].ToString();
                string pwd = dt.Rows[0]["Password"].ToString();
                string rnm = dt.Rows[0]["RealName"].ToString();
                Host host = new Host(hid, nick, rnm, pwd);
                //Frame.Navigate(typeof(ResultPage), user);
                Frame prt = FindParent<Frame>(this);
                prt.Navigate(typeof(LobbyPage), host);

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
        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("登录参数错误") { Title = "可能是您敲错了几个字符" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => {; }));
            await msgDialog.ShowAsync();
        }
    }
}
