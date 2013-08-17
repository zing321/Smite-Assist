using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;

namespace Smite_Helper
{
    public class CustomToast
    {
        public string title { get; set; }
        public string content { get; set; }
        public Uri uri { get; set; }
        public int size { get; set; }
        public Grid layout { get; set; }
        private int time = 0;      
        private Grid toastBody;
        private TextBlock toastText;
        private DispatcherTimer timer;
        public CustomToast()
        {
            toastBody = new Grid();
            toastBody.Background = new SolidColorBrush() { Color = (Color)Application.Current.Resources["PhoneAccentColor"] };
            toastBody.Visibility = Visibility.Collapsed;
            toastBody.VerticalAlignment = VerticalAlignment.Top;
            toastBody.Tap += toastTap;
            size = 70;
            Grid.SetRowSpan(toastBody, 2);

            toastText = new TextBlock();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += tick;
            toastText.HorizontalAlignment = HorizontalAlignment.Left;
            toastText.VerticalAlignment = VerticalAlignment.Bottom;
            toastText.Foreground = new SolidColorBrush() { Color = Colors.White };
            toastText.Tap += toastTap;
            toastBody.Children.Add(toastText);
        }

        private void toastTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(uri!=null)
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(uri);
        }
        public void show()
        {
            toastText.Inlines.Clear();
            toastText.Inlines.Add(new Run() { Text = " " + title + " ", FontWeight = FontWeights.Bold });
            toastText.Inlines.Add(new Run() { Text = content });
            layout.Children.Add(toastBody);
            toastBody.Height = size;
            toastBody.Visibility = Visibility.Visible;
            timer.Start();
        }
        private void tick(object sender, EventArgs e)
        {
            time++;
            if (time >= 5)
            {
                timer.Stop();
                time = 0;
                toastBody.Visibility = Visibility.Collapsed;
                layout.Children.Remove(toastBody);
            }
        }

    }
}
