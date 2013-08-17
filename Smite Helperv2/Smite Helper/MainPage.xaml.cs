using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.ApplicationModel.Store;
using Parse;
using System.Windows.Media;
using SmiteAssistPortable;

namespace Smite_Helper
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Image[] combinedIcons;
        private int combinedTotal = 0;
        private PurchaseInterface purchaser = new PurchaseInterface();
        public MainPage()
        {
            InitializeComponent();
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            LayoutRoot.Background = blackBrush;
            combinedTotal = 0;
            combinedTotal = UniversalData.godsList.Count + totalType("Physical") + totalType("Magical") + totalType("Tank") + totalType("Support");
            combinedIcons = new Image[combinedTotal];
            loadSmallGodIcons();
            setGodIcons(0, AllGrid);
            setGodIcons(1, PhysicalGrid);
            setGodIcons(2, MagicalGrid);
            setGodIcons(3, TankGrid);
            setGodIcons(4, SupportGrid);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GlobalData.currentGrid = LayoutRoot;
            checkForActiveProducts();
            base.OnNavigatedTo(e);
            for (int i = 0; i < combinedIcons.Length; i++)
            {
                combinedIcons[i].Tap += godClick;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            for (int i = 0; i < combinedIcons.Length; i++)
            {
                combinedIcons[i].Tap -= godClick;
            }
        }
        private void godClick(Object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Image analyser = (Image)sender;
                for (int i = 0; i < UniversalData.godsList.Count; i++)
                {
                    string godName = (string)UniversalData.godsList.ElementAt<ParseObject>(i)["Name"];
                    if (analyser.Tag.ToString() == godName)
                    {
                        GlobalData.setPageView(godName);
                        NavigationService.Navigate(new Uri("//GodViewPage.xaml", UriKind.Relative));
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
        private void setGodIcons(int type, Grid nGrid)
        {
            LinkedList<int> godsToUse = new LinkedList<int>();
            int row=0, col = 0, offset=0;
            double gridHeight = Math.Ceiling((double)UniversalData.godsList.Count / 4) * 114;
            switch (type)
            {
                case 0:
                    for (int j = 0; j < UniversalData.godsList.Count; j++)
                    {
                        godsToUse.AddLast(j);
                    }
                    break;
                case 1:
                    for (int j = 0; j < UniversalData.godsList.Count; j++)
                    {
                        string godType=(string)UniversalData.godsList.ElementAt<ParseObject>(j)["Type"];
                        if (godType.Contains("Physical"))
                        {
                            godsToUse.AddLast(j);
                        }
                    }
                    offset = UniversalData.godsList.Count;
                    break;
                case 2:
                    for (int j = 0; j < UniversalData.godsList.Count; j++)
                    {
                        string godType=(string)UniversalData.godsList.ElementAt<ParseObject>(j)["Type"];
                        if (godType.Contains("Magical"))
                        {
                            godsToUse.AddLast(j);
                        }
                    }
                    offset = UniversalData.godsList.Count + totalType("Physical");
                    break;
                case 3:
                    for (int j = 0; j < UniversalData.godsList.Count; j++)
                    {
                        string godRole = (string)UniversalData.godsList.ElementAt<ParseObject>(j)["Roles"];
                        if (godRole.Contains("Tank"))
                        {
                            godsToUse.AddLast(j);
                        }
                    }
                    offset = UniversalData.godsList.Count + totalType("Physical") + totalType("Magical");
                    break;
                case 4:
                    for (int j = 0; j < UniversalData.godsList.Count; j++)
                    {
                        string godRole = (string)UniversalData.godsList.ElementAt<ParseObject>(j)["Roles"];
                        if (godRole.Contains("Support"))
                        {
                            godsToUse.AddLast(j);
                        }
                    }
                    offset = UniversalData.godsList.Count + totalType("Physical") + totalType("Magical") + totalType("Tank");
                    break;
            }
            nGrid.Height = gridHeight;
            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(114);
            nGrid.RowDefinitions.Add(r1);
            
            
            for (int i = 0; i < UniversalData.godsList.Count; i++)
            {
                if (godsToUse.Count == 0)
                {
                    break;
                }
                
                var godN = godsToUse.First;
                godsToUse.RemoveFirst();
                combinedIcons[offset+i]=GlobalData.getSmallGodIcons(godN.Value);
                combinedIcons[offset+i].Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(godClick);
                Grid.SetColumn(combinedIcons[offset+i], col);
                Grid.SetRow(combinedIcons[offset+i], row);
                nGrid.Children.Add(combinedIcons[offset + i]);
                col++;
                if (col == 4)
                {
                    col = 0;
                    row++;
                    RowDefinition r1n = new RowDefinition();
                    r1n.Height = new GridLength(114);
                    nGrid.RowDefinitions.Add(r1n);
                }
            }
        }
        private int totalType(string type)
        {
            int total = 0;
            for (int i = 0; i < UniversalData.godsList.Count; i++)
            {
                string godType = (string)UniversalData.godsList.ElementAt<ParseObject>(i)["Type"];
                string godRole = (string)UniversalData.godsList.ElementAt<ParseObject>(i)["Roles"];
                if (godType.Contains(type))
                {
                    total++;
                }
                if (godRole.Contains(type))
                {
                    total++;
                }
            }
            return total;
        }
        private void loadSmallGodIcons()
        {
            if (!GlobalData.iconsSet())
            {
                Image[] smallGodIcon = new Image[UniversalData.godsList.Count];
                for (int i = 0; i < UniversalData.godsList.Count; i++)
                {
                    string godName = (string)UniversalData.godsList.ElementAt<ParseObject>(i)["Name"];
                    smallGodIcon[i] = new Image();
                    if (godName.Contains(' '))
                    {
                        string[] split = new string[2];
                        split = godName.Split(' ');
                        smallGodIcon[i].Source = new BitmapImage(new Uri("images//" + split[0] + "_" + split[1] + ".jpg", UriKind.Relative));
                    }
                    else
                    {
                        smallGodIcon[i].Source = new BitmapImage(new Uri("images//" + godName + ".jpg", UriKind.Relative));
                    }
                    smallGodIcon[i].Height = 100;
                    smallGodIcon[i].Width = 100;
                    smallGodIcon[i].Margin = new Thickness(7, 7, 7, 7);
                    smallGodIcon[i].Tag = godName;
                    GlobalData.setSmallGodIcons(smallGodIcon);
                }
            }
            
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
                MainPageAd.IsEnabled = false;
                MainPageAd.Visibility = Visibility.Collapsed;
                ApplicationBar.IsVisible = false;
            }
        }
        private async void parseTest()
        {
            try
            {
                Dictionary<string,object> parameter = new Dictionary<string, object>();
                var result = await ParseCloud.CallFunctionAsync<string>("loadTest", parameter);
                MessageBox.Show(result);
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
    }
}