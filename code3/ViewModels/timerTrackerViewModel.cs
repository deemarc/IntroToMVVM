using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace code3
{
    class timerTrackerViewModel: BaseViewModel
    {
        #region Public Properties
        public bool btnStartEnable { get; set; }
        public bool btnStopEnable { get; set; }
        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand GetTimeCommand { get; private set; }
        public ICommand ClearTimeCommand { get; private set; }
        public ObservableCollection<string> SavedTimeList { get; set; }
        public string CurrentTime { get; set; }

        #endregion


        private DispatcherTimer timer = new DispatcherTimer();
        private Stopwatch stopWatch = new Stopwatch();
        #region Constructor
        public timerTrackerViewModel()
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(UpdateTime);
            StartCommand = new RelayCommand(StartFunc);
            StopCommand = new RelayCommand(StopFunc);
            ClearCommand = new RelayCommand(ClearFunc);
            GetTimeCommand = new RelayCommand(GetTimeFunc);
            ClearTimeCommand = new RelayCommand(ClearTimeFunc);
            SavedTimeList = new ObservableCollection<string>();
            btnStopEnable = false;
            btnStartEnable = true;
            CurrentTime = "00:00:00";

        }

        


        #endregion

        #region Commands

        private void StartFunc()
        {
            timer.Start();
            stopWatch.Start();
            btnStartEnable = false;
            btnStopEnable = true;
        }
        private void StopFunc()
        {
            timer.Stop();
            stopWatch.Stop();
            btnStartEnable = true;
            btnStopEnable = false;
        }
        private void ClearFunc()
        {
            stopWatch.Restart();
            CurrentTime = "00:00:00";
        }

        private void GetTimeFunc()
        {
            SavedTimeList.Add(CurrentTime);
        }
        private void ClearTimeFunc()
        {
            SavedTimeList.Clear();
        }


        #endregion
        #region Helpers Function

        private void UpdateTime(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }
        #endregion

    }
}
