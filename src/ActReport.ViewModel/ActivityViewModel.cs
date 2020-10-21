using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ActReport.ViewModel
{
    public class ActivityViewModel : BaseViewModel
    {
        private readonly Employee _employee;
        private ObservableCollection<Activity> _activities { get; set; }
        
        private Activity _selectedActivity;

        public ObservableCollection<Activity> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                OnPropertyChanged(nameof(Activities));
            }
        }
        public string FullName { get => $"{ _employee.FirstName} {_employee.LastName}"; }
        public Activity SelectedActivity 
        { 
            get => _selectedActivity;

            set 
            {
                _selectedActivity = value;
                OnPropertyChanged(nameof(SelectedActivity));
            } 
        }

        public ActivityViewModel(IController controller, Employee employee) : base(controller)
        {
            _employee = employee;
            LoadActivities();
            Activities.CollectionChanged += Activities_CollectionChanged;
        }
        private void LoadActivities()
        {
            using IUnitOfWork uow = new UnitOfWork();
            Activities = new ObservableCollection<Activity>(
                uow.ActivityRepository
                    .Get(
                    filter: x => x.Employee_Id == _employee.Id,
                    orderBy: x => x.OrderBy(activity => activity.Date)
                                    .ThenBy(activity => activity.StartTime)));
        }

        private void Activities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                using(IUnitOfWork uow = new UnitOfWork())
                {
                    foreach(var item in e.OldItems)
                    {
                        uow.ActivityRepository.Delete((item as Activity).Id);
                    }

                    uow.Save();
                }
            }
        }

        private ICommand _cmdEditActivities;
        public ICommand CmdEditActivities
        {
            get
            {
                if (_cmdEditActivities == null)
                {
                    _cmdEditActivities = new RelayCommand(
                      execute: _ => _controller.ShowWindow(new ActivityCreateAndEditModel(_controller, SelectedActivity)),
                      canExecute: _ => SelectedActivity != null);
                }

                return _cmdEditActivities;
            }
            set { _cmdEditActivities = value; }
        }

        private ICommand _cmdCreateActivity;
        public ICommand CmdCreateActivity
        {
            get
            {
                if (_cmdCreateActivity == null)
                {
                    _cmdCreateActivity = new RelayCommand(
                      execute: _ => _controller.ShowWindow(new ActivityCreateAndEditModel(_controller, SelectedActivity)),
                      canExecute: _ => SelectedActivity != null);
                }

                return _cmdCreateActivity;
            }
            set { _cmdCreateActivity = value; }
        }

        private ICommand _cmdDeleteActivity;
        public ICommand CmdDeleteActivity
        {
            get
            {
                if (_cmdDeleteActivity == null)
                {
                    _cmdDeleteActivity = new RelayCommand(
                      execute: _ => _controller.ShowWindow(new ActivityCreateAndEditModel(_controller, SelectedActivity)),
                      canExecute: _ => SelectedActivity != null);
                }

                return _cmdDeleteActivity;
            }
            set { _cmdDeleteActivity = value; }
        }

    }
}
