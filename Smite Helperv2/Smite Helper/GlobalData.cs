using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Smite_Helper
{
    public static class GlobalData
    {
        private static int totalGods=-1;
        private static Image[] smallGodIcon;
        private static bool set = false;
        private static string pageView;
        //timers
        public static Grid currentGrid { get; set; }
        public static CustomTimer manaTimer;
        public static CustomTimer attackTimer;
        public static CustomTimer cdrTimer;
        public static CustomTimer speedTimer;
        public static CustomTimer fireGiantTimer;
        public static CustomTimer goldFuryTimer;
        private static Uri timerPageUri = new Uri("//TimerPage.xaml", UriKind.Relative);

        public static string[] keys { get; private set; }
        public static string[] data { get; private set; }
        public static Dictionary<string, Dictionary<string, string>> gods { get; private set; }
        public static async void loadData()
        {
            gods = new Dictionary<string, Dictionary<string, string>>();
            data = new string[40] { "abilityDisc1", "abilityDisc2", "abilityDisc3", "abilityDisc4", "abilityDiscPassive",
                "abilityMisc1", "abilityMisc2", "abilityMisc3", "abilityMisc4", "abilityMiscPassive",
                "abilityName1", "abilityName2", "abilityName3", "abilityName4", "abilityNamePassive",
                "attackSpeed", "attackSpeedPerLevel", "capabilities", "cons", "free", "health",
                "healthPerLevel", "hp5", "hp5l", "lore", "mana", "manaPerLevel", "movement", "mp5",
                "mp5l", "mProt", "mProtl", "pantheon", "pPowr", "pPowrl", "pProt", "pProtl", "pros", "roles", "title"};

            string godfile = "gods.txt";
            try
            {
                var godFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("parsedData\\" + godfile);
                var godStream = await godFile.OpenReadAsync();
                var godReader = new StreamReader(godStream.AsStream());
                int totalGods = Convert.ToInt32(godReader.ReadLine());
                keys = new string[totalGods];
                for (int i = 0; i < totalGods; i++)
                {
                    keys[i] = godReader.ReadLine();
                    gods.Add(keys[i], new Dictionary<string, string>());

                }
                godReader.Dispose();
                godStream.Dispose();
                for (int i = 0; i < 40; i++)
                {
                    var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("parsedData\\" + data[i] + ".txt");
                    var stream = await file.OpenReadAsync();
                    var reader = new StreamReader(stream.AsStream());
                    for (int j = 0; j < totalGods; j++)
                    {
                        gods[keys[j]].Add(data[i], reader.ReadLine());
                    }
                    reader.Dispose();
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static void setSmallGodIcons(Image[] nImages)
        {
            smallGodIcon = nImages;
            set = true;
        }
        public static Image getSmallGodIcons(int i)
        {
            Image returnImage = new Image();
            returnImage.Source = smallGodIcon[i].Source;
            returnImage.Height = smallGodIcon[i].Height;
            returnImage.Width = smallGodIcon[i].Width;
            returnImage.Margin = smallGodIcon[i].Margin;
            returnImage.Tag = smallGodIcon[i].Tag;
            return returnImage;
        }
        public static bool iconsSet()
        {
            return set;
        }
        public static void setPageView(string god)
        {
            pageView = god;
        }
        public static string getPageView()
        {
            return pageView;
        }
        public static void setManaTimer(CustomTimer timer)
        {
            manaTimer = timer;
            manaTimer.ready += timerReady;
        }
        public static void setAttackTimer(CustomTimer timer)
        {
            attackTimer = timer;
            attackTimer.ready += timerReady;
        }
        public static void setCDRTimer(CustomTimer timer)
        {
            cdrTimer = timer;
            cdrTimer.ready += timerReady;
        }
        public static void setSpeedTimer(CustomTimer timer)
        {
            speedTimer = timer;
            speedTimer.ready += timerReady;
        }
        public static void setFireGiantTimer(CustomTimer timer)
        {
            fireGiantTimer = timer;
            fireGiantTimer.ready += timerReady;
        }
        public static void setGoldFuryTimer(CustomTimer timer)
        {
            goldFuryTimer = timer;
            goldFuryTimer.ready += timerReady;
        }
        private static void timerReady(object sender, EventArgs e)
        {
            CustomToast toast = new CustomToast();
            toast.layout = currentGrid;
            toast.uri = timerPageUri;
            toast.title = "SA Timer:";
            if (sender == manaTimer)
            {
                toast.content = "Mana is ready!";
                toast.show();
            }
            else if (sender == attackTimer)
            {
                toast.content = "Attack is ready!";
                toast.show();
            }
            else if (sender == cdrTimer)
            {
                toast.content = "CDR is ready!";
                toast.show();
            }
            else if (sender == speedTimer)
            {
                toast.content = "Speed is ready!";
                toast.show();
            }
            else if (sender == fireGiantTimer)
            {
                toast.content = "Fire Giant is ready!";
                toast.show();
            }
            else if (sender == goldFuryTimer)
            {
                toast.content = "Gold Fury is ready!";
                toast.show();
            }
        }
    }
}
