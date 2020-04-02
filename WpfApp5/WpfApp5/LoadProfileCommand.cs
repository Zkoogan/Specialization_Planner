using System;
using System.Windows.Input;

namespace WpfApp5
{
    internal class LoadProfileCommand : ICommand
    {
        private Action<object> executeLoadProfile;
        private Func<object, bool> canExecuteOpenWindow;

        public LoadProfileCommand(Action<object> executeLoadProfile, Func<object, bool> canExecuteOpenWindow)
        {
            this.executeLoadProfile = executeLoadProfile;
            this.canExecuteOpenWindow = canExecuteOpenWindow;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeLoadProfile(parameter);
        }
    }
}