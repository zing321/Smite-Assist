using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Parse;
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
using SmiteAssistPortable;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SmiteAssistWin8
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : SmiteAssistWin8.Common.LayoutAwarePage
    {
        private List<Dictionary<string, object>> godsGroup = new List<Dictionary<string, object>>();
        private int navigateTo = -1;
        public MainPage()
        {
            this.InitializeComponent();
            //parseTest();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                if (UniversalData.loaded)
                {
                    UniversalData_ready(null, null);
                }
                else
                {
                    ImageBrush logoBrush = new ImageBrush();
                    logoBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/SplashScreenload.png", UriKind.RelativeOrAbsolute));
                    SplashLogo.Fill = logoBrush;
                    SplashBackground.Visibility = Visibility.Visible;
                    SplashLogo.Visibility = Visibility.Visible;
                }
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["firstRuncomplete"] == null)
                {
                    GlobalFunctions.messageBox("Welcome to Smite Assist! This is the first time you're running the app so let me download the images (~4MB), this may take a bit. " +
                        "You can continue to use the app in the mean time but some images might not be there until you revisit the page after they have downloaded.");
                }
                UniversalData.ready += UniversalData_ready;
            }
            catch (Exception excep)
            {
                GlobalFunctions.messageBox(excep.Message);
            }
        }
        private async void parseTest()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var result = await ParseCloud.CallFunctionAsync<List<object>>("updateGodTable", parameters);
        }
        private void UniversalData_ready(object sender, EventArgs e)
        {
            switch (navigateTo)
            {
                case 0:
                    this.Frame.Navigate(typeof(GroupView), 0);
                    break;
                case 1:
                    this.Frame.Navigate(typeof(GroupView), 1);
                    break;
                case 2:
                    this.Frame.Navigate(typeof(GroupView), 2);
                    break;
                case 3:
                    this.Frame.Navigate(typeof(GroupView), 3);
                    break;
                case 4:
                    this.Frame.Navigate(typeof(GroupView), 4);
                    break;
                default:
                    break;
            }

            godsGroup.Add(new Dictionary<string, object>()
            {
                {"Title","All Gods"},
                {"ItemID",0}
            });
            godsGroup.Add(new Dictionary<string, object>()
            {
                {"Title","Physical"},
                {"ItemID",1}
            });
            godsGroup.Add(new Dictionary<string, object>()
            {
                {"Title","Magical"},
                {"ItemID",2}
            });
            godsGroup.Add(new Dictionary<string, object>()
            {
                {"Title","Tank"},
                {"ItemID",3}
            });
            godsGroup.Add(new Dictionary<string, object>()
            {
                {"Title","Support"},
                {"ItemID",4}
            });

            TranslateTransform translate = new TranslateTransform();
            translate.Y = 25;
            Random random = new Random();

            int max = UniversalData.godsList.Max<ParseObject>(god => Convert.ToInt32(god["GodId"]));
            BitmapImage newGodImage = new BitmapImage(new Uri("ms-appdata:///local/god_portraits/" +Convert.ToString(max)+ "c.jpg"));
            ImageBrush iBrush = new ImageBrush();
            newGodImage.DecodePixelWidth = 200;       
            iBrush.Transform = translate;
            iBrush.ImageSource = newGodImage;
            iBrush.Stretch = Stretch.None;
            godsGroup[0].Add("Image", iBrush);

            List<ParseObject> phys = UniversalData.godsList.FindAll(x => Convert.ToString(x["Type"]).Contains("Physical"));
            ImageBrush iBrush2 = new ImageBrush();
            BitmapImage physImage = new BitmapImage(new Uri("ms-appdata:///local/god_portraits/" + Convert.ToString(phys[random.Next(0, phys.Count)]["GodId"]) + "c.jpg"));
            physImage.DecodePixelWidth = 200;
            iBrush2.ImageSource = physImage;
            iBrush2.Transform = translate;
            iBrush2.Stretch = Stretch.None;
            godsGroup[1].Add("Image", iBrush2);

            List<ParseObject> mage = UniversalData.godsList.FindAll(x => Convert.ToString(x["Type"]).Contains("Magical"));
            ImageBrush iBrush3 = new ImageBrush();
            BitmapImage mageImage = new BitmapImage(new Uri("ms-appdata:///local/god_portraits/" + Convert.ToString(mage[random.Next(0, mage.Count)]["GodId"]) + "c.jpg"));
            mageImage.DecodePixelWidth = 200;
            iBrush3.ImageSource = mageImage;
            iBrush3.Transform = translate;
            iBrush3.Stretch = Stretch.None;
            godsGroup[2].Add("Image", iBrush3);

            List<ParseObject> tank = UniversalData.godsList.FindAll(x => Convert.ToString(x["Roles"]).Contains("Tank"));
            ImageBrush iBrush4 = new ImageBrush();
            BitmapImage tankImage = new BitmapImage(new Uri("ms-appdata:///local/god_portraits/" + Convert.ToString(tank[random.Next(0, tank.Count)]["GodId"]) + "c.jpg"));
            tankImage.DecodePixelWidth = 200;
            iBrush4.ImageSource = tankImage;
            iBrush4.Transform = translate;
            iBrush4.Stretch = Stretch.None;
            godsGroup[3].Add("Image", iBrush4);

            List<ParseObject> support = UniversalData.godsList.FindAll(x => Convert.ToString(x["Roles"]).Contains("Support"));
            ImageBrush iBrush5 = new ImageBrush();
            BitmapImage supportImage = new BitmapImage(new Uri("ms-appdata:///local/god_portraits/" + Convert.ToString(support[random.Next(0, support.Count)]["GodId"]) + "c.jpg"));
            supportImage.DecodePixelWidth = 200;
            iBrush5.ImageSource = supportImage;
            iBrush5.Transform = translate;
            iBrush5.Stretch = Stretch.None;
            godsGroup[4].Add("Image", iBrush5);
            GodsGrid.ItemsSource = godsGroup;

            List<Dictionary<string, object>> itemGroup = new List<Dictionary<string, object>>();
            ImageBrush iBrush6 = new ImageBrush();
            BitmapImage itemImage = new BitmapImage(new Uri("ms-appdata:///local/items/" + Convert.ToString(UniversalData.itemsList[random.Next(0, UniversalData.itemsList.Count)]["Name"]) + ".jpg"));
            itemImage.DecodePixelHeight = 200;
            itemImage.DecodePixelWidth = 200;
            iBrush6.ImageSource = itemImage;
            itemGroup.Add(new Dictionary<string, object>()
            {
                {"Title","Items"},
                {"Image",iBrush6}
            });
            ItemsGrid.ItemsSource = itemGroup;

            SplashLogo.Visibility = Visibility.Collapsed;
            SplashBackground.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            
        }
        private void PlayerPageButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlayerPage));
        }

        private void GodsGrid_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            Dictionary<string, object> item = e.ClickedItem as Dictionary<string, object>;
            if (UniversalData.loaded)
            {
                this.Frame.Navigate(typeof(GroupView), (int)item["ItemID"]);
            }
            else
            {
                navigateTo = (int)item["itemID"];
            }
        }
    }
}
