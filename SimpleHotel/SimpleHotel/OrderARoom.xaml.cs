using SimpleHotel.Models;
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
using Windows.UI.Popups;
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
    public sealed partial class OrderARoom : Page
    {
        public ObservableCollection<String> provSel;
        public ObservableCollection<String> citySel= new ObservableCollection<string>();
        public ObservableCollection<String> distSel= new ObservableCollection<string>();
        public ObservableCollection<String> rentInfo = new ObservableCollection<string>();
        public SqlConnection mycon;
        public OrderARoom()
        {
            this.InitializeComponent();
            String[] vs = {"新疆维吾尔自治区","云南省","河南省","贵州省","海南省","黑龙江省","江西省","内蒙古自治区","四川省","湖南省","安徽省","天津市",
           "陕西省","北京市","山东省","湖北省","吉林省","上海市","江苏省","西藏自治区","辽宁省","山西省",
         "河北省","浙江省","甘肃省","重庆市","福建省","广西壮族自治区","青海省","宁夏回族自治区","广东省" };
            String[] rttp = { "独立房间","整套出租" };
            rentInfo = new ObservableCollection<string>(rttp);
            Collection<string> cs = new Collection<string>(vs);
            provSel = new ObservableCollection<string>(cs);
            string con = "server = DESKTOP-RPMS5O5; DataBase = HotelDB; uid = wyt; pwd = t68sibzg";  //这里是保存连接数据库的字符串  
            mycon = new SqlConnection(con);
            mycon.Open();

        }

        private void province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            citySel.Clear();
            distSel.Clear();
            String prvc = this.province.SelectedItem.ToString();//选中的省份STRING
            String query = @"select distinct city_name from [regions-of-china] where [province_name]='"+prvc+@"'";
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
            if (this.citySel.Count == 0) {
                return;
            }
            else
            {
                ;
            }
            distSel.Clear();
            String prvc = this.province.SelectedItem.ToString();//选中的省份STRING
            String cty = this.city.SelectedItem.ToString();//选中的省份STRING
            String query = @"select distinct district_name from [regions-of-china] where [province_name]='" + prvc + @"' and [city_name]='"+cty+@"'";
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
            
        }

        private void submitRoomQuery_Click(object sender, RoutedEventArgs e)
        {
            var prov = Convert.ToString(this.province.SelectedItem);
            var cty = Convert.ToString(this.city.SelectedItem);
            var dis = Convert.ToString(this.district.SelectedItem);
            var rttp = Convert.ToString(this.rentType.SelectedItem);
            int priceLimit = (int)this.moneySlider.Value;//价格上限
            double score = Math.Round(this.minStars.Value,2);//z分数下限
            if (string.IsNullOrEmpty(prov)||string.IsNullOrEmpty(cty)|| string.IsNullOrEmpty(dis) || string.IsNullOrEmpty(rttp) )
            {
                ShowMessageDialog();   
            }
            else
            {
                if (prov.Equals("黑龙江省"))
                {
                    prov = "黑龙江";
                }
                else
                {
                    ;
                }
            String query = @"select * from Rooms,Host where Host.HostId=Rooms.HostId and Province='" + prov+@"' and City='"+cty+@"' and District='" +
                    dis+@"' and RentInfo='"+rttp+@"' and UnitPrice<="+priceLimit+@" and Score>="+score;
            Frame upf = FindParent<Frame>(this);
            upf.Navigate(typeof(HotelsList),query);

            }

        }


        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
        private async void ShowMessageDialog()
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("很显然，您漏掉了若干输入") ;
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("好的", uiCommand => {  }));
            await msgDialog.ShowAsync();
        }

    }
}
