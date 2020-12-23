using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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

    public sealed partial class createAccount : Page
    {
        public ObservableCollection<String> langs = new ObservableCollection<String>();
        public static string langageSel;
        public createAccount()
        {
            this.InitializeComponent();
            langs.Add("中文");
            langs.Add("English-US");
            langs.Add("Français-France");
            langs.Add("日本語");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.nicknameWarning.Text = "";
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            string query = @"select * from Guest where Nickname='"+this.nicknameInput.Text+"'";
            SqlConnection mycon = new SqlConnection(con);
            //创建SQL连接对象  
            ring.IsActive = true;
            mycon.Open();
            ring.IsActive = false;
            SqlDataAdapter myda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            int num = dt.Rows.Count;
            string guestId = System.Guid.NewGuid().ToString();
            int languageId = 0;
            if ("中文".Equals(langageSel))
            {
                languageId = 1;
            }
            else if ("English-US".Equals(langageSel)) {
                languageId = 2;
            }
            else if ("日本語".Equals(langageSel))
            {
                languageId = 4;
            }
            else if ("Français-France".Equals(langageSel))
            {
                languageId = 3;
            }
            else
            {
                ;
            }
            if (num == 0)
            {
                String kommand = @"insert into Guest Values('" + this.realnameInput.Text + @"','" + guestId + @"','" + languageId + @"','" + this.realnameInput.Text+"')";
                //languageId  guestId nickname realname 
                SqlCommand typein = new SqlCommand(kommand, mycon);
                int i = Convert.ToInt32(typein.ExecuteNonQuery());
                if (i > 0)
                {
                    string walletcommand = @"insert into GuestWallet Values('" + guestId+@"',100)";
                    SqlCommand wallet = new SqlCommand(walletcommand,mycon);
                    wallet.ExecuteNonQuery();
                    ShowMessageDialog(guestId);
                }
                else {
                    this.nicknameWarning.Text="数据库写入出错，再试试？";
                }



            }
            else {
                this.nicknameWarning.Text = "有重复用户名，请换一个试试咯？";
            }
            
            mycon.Close();
            mycon.Dispose();
        }

        private async void ShowMessageDialog(string guestId)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("您的用户名是"+this.nicknameInput.Text+"\n您的用户ID是"+guestId+",您可以直接输入ID登录！") { Title = "用一些时间记住它" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("我已经记住", uiCommand => { backtoMain();  }));
            await msgDialog.ShowAsync();
        }

        private void backtoMain() {
            Frame frametmp = FindParent<Frame>(this);
            frametmp.Navigate(typeof(welcomePage));
        }

        private void langInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            // Get the ComboBox selected item text
            string selectedItems = comboBox.SelectedItem.ToString();

            langageSel = selectedItems;
        }

        private void returnER_Click(object sender, RoutedEventArgs e)
        {
            Frame frametmp = FindParent<Frame>(this);
            if (frametmp.CanGoBack) {
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
