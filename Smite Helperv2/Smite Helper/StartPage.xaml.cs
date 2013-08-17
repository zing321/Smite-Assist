using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using SmiteAssistPortable;
using Windows.ApplicationModel.Store;

namespace Smite_Helper
{
    public partial class StartPage : PhoneApplicationPage
    {
        private ProgressBar bar = new ProgressBar();
        private PurchaseInterface purchaser = new PurchaseInterface();
        public StartPage()
        {
            InitializeComponent();
            checkForActiveProducts();
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            LayoutRoot.Background = blackBrush;

            ImageBrush godsBrush=new ImageBrush();
            BitmapImage godsImage = new BitmapImage(new Uri("images//Chronos_card.jpg", UriKind.Relative));
            godsImage.DecodePixelWidth = (int)GodsButton.Width;
            godsBrush.ImageSource = godsImage;
            TranslateTransform translate = new TranslateTransform();
            translate.Y = 150;
            godsBrush.Transform = translate;
            godsBrush.Stretch = Stretch.None;
            GodsButton.Fill = godsBrush;

            ImageBrush timersBrush = new ImageBrush();
            BitmapImage timersImage = new BitmapImage(new Uri("images//Chronos' Pendant.jpg", UriKind.Relative));
            timersImage.DecodePixelWidth = (int)TimersButton.Width;
            timersBrush.ImageSource = timersImage;
            timersBrush.Stretch = Stretch.None;
            TimersButton.Fill = timersBrush;

            bar.IsIndeterminate = true;
            Grid.SetRow(bar, 0);
            bar.VerticalAlignment = VerticalAlignment.Top;
            bar.HorizontalAlignment = HorizontalAlignment.Stretch;
            bar.Visibility = Visibility.Collapsed;
            LayoutRoot.Children.Add(bar);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GlobalData.currentGrid = LayoutRoot;
        }
        private void God_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if ((DeviceNetworkInformation.IsNetworkAvailable && UniversalData.loading) || UniversalData.loaded)
            {
                bar.Visibility = Visibility.Visible;
                if (UniversalData.loaded)
                {
                    NavigationService.Navigate(new Uri("//MainPage.xaml", UriKind.Relative));
                    bar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    UniversalData.ready += UniversalData_ready;
                }
            }
            else if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                UniversalData.loadData(null,null);
            }
            else
            {
                MessageBox.Show("No network connection!");
            }
        }

        void UniversalData_ready(object sender, EventArgs e)
        {
            bar.Visibility = Visibility.Collapsed;
            UniversalData.ready -= UniversalData_ready;               
            NavigationService.Navigate(new Uri("//MainPage.xaml", UriKind.Relative));
        }

        private void Timer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("//TimerPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, System.EventArgs e)
        {
            purchaser.invokePurchaceInterface(LayoutRoot,"RmAdsPID");
        }
        private void checkForActiveProducts()
        {
            var licences = CurrentApp.LicenseInformation.ProductLicenses;
            if (licences["RmAdsPID"].IsActive)
            {
                ApplicationBar.IsVisible = false;
            }
        }
    }
}