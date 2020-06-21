using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp5.ViewModels;

namespace WpfApp5
{
    class MainViewModel : BaseViewModel
    {
        public ICommand SearchButtonCommand { get; set; }

        public ICommand AddButtonCommand { get; set; }

        public ICommand NavigateHomeCommand { get; set; }

        public ICommand NavigateSearchCommand { get; set; }

        public ICommand NavigateProfileCommand { get; set; }

        public ICommand NavigateStatisticsCommand { get; set; }

        public ObservableCollection<Course> Chosen { get; set; }

        public RelayCommand<Window> CloseCommand { get; private set; }

        public RelayCommand<Window> MinimizeCommand { get; private set; }

        public object selectedViewModel { 
            get
            {
                return SelectedViewModel;
            }
            set
            {
                SelectedViewModel = value;
                OnPropertyChanged("selectedViewModel");
            }
        }

        private object SelectedViewModel;

        public SearchViewModel svm;

        public HomeViewModel hvm;

        public ProfileViewModel pvm;

        public StatisticsViewModel stvm;

        public String NumCourses
        {
            get
            {
                return _hidden;
            }
            set
            {
                _hidden = value;
                OnPropertyChanged("NumCourses");
            }
        }
        private String _hidden { get; set; }

        public MainViewModel()
        {
            Chosen = new ObservableCollection<Course>();
            Messenger.Default.Register<ObservableCollection<Course>>(this, (action) => UpdateCourseList(action));
            Messenger.Default.Register<Course>(this, (action) => RemoveFromCourseList(action));
            CloseCommand = new RelayCommand<Window>(this.CloseWindow);
            MinimizeCommand = new RelayCommand<Window>(this.MinimizeWindow);

            NavigateHomeCommand = new Command(ExecuteNavigateHome, canExecuteNavigateHome);
            NavigateSearchCommand = new Command(ExecuteNavigateSearch, CanExecuteNavigateSearch);
            NavigateProfileCommand = new Command(ExecuteNavigateProfile, CanExecuteNavigateProfile);
            NavigateStatisticsCommand = new Command(ExecuteNavigateStatistics, CanExecuteNavigateStatistics);

            svm = new SearchViewModel(Chosen);
            stvm = new StatisticsViewModel(Chosen);
            hvm = new HomeViewModel();
            pvm = new ProfileViewModel();
            selectedViewModel = hvm;
            NumCourses = "0";
        }

        private void ExecuteNavigateHome(object parameter)
        {
            selectedViewModel = hvm;
        }
        private Boolean canExecuteNavigateHome(object obj)
        {
            return true;
        }

        private void ExecuteNavigateSearch(object parameter)
        {
            selectedViewModel = svm;
        }
        private Boolean CanExecuteNavigateSearch(object obj)
        {
            return true;
        }

        private void ExecuteNavigateProfile(object parameter)
        {
            selectedViewModel = pvm;
        }
        private Boolean CanExecuteNavigateProfile(object obj)
        {
            return true;
        }

        private void ExecuteNavigateStatistics(object parameter)
        {
            selectedViewModel = stvm;
        }

        private Boolean CanExecuteNavigateStatistics(object obj)
        {
            return true;
        }

        private void RemoveFromCourseList(Course action)
        {
            NumCourses = Chosen.Count() + "";
        }

        private void MinimizeWindow(Window obj)
        {
            obj.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void UpdateCourseList(ObservableCollection<Course> action)
        {
            NumCourses = Chosen.Count().ToString();
        }

    }
}
