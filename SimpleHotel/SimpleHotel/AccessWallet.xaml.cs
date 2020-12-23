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

namespace SimpleHotel
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccessWallet : Page
    {
        public AccessWallet()
        {
            this.InitializeComponent();
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            string query = @"select Balance from GuestWallet where GuestId='"+App.usingGuest.gid()+"'";
            SqlConnection mycon = new SqlConnection(con);

            //////////////////////////

            SqlDataAdapter myda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            this.balanceArea.Text = dt.Rows[0]["Balance"].ToString() ;

        }
    }
}
