// Copyright (c) IPG Photonics Corporation, Inc.  All Rights Reserved.
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace WpfApp1.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDispatcherSettable
    {
        private static Dispatcher? dispatcher;
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ViewModelBase(Dispatcher dispatcher)
        {
            ViewModelBase.dispatcher = dispatcher;
        }
        public ViewModelBase()
        {
        }

        public void BeginInvoke(Action action, DispatcherPriority dispPrio = DispatcherPriority.Normal)
        {
            dispatcher?.BeginInvoke(action, dispPrio);
        }

        public void Invoke(Action action, DispatcherPriority dispPrio = DispatcherPriority.Normal)
        {
            if (dispatcher?.CheckAccess() ?? false)
            {
                action();
            }
            else
            {
                dispatcher?.Invoke(action, dispPrio);
            }
        }
        public void SetDispatcher(Dispatcher dispy)
        {
            dispatcher = dispy;
        }
    }
}