using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ReleaseARoom : Page
    {
        public ObservableCollection<String> provSel;
        public ObservableCollection<String> citySel = new ObservableCollection<string>();
        public ObservableCollection<String> distSel = new ObservableCollection<string>();
        public ObservableCollection<String> rentInfo = new ObservableCollection<string>();
        public SqlConnection mycon;
        public int guestNum;
        public int unitPrice;
        public string Province;
        public string City;
        public string District;
        public string Detail;
        public string Intro;
        public string RoomName;
        public string RoomId;
        public string hid;
        public string locationId;
        public ReleaseARoom()
        {
            this.InitializeComponent();
            String[] vs = {"新疆维吾尔自治区","云南省","河南省","贵州省","海南省","黑龙江省","江西省","内蒙古自治区","四川省","湖南省","安徽省","天津市",
           "陕西省","北京市","山东省","湖北省","吉林省","上海市","江苏省","西藏自治区","辽宁省","山西省",
         "河北省","浙江省","甘肃省","重庆市","福建省","广西壮族自治区","青海省","宁夏回族自治区","广东省" };
            String[] rttp = { "独立房间", "整套出租" };
            rentInfo = new ObservableCollection<string>(rttp);
            Collection<string> cs = new Collection<string>(vs);
            provSel = new ObservableCollection<string>(cs);
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = hst; pwd = yjsp114514";  //这里是保存连接数据库的字符串  
            mycon = new SqlConnection(con);
            mycon.Open();
        }

        private void province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            citySel.Clear();
            distSel.Clear();
            String prvc = this.province.SelectedItem.ToString();//选中的省份STRING
            this.Province = prvc;
            String query = @"select distinct city_name from [regions-of-china] where [province_name]='" + prvc + @"'";
            SqlDataAdapter myda = new SqlDataAdapter(query, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            //this.citySel = new ObservableCollection<string>();
            foreach (DataRow row in dt.Rows)
            {
                citySel.Add(row[0].ToString());
            }
            myda.Dispose();

        }

        private void city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.citySel.Count == 0)
            {
                return;
            }
            else
            {
                ;
            }
            distSel.Clear();
            String prvc = this.province.SelectedItem.ToString();//选中的省份STRING
            String cty = this.city.SelectedItem.ToString();//选中的省份STRING
            this.City = cty;
            String query = @"select distinct district_name from [regions-of-china] where [province_name]='" + prvc + @"' and [city_name]='" + cty + @"'";
            SqlDataAdapter myda = new SqlDataAdapter(query, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            //this.citySel = new ObservableCollection<string>();
            foreach (DataRow row in dt.Rows)
            {
                distSel.Add(row[0].ToString());
            }
            myda.Dispose();
        }

        private void district_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.distSel.Count == 0)
            {
                return;
            }
            else
            {
                ;
            }
            
            String prvc = this.province.SelectedItem.ToString();//选中的省份STRING
            String cty = this.city.SelectedItem.ToString();//选中的省份STRING
            String dct = this.district.SelectedItem.ToString();
            this.District = dct;
            String query = @"select distinct location_id from [regions-of-china] where [province_name]='" + prvc + @"' and [city_name]='" + cty + @"' and [district_name]='"+dct+@"'";
            SqlDataAdapter myda = new SqlDataAdapter(query, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                this.locationId = row[0].ToString();
            }

        }

        

        private void gstNumSum_LostFocus(object sender, RoutedEventArgs e)
        {
            String num = this.gstNumSum.Text;
            int r;
            if (int.TryParse(num,out r))
            {
                this.guestNum = Convert.ToInt32(num);
            }
            else
            {
                this.gstNumWarn.Text = "含有非阿拉伯数字的字符";
            }
        }

        private void priceNum_LostFocus(object sender, RoutedEventArgs e)
        {
            String num = this.priceNum.Text;
            int r;
            if (int.TryParse(num, out r))
            {
                this.unitPrice= Convert.ToInt32(num);
            }
            else
            {
                this.unitPriceWarn2.Text = "含有非阿拉伯数字的字符";
            }
        }


        private void submitAttempt_Click(object sender, RoutedEventArgs e)
        {
            this.Detail = dtl.Text;
            this.Intro = rIntro.Text;
            this.RoomName = rName.Text;
            this.hid = App.usingHost.hostId;
            this.RoomId = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(Detail) ||
                string.IsNullOrWhiteSpace(Province) ||
                string.IsNullOrWhiteSpace(City) ||
                string.IsNullOrWhiteSpace(District) ||
                string.IsNullOrWhiteSpace(Detail) ||
                string.IsNullOrWhiteSpace(Intro) ||
                string.IsNullOrWhiteSpace(RoomName) ||
                string.IsNullOrWhiteSpace(guestNum.ToString()) ||
                string.IsNullOrWhiteSpace(unitPrice.ToString()) ||
                string.IsNullOrWhiteSpace(locationId) ||
                string.IsNullOrWhiteSpace(hid)
                )
            {
                ShowMessageDialogNotAvailable();
            }
            else {
                string locFull = Province + City + District + Detail;
                string insertQuery = "Insert into Rooms(Score,RentInfo,RoomId,LocationDetailed,Province,City,District,Detail,Intro,RoomName,GuestNum,UnitPrice,HostId,location_id) values(4.8,'整套出租','" +
                    this.RoomId+"','"+ locFull + "','" + 
                    this.Province + "','" + this.City + "','" +
                    this.District + "','" + this.Detail + "','" +
                    this.Intro + "','" + this.RoomName + "'," + 
                    this.guestNum + "," + this.unitPrice + ",'" + 
                    this.hid + "','" + this.locationId + "')";

                SqlCommand sqlman = new SqlCommand(insertQuery, mycon);


                int a=sqlman.ExecuteNonQuery();
                if (a > 0)
                {
                    ShowMessageDialogInsertSuccess();
                    sqlman.Dispose();
                    mycon.Close();
                    Frame frame = FindParent<Frame>(this);
                    frame.Navigate(typeof(LobbyPage),App.usingHost);
                }
                else
                {
                    ShowMessageDialogInsertError();
                    Frame frame = FindParent<Frame>(this);
                    frame.Navigate(typeof(LobbyPage), App.usingHost);
                }

            }


        }
        private async void ShowMessageDialogNotAvailable()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("有些选项没填啊");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => { }));
            await msgDialog.ShowAsync();
        }
        private async void ShowMessageDialogInsertError()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("数据库写入出错");
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => { }));
            await msgDialog.ShowAsync();
        }
        private async void ShowMessageDialogInsertSuccess()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("提交成功");
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
