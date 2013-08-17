using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Smite_Helper
{
    public class CustomTimer
    {
        public bool isComplete { get; private set;}
        public int time { get; set; }
        public int currentTime { get; private set; }
        public bool isRunning {get; private set;}
        public Rectangle rect=new Rectangle();
        public TextBlock counter=new TextBlock();
        public Rectangle fillRect { get; set; }
        private CustomToast toast;
        private VibrateController vibrator = VibrateController.Default;
        private string text = "";
        private Timer timer;
        public CustomTimer(Grid layout, int row, int col, int height, int width, Thickness margin, HorizontalAlignment hAlignment, VerticalAlignment vAlignment, string nText, int nTime, Color nColor)
        {
            fillRect = new Rectangle();
            isRunning = false;
            currentTime = 0;
            isComplete = false;
            rect.Height = height;
            rect.Width = width;
            rect.Margin = margin;
            rect.HorizontalAlignment = hAlignment;
            rect.VerticalAlignment = vAlignment;
            rect.Stroke = new SolidColorBrush() { Color = nColor };
            rect.StrokeThickness = 3;
            rect.Fill = new SolidColorBrush() { Color = Colors.Black };
            Grid.SetColumn(rect, col);
            Grid.SetRow(rect, row);
            layout.Children.Add(rect);

            time = nTime;
            fillRect.Width = rect.Width;
            fillRect.Height = 0;
            fillRect.Margin = rect.Margin;
            fillRect.HorizontalAlignment = rect.HorizontalAlignment;
            fillRect.VerticalAlignment = rect.VerticalAlignment;
            Grid.SetColumn(fillRect, col);
            Grid.SetRow(fillRect, row);
            SolidColorBrush colorBrush = new SolidColorBrush() { Color = nColor };
            fillRect.Fill = colorBrush;
            fillRect.Tap += fillRect_Tap;
            layout.Children.Add(fillRect);

            counter.Text = nText;
            text = nText;
            counter.Margin = margin;
            counter.HorizontalAlignment = HorizontalAlignment.Center;
            counter.VerticalAlignment = VerticalAlignment.Center;
            counter.FontSize = 30;
            counter.Foreground = new SolidColorBrush() { Color = Colors.White };
            Grid.SetColumn(counter, col);
            Grid.SetRow(counter, row);
            layout.Children.Add(counter);
        }

        void fillRect_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (isComplete)
            {
                reset();
                startTick();
            }
        }

        private void timer_Tick(Object sender)
        {
                AutoResetEvent autoEvent = (AutoResetEvent)sender;
                currentTime++;
                counter.Dispatcher.BeginInvoke(delegate()
                {
                    counter.Text = Convert.ToString(time - currentTime);
                    double gain = rect.Height / time;
                    fillRect.Height += gain;
                });
                if (currentTime >= time)
                {
                    stopTick();
                    isComplete = true;
                    counter.Dispatcher.BeginInvoke(delegate()
                    {
                        onReady(EventArgs.Empty);
                        vibrator.Start(TimeSpan.FromSeconds(0.5));
                        counter.Text = text + " Ready!";
                    });
                }
        }
        public void startTick()
        {
            if (!isRunning)
            {
                TimerCallback tcb= timer_Tick;
                AutoResetEvent autoEvent=new AutoResetEvent(false);
                timer = new Timer(tcb, autoEvent, 1000, 1000);
                counter.Text = Convert.ToString(time);
            }
            isRunning = true;
        }
        public void stopTick()
        {
            if (isRunning)
            {
                timer.Dispose();
            }
            isRunning = false;
        }
        public void reset()
        {
            fillRect.Height = 0;
            currentTime = 0;
            isComplete = false;
        }
        public event EventHandler ready;
        protected virtual void onReady(EventArgs e)
        {
            if (isComplete)
                ready(this, e);
        }
    }
}
