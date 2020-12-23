using SimpleHotel.Models;
using System;
using System.Collections.Generic;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace SimpleHotel
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Guest gst=new Guest();
            this.InitializeComponent();
            DisplayArea.Navigate(typeof(welcomePage));
            this.menuNav.IsEnabled = false;
            this.PaneOpen.IsEnabled = false;
        }
        public void activatePane() {
            this.menuNav.IsEnabled = true;
            this.PaneOpen.IsEnabled = true;
        }
        public void deactivatePane() {
            this.menuNav.IsEnabled = false;
            this.PaneOpen.IsEnabled = false;
        }

        private void PaneOpen_Click(object sender, RoutedEventArgs e)
        {
            burger.IsPaneOpen = !burger.IsPaneOpen;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayArea.CanGoBack)
            {
                DisplayArea.GoBack();
            }
        }

        private void menuNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Order.IsSelected)
            {
                DisplayArea.Navigate(typeof(OrderARoom));
            }
            else if (this.OrderManage.IsSelected) {
                DisplayArea.Navigate(typeof(AccessOrderedRoom));
            }
            else if (this.Wallet.IsSelected)
            {
                DisplayArea.Navigate(typeof(AccessWallet));
            }
            else if (this.Account.IsSelected)
            {
                DisplayArea.Navigate(typeof(AccessAccount));
            }
        }

        private void DisplayArea_Navigated(object sender, NavigationEventArgs e)
        {
            var framePg = DisplayArea.Content;
            var PgType=framePg.GetType();
            if ("SimpleHotel.LobbyPage".Equals(PgType.FullName)) {
                this.DisplayArea.BackStack.Clear();
                activatePane();
            }
            else if ("SimpleHotel.InnerLobby".Equals(PgType.FullName))
            {
                this.DisplayArea.BackStack.Clear();
            }
            else
            {

            }
        }//this.Frame.BackStack.Clear();登陆页面过来的操作


    }
}
