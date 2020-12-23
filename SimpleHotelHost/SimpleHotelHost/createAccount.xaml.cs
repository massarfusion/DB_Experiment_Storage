using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class createAccount : Page
    {
        
        public createAccount()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nicknameInput.Text)|| string.IsNullOrWhiteSpace(passwordInput.Text) || string.IsNullOrWhiteSpace(realNameInput.Text) )
            {
                ShowMessageDialogEmpty();
                return;
            }
            else
            {
                ;
            }

            this.nicknameWarning.Text = "";
            string pwd = this.passwordInput.Text;
            if (Regex.IsMatch(pwd, @"\w+")) { ; }
            else
            {
                this.pwdWarning.Text = "密码包含非法字符"; return;
            }
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串  
            string query = @"select * from Host where Nickname='" + this.nicknameInput.Text + "'";
            SqlConnection mycon = new SqlConnection(con);
            
            //创建SQL连接对象  
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int num = dt.Rows.Count;
            string hostId = System.Guid.NewGuid().ToString();
            if (num == 0)
            {
                String kommand = @"insert into Host Values('" + this.realNameInput.Text + @"','" + hostId + @"','" + this.nicknameInput.Text + @"','" + this.passwordInput.Text + "')";
                //languageId  guestId nickname realname 
                SqlCommand typein = new SqlCommand(kommand, mycon);
                int i = Convert.ToInt32(typein.ExecuteNonQuery());
                if (i > 0)
                {
                    string walletcommand = @"insert into HostWallet Values('" + hostId + @"',100)";
                    SqlCommand wallet = new SqlCommand(walletcommand, mycon);
                    wallet.ExecuteNonQuery();
                    mycon.Close();
                    myda.Dispose();
                    ShowMessageDialog(hostId);
                }
                else
                {
                    this.nicknameWarning.Text = "数据库写入出错，再试试？";
                    mycon.Close();
                    myda.Dispose();
                }



            }
            else
            {
                this.nicknameWarning.Text = "有重复用户名，建议您换一个或者加上容易记忆的后缀？";

            }

            
        }

        private async void ShowMessageDialog(string guestId)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("您的用户名是" + this.nicknameInput.Text + "\n您的用户ID是" + guestId + ",您可以直接输入ID登录！") { Title = "用一些时间记住它" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("我已经记住", uiCommand => { backtoMain(); }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("复制到剪贴板", uiCommand => { copyToClip(guestId); backtoMain(); }));
            await msgDialog.ShowAsync();
        }

        private async void ShowMessageDialogEmpty()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("有些选项没有填写");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => {; }));
            
            await msgDialog.ShowAsync();
        }

        private void copyToClip(string id)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(id);
            Clipboard.SetContent(dataPackage);
        }

        private void backtoMain()
        {
            Frame frametmp = FindParent<Frame>(this);
            frametmp.Navigate(typeof(welcomePage));
        }


        private void returnER_Click(object sender, RoutedEventArgs e)
        {
            Frame frametmp = FindParent<Frame>(this);
            if (frametmp.CanGoBack)
            {
                frametmp.GoBack();
            }
            else {; }
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
