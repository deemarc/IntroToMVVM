using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace introToMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;
        public List<string> saveTimeList { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            saveTimeList = new List<string>();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(UpdateTime);
            DataContext = this;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            stopWatch.Start();
            btn_stop.IsEnabled = true;
            btn_start.IsEnabled = false;
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            stopWatch.Stop();
            btn_stop.IsEnabled = false;
            btn_start.IsEnabled = true;
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Reset();
            txtBlock_timer.Text= "00:00:00";
        }

        void UpdateTime(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                txtBlock_timer.Text = currentTime;
            }

        }

        private void btn_getTime_Click(object sender, RoutedEventArgs e)
        {
            savedTime.Items.Add(txtBlock_timer.Text);
        }

        private void btn_clearTime_Click(object sender, RoutedEventArgs e)
        {
            savedTime.Items.Clear();
        }
    }
}
