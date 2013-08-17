using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.ApplicationModel.Store;

namespace Smite_Helper
{
    public partial class TimerPage : PhoneApplicationPage
    {
        private PurchaseInterface purchaser = new PurchaseInterface();
        private CustomToast toast = new CustomToast();
        private static bool running = false;
        private static Grid ContentPanel = new Grid(){
            Margin=new Thickness(12,0,12,0),
            RowDefinitions={new RowDefinition(),new RowDefinition(),new RowDefinition()},
            ColumnDefinitions={new ColumnDefinition(){MinWidth=228},new ColumnDefinition(){MinWidth=228}}
        };
        private static CustomTimer manaTimer = new CustomTimer(ContentPanel, 0, 0, 222, 222, new Thickness(0, 12, 6, 0), HorizontalAlignment.Left, VerticalAlignment.Bottom, "Mana", 240, Colors.Blue);
        private static CustomTimer attackTimer = new CustomTimer(ContentPanel, 0, 1, 222, 222, new Thickness(6, 12, 0, 0), HorizontalAlignment.Right, VerticalAlignment.Bottom, "Attack", 240, Colors.Red);
        private static CustomTimer cdrTimer = new CustomTimer(ContentPanel, 1, 0, 222, 222, new Thickness(0, 12, 6, 0), HorizontalAlignment.Left, VerticalAlignment.Bottom, "CDR", 240, Colors.Gray);
        private static CustomTimer speedTimer = new CustomTimer(ContentPanel, 1, 1, 222, 222, new Thickness(6, 12, 0, 0), HorizontalAlignment.Right, VerticalAlignment.Bottom, "Speed", 240, Colors.Orange);
        private static CustomTimer fireGiantTimer = new CustomTimer(ContentPanel, 2, 0, 222, 222, new Thickness(0, 12, 6, 0), HorizontalAlignment.Left, VerticalAlignment.Bottom, "Fire Giant", 300, new Color() { A = 255, R = 128, G = 0, B = 0 });
        private static CustomTimer goldFuryTimer = new CustomTimer(ContentPanel, 2, 1, 222, 222, new Thickness(6, 12, 0, 0), HorizontalAlignment.Right, VerticalAlignment.Bottom, "Gold Fury", 300, Colors.Green);
        public TimerPage()
        {
            InitializeComponent();
            checkForActiveProducts();
            LayoutRoot.Background = new SolidColorBrush() { Color = Colors.Black };
            if (!running)
            {
                GlobalData.setManaTimer(manaTimer);
                GlobalData.setAttackTimer(attackTimer);
                GlobalData.setCDRTimer(cdrTimer);
                GlobalData.setSpeedTimer(speedTimer);
                GlobalData.setFireGiantTimer(fireGiantTimer);
                GlobalData.setGoldFuryTimer(goldFuryTimer);
                running = true;
            }
            toast.layout = LayoutRoot;
            toast.title = "SA Timer:";
            toast.content = "Timers will stop working if you leave SA!";
            toast.size = 50;
            toast.show();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GlobalData.currentGrid = LayoutRoot;
            
            try
            {
                Grid.SetRow(ContentPanel, 1);
                if (!LayoutRoot.Children.Contains(ContentPanel))
                {
                    LayoutRoot.Children.Add(ContentPanel);
                }
                manaTimer.fillRect.Tap += ManaTimer_Tap;
                manaTimer.rect.Tap += ManaTimer_Tap;
                manaTimer.counter.Tap += ManaTimer_Tap;

                attackTimer.fillRect.Tap += AttackTimer_Tap;
                attackTimer.rect.Tap += AttackTimer_Tap;
                attackTimer.counter.Tap += AttackTimer_Tap;

                cdrTimer.fillRect.Tap += CDRTimer_Tap;
                cdrTimer.rect.Tap += CDRTimer_Tap;
                cdrTimer.counter.Tap += CDRTimer_Tap;

                speedTimer.fillRect.Tap += SpeedTimer_Tap;
                speedTimer.rect.Tap += SpeedTimer_Tap;
                speedTimer.counter.Tap += SpeedTimer_Tap;

                fireGiantTimer.fillRect.Tap += FireGiantTimer_Tap;
                fireGiantTimer.rect.Tap += FireGiantTimer_Tap;
                fireGiantTimer.counter.Tap += FireGiantTimer_Tap;

                goldFuryTimer.fillRect.Tap += GoldFuryTimer_Tap;
                goldFuryTimer.rect.Tap += GoldFuryTimer_Tap;
                goldFuryTimer.counter.Tap += GoldFuryTimer_Tap;

                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if(LayoutRoot.Children.Contains(ContentPanel))
            {
                LayoutRoot.Children.Remove(ContentPanel);
            }
            manaTimer.fillRect.Tap -= ManaTimer_Tap;
            manaTimer.rect.Tap -= ManaTimer_Tap;
            manaTimer.counter.Tap -= ManaTimer_Tap;
            attackTimer.fillRect.Tap -= AttackTimer_Tap;
            attackTimer.rect.Tap -= AttackTimer_Tap;
            attackTimer.counter.Tap -= AttackTimer_Tap;
            cdrTimer.fillRect.Tap -= CDRTimer_Tap;
            cdrTimer.rect.Tap -= CDRTimer_Tap;
            cdrTimer.counter.Tap -= CDRTimer_Tap;
            speedTimer.fillRect.Tap -= SpeedTimer_Tap;
            speedTimer.rect.Tap -= SpeedTimer_Tap;
            speedTimer.counter.Tap -= SpeedTimer_Tap;
            fireGiantTimer.fillRect.Tap -= FireGiantTimer_Tap;
            fireGiantTimer.rect.Tap -= FireGiantTimer_Tap;
            fireGiantTimer.counter.Tap -= FireGiantTimer_Tap;
            goldFuryTimer.fillRect.Tap -= GoldFuryTimer_Tap;
            goldFuryTimer.rect.Tap -= GoldFuryTimer_Tap;
            goldFuryTimer.counter.Tap -= GoldFuryTimer_Tap;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        }
        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            purchaser.invokePurchaceInterface(LayoutRoot, "RmAdsPID");
        }

        private void ManaTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            manaTimer.startTick();
            if (manaTimer.isComplete)
            {
                manaTimer.reset();
                manaTimer.startTick();
            }
        }

        private void AttackTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            attackTimer.startTick();
            if (attackTimer.isComplete)
            {
                attackTimer.reset();
                attackTimer.startTick();
            }
        }

        private void CDRTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            cdrTimer.startTick();
            if (cdrTimer.isComplete)
            {
                cdrTimer.reset();
                cdrTimer.startTick();
            }
        }

        private void SpeedTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            speedTimer.startTick();
            if (speedTimer.isComplete)
            {
                speedTimer.reset();
                speedTimer.startTick();
            }
        }

        private void FireGiantTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            fireGiantTimer.startTick();
            if (fireGiantTimer.isComplete)
            {
                fireGiantTimer.reset();
                fireGiantTimer.startTick();
            }
        }

        private void GoldFuryTimer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            goldFuryTimer.startTick();
            if (goldFuryTimer.isComplete)
            {
                goldFuryTimer.reset();
                goldFuryTimer.startTick();
            }
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