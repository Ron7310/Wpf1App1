// Copyright (c) IPG Photonics Corporation, Inc.  All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Timers;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string? currentTime;
        public object syncLock;
        System.Timers.Timer timer;

        public MainViewModel()
        {
            syncLock = new object();
            Title = "Clock!";
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1 second
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();

            SetTime();
        }

        private void SetTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            lock (syncLock)
            {
                SetTime();
            }
        }

        public string? CurrentTime
        {
            get
            {
                return currentTime;

            }
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
