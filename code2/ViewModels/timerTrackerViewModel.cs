using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace code2
{
    class timerTrackerViewModel
    {
        #region Public Properties
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand GetTimeCommand { get; set; }
        public ICommand ClearTimeCommand { get; set; }

        public string currentTime { get; set; }

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
            ClearTimeCommand = new RelayCommand(ClearTimeFunction);

        }

        


        #endregion

        #region Commands

        private void StartFunc()
        {
            timer.Start();
            stopWatch.Start();
            StartCommand.CanExecute(false);
            StopCommand.CanExecute(true);
        }
        private void StopFunc()
        {
            timer.Stop();
            stopWatch.Stop();
            StartCommand.CanExecute(true);
            StopCommand.CanExecute(false);
        }
        private void ClearFunc()
        {
            stopWatch.Reset();
            currentTime = "00:00:00";
        }

        private void GetTimeFunc()
        {
            throw new NotImplementedException();
        }
        private void ClearTimeFunction()
        {
            throw new NotImplementedException();
        }


        #endregion
        #region Helpers Function

        private void UpdateTime(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }
        #endregion

    }
}
