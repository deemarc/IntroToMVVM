﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace code2
{
    class timerTrackerViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        #region Public Properties
        public bool btnStartEnable { get; private set; }
        public bool btnStopEnable { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand GetTimeCommand { get; private set; }
        public ICommand ClearTimeCommand { get; private set; }

        private string _currentTime = "00:00:00";
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                if (_currentTime == value)
                    return;
                _currentTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentTime)));
            }
        }

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
            btnStopEnable = false;
            btnStartEnable = true;

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
                CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }
        #endregion

    }
}
