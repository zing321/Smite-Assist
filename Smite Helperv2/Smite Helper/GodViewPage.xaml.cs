using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using Windows.ApplicationModel.Store;
using SmiteAssistPortable;
using Parse;

namespace Smite_Helper
{
    public partial class GodViewPage : PhoneApplicationPage
    {
        ImageBrush bGBrush = new ImageBrush();
        Image passiveIcon = new Image();
        Image skill1 = new Image();
        Image skill2 = new Image();
        Image skill3 = new Image();
        Image skill4 = new Image();
        TextBlock HealthBlock1 = new TextBlock();
        TextBlock HealthBlock2 = new TextBlock();
        TextBlock Health5Block1 = new TextBlock();
        TextBlock Health5Block2 = new TextBlock();
        TextBlock ManaBlock1 = new TextBlock();
        TextBlock ManaBlock2 = new TextBlock();
        TextBlock Mana5Block1 = new TextBlock();
        TextBlock Mana5Block2 = new TextBlock();
        TextBlock PPowerBlock1 = new TextBlock();
        TextBlock PPowerBlock2 = new TextBlock();
        TextBlock PProtBlock1 = new TextBlock();
        TextBlock PProtBlock2 = new TextBlock();
        TextBlock MProtBlock1 = new TextBlock();
        TextBlock MProtBlock2 = new TextBlock();
        TextBlock MoveBlock1 = new TextBlock();
        TextBlock MoveBlock2 = new TextBlock();
        private PurchaseInterface purchaser= new PurchaseInterface();
        string finalLore = "";
        private int godNumber = -1;
        private int currentlyRunning = 0;
        public GodViewPage()
        {
            InitializeComponent();
            SolidColorBrush blackBrush=new SolidColorBrush();
            blackBrush.Color=Colors.Black;
            LayoutRoot.Background = blackBrush;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GlobalData.currentGrid = LayoutRoot;
            checkForActiveProducts();
            try
            {
                if (currentlyRunning == 0)
                {
                    //start inital load
                    base.OnNavigatedTo(e);
                    for (int i = 0; i < UniversalData.godsList.Count; i++)
                    {
                        if (GlobalData.getPageView() == (string)UniversalData.godsList.ElementAt<ParseObject>(i)["Name"])
                        {
                            godNumber = i;
                            break;
                        }
                    }
                    GodPivot.Title = GlobalData.getPageView();

                    BitmapImage bgBitmap;
                    string godName = (string)UniversalData.godsList.ElementAt<ParseObject>(godNumber)["Name"];
                    if (godName.Contains(' '))
                    {
                        string[] godSplit = godName.Split(' ');
                        bgBitmap = new BitmapImage(new Uri("images//" + godSplit[0] + "_" + godSplit[1] + "_card.jpg", UriKind.Relative));
                    }
                    else
                    {
                        bgBitmap = new BitmapImage(new Uri("images//" + godName + "_card.jpg", UriKind.Relative));
                    }
                    bgBitmap.DecodePixelHeight = 800;
                    bgBitmap.DecodePixelWidth = 480;
                    bGBrush.ImageSource = bgBitmap;
                    bGBrush.Opacity = .35;
                    GodPivot.Background = bGBrush;
                    //end initial load
                    //load skill pivot

                    setSkillImage(passiveIcon, 0);
                    setSkillImage(skill1, 1);
                    setSkillImage(skill2, 2);
                    setSkillImage(skill3, 3);
                    setSkillImage(skill4, 4);
                    string ABlock = setAbilityText(0);
                    setTextToGrid(PassiveText, ABlock, 0, 1, SkillsGrid, false);
                    ABlock = setAbilityText(1);
                    setTextToGrid(Skill1Text, ABlock, 1, 1, SkillsGrid, false);
                    ABlock = setAbilityText(2);
                    setTextToGrid(Skill2Text, ABlock, 2, 1, SkillsGrid, false);
                    ABlock = setAbilityText(3);
                    setTextToGrid(Skill3Text, ABlock, 3, 1, SkillsGrid, false);
                    ABlock = setAbilityText(4);
                    setTextToGrid(Skill4Text, ABlock, 4, 1, SkillsGrid, false);
                    //end skill pivot
                    //load stat pivot
                    RoleCaps.Text = "Role and capabilities: " + (string)UniversalData.godsList.ElementAt<ParseObject>(godNumber)["Roles"] + "  (" + (string)UniversalData.godsList.ElementAt<ParseObject>(godNumber)["Type"] + " )";
                    ParseObject god = UniversalData.godsList.ElementAt<ParseObject>(godNumber);
                    setTextToGrid(HealthBlock1, Convert.ToString(god["Health"]), 3, 1, StatsGrid, true);
                    setTextToGrid(HealthBlock2, Convert.ToString(god["HealthPerLevel"]), 3, 2, StatsGrid, true);
                    setTextToGrid(Health5Block1, Convert.ToString(god["HealthPerFive"]), 4, 1, StatsGrid, true);
                    setTextToGrid(Health5Block2, Convert.ToString(god["HP5PerLevel"]), 4, 2, StatsGrid, true);
                    setTextToGrid(ManaBlock1, Convert.ToString(god["Mana"]), 5, 1, StatsGrid, true);
                    setTextToGrid(ManaBlock2, Convert.ToString(god["ManaPerLevel"]), 5, 2, StatsGrid, true);
                    setTextToGrid(Mana5Block1, Convert.ToString(god["ManaPerFive"]), 6, 1, StatsGrid, true);
                    setTextToGrid(Mana5Block2, Convert.ToString(god["MP5PerLevel"]), 6, 2, StatsGrid, true);
                    setTextToGrid(PPowerBlock1, Convert.ToString(god["PhysicalPower"]), 7, 1, StatsGrid, true);
                    setTextToGrid(PPowerBlock2, Convert.ToString(god["PhysicalPowerPerLevel"]), 7, 2, StatsGrid, true);
                    setTextToGrid(PProtBlock1, Convert.ToString(god["PhysicalProtection"]), 8, 1, StatsGrid, true);
                    setTextToGrid(PProtBlock2, Convert.ToString(god["PhysicalProtectionPerLevel"]), 8, 2, StatsGrid, true);
                    setTextToGrid(MProtBlock1, Convert.ToString(god["MagicProtection"]), 9, 1, StatsGrid, true);
                    setTextToGrid(MProtBlock2, Convert.ToString(god["MagicProtectionPerLevel"]), 9, 2, StatsGrid, true);
                    setTextToGrid(MoveBlock1, Convert.ToString(god["AttackSpeed"]), 10, 1, StatsGrid, true);
                    setTextToGrid(MoveBlock2, Convert.ToString(god["AttackSpeedPerLevel"]), 10, 2, StatsGrid, true);
                    //end stat pivot
                    //load lore pivot
                    string[] split = new string[2];
                    string lore = (string)god["Lore"];
                    finalLore = "Pantheon: " + (string)god["Pantheon"] + "\n\n";
                    split[1] = lore;
                    while (split[1].Contains("\\n\\n"))
                    {
                        split = split[1].Split(new string[] { "\\n\\n" }, 2, StringSplitOptions.None);
                        finalLore += "\t" + split[0] + "\n\n";
                    }
                    finalLore += "\t" + split[1] + "\n\n";
                    LoreText.Text = finalLore;
                    //end lore pivot
                    currentlyRunning = 1;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        //no new allocations
        private void setTextToGrid(TextBlock block, string text, int row, int col, Grid nGrid, bool center)
        {
            block.Text = text;
            if (center)
            {
                block.HorizontalAlignment = HorizontalAlignment.Center;
                block.VerticalAlignment = VerticalAlignment.Center;
            }
            Grid.SetRow(block, row);
            Grid.SetColumn(block, col);
            if(!nGrid.Children.Contains(block))
                nGrid.Children.Add(block);
        }
        private void setSkillImage(Image nImage, int skill)
        {
            ParseObject god = UniversalData.godsList.ElementAt<ParseObject>(godNumber);
            //allocation
            Rectangle blackUnderlay = new Rectangle();
            SolidColorBrush blackBrush = new SolidColorBrush();
            //allocation end
            string godName=(string)god["Name"],skillName="";
            if(godName.Contains(' '))
            {
                godName = godName.Replace(" ", "");
            }
            switch(skill)
            {
                case 0:
                    skillName=(string)god["Ability5"];
                    Grid.SetRow(nImage,0);
                    Grid.SetRow(blackUnderlay, 0);
                    break;
                case 1:
                    skillName = (string)god["Ability1"];
                    Grid.SetRow(nImage,1);
                    Grid.SetRow(blackUnderlay, 1);
                    break;
                case 2:
                    skillName = (string)god["Ability2"];
                    Grid.SetRow(nImage,2);
                    Grid.SetRow(blackUnderlay, 2);
                    break;
                case 3:
                    skillName = (string)god["Ability3"];
                    Grid.SetRow(nImage,3);
                    Grid.SetRow(blackUnderlay, 3);
                    break;
                case 4:
                    skillName = (string)god["Ability4"];
                    Grid.SetRow(nImage,4);
                    Grid.SetRow(blackUnderlay, 4);
                    break;
            }
            Grid.SetColumn(nImage,0);
            Grid.SetColumn(blackUnderlay, 0);
            if (skillName.Contains(' '))
            {
                skillName=skillName.Replace(" ","");          
            }
            if (skillName.Contains('/'))
            {
                skillName=skillName.Replace('/','-');
            }
            //prime area
            nImage.Margin = new Thickness(18, 31, 18, 31);
            blackBrush.Color = Colors.Black;
            blackBrush.Opacity = .7;
            blackUnderlay.Fill = blackBrush;
            blackUnderlay.Height = 80;
            blackUnderlay.Width = 80;
            SkillsGrid.Children.Add(blackUnderlay);
            //end prime
            nImage.Source = new BitmapImage(new Uri("images\\" + godName + "_" + skillName + ".jpg", UriKind.Relative));
            
            SkillsGrid.Children.Add(nImage);
        }
        private string setAbilityText(int skill)
        {
            ParseObject god = UniversalData.godsList.ElementAt<ParseObject>(godNumber);
            Dictionary<string, object> rawAbility = new Dictionary<string, object>(); ;
            Dictionary<string, object> ability = new Dictionary<string, object>();
            List<object> menuItems = new List<object>();
            List<object> rankItems = new List<object>();
            string blockText = "";
            string name="", disc="";
            switch (skill)
            {
                case 0:
                    name = (string)god["Ability5"];
                    rawAbility = (Dictionary<string, object>)god["abilityDescription5"];
                    ability = (Dictionary<string, object>)rawAbility["itemDescription"];
                    disc = (string)ability["description"];
                    menuItems = (List<object>)ability["menuitems"];
                    rankItems = (List<object>)ability["rankitems"];
                    break;
                case 1:
                    name = (string)god["Ability1"];
                    rawAbility = (Dictionary<string, object>)god["abilityDescription1"];
                    ability = (Dictionary<string, object>)rawAbility["itemDescription"];
                    disc = (string)ability["description"];
                    menuItems = (List<object>)ability["menuitems"];
                    rankItems = (List<object>)ability["rankitems"];
                    break;
                case 2:
                    name = (string)god["Ability2"];
                    rawAbility = (Dictionary<string, object>)god["abilityDescription2"];
                    ability = (Dictionary<string, object>)rawAbility["itemDescription"];
                    disc = (string)ability["description"];
                    menuItems = (List<object>)ability["menuitems"];
                    rankItems = (List<object>)ability["rankitems"];
                    break;
                case 3:
                    name = (string)god["Ability3"];
                    rawAbility = (Dictionary<string, object>)god["abilityDescription3"];
                    ability = (Dictionary<string, object>)rawAbility["itemDescription"];
                    disc = (string)ability["description"];
                    menuItems = (List<object>)ability["menuitems"];
                    rankItems = (List<object>)ability["rankitems"];
                    break;
                case 4:
                    name = (string)god["Ability4"];
                    rawAbility = (Dictionary<string, object>)god["abilityDescription4"];
                    ability = (Dictionary<string, object>)rawAbility["itemDescription"];
                    disc = (string)ability["description"];
                    menuItems = (List<object>)ability["menuitems"];
                    rankItems = (List<object>)ability["rankitems"];
                    break;
            }           
            blockText = name + "\n\n" + disc+"\n\n"+"Cooldown: "+(string)ability["cooldown"]+"\n"+"Cost: "+(string)ability["cost"];
            for (int i = 0; i < menuItems.Count; i++)
            {
                Dictionary<string, object> menuItem = new Dictionary<string, object>();
                menuItem = (Dictionary<string,object>)menuItems[i];
                blockText += "\n"+ (string)menuItem["description"] + " " + (string)menuItem["value"];
            }
            for (int i = 0; i < rankItems.Count; i++)
            {
                Dictionary<string, object> rankItem = new Dictionary<string, object>();
                rankItem = (Dictionary<string, object>)rankItems[i];
                blockText += "\n" + (string)rankItem["description"] + " " + (string)rankItem["value"];
            }
            if ((string)ability["secondaryDescription"] != null && (string)ability["secondaryDescription"] != "")
            {
                blockText += "\n\n" + (string)ability["secondaryDescription"];
            }
            return blockText;
        }

        private void ApplicationBarMenuItem_Click(object sender, System.EventArgs e)
        {
            purchaser.invokePurchaceInterface(LayoutRoot, "RmAdsPID");
        }
        private void checkForActiveProducts()
        {
            var licences = CurrentApp.LicenseInformation.ProductLicenses;
            if (licences["RmAdsPID"].IsActive)
            {
                StatsPageAd.IsEnabled = false;
                StatsPageAd.Visibility = Visibility.Collapsed;
                ApplicationBar.IsVisible = false;
            }
        }
    }
}