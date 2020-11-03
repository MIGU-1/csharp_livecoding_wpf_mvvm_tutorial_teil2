using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ActReport.ViewModel
{
    public class ActivityCreateAndEditModel : BaseViewModel
    {
        private bool _create;
        private Activity _activity;
        private string _activityText;
        private DateTime _date;
        private DateTime _start;
        private DateTime _end;

        public string ActivityText
        {
            get => _activityText;

            set
            {
                _activityText = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;

            set
            {
                _date = value;
                OnPropertyChanged(nameof(ActivityCreateAndEditModel));
            }
        }
        public DateTime Start
        {
            get => _start;

            set
            {
                _start = value;
                OnPropertyChanged(nameof(ActivityCreateAndEditModel));
            }
        }
        public DateTime End
        {
            get => _end;

            set
            {
                _end = value;
                OnPropertyChanged(nameof(ActivityCreateAndEditModel));
            }
        }
        public Activity Activity
        {
            get => _activity;
            set
            {
                _activity = value;
                OnPropertyChanged(nameof(ActivityCreateAndEditModel));
            }
        }

        public ActivityCreateAndEditModel(IController controller, Activity activity, Employee employee) : base(controller)
        {
            if (activity != null)
            {
                _create = false;
                _activity = activity;
                _activityText = activity.ActivityText;
                _date = activity.Date;
                _start = activity.StartTime;
                _end = activity.EndTime;
            }
            else
            {
                _create = true;
                _activity = new Activity
                {
                    Employee = employee,
                    Employee_Id = employee.Id
                };
                _date = DateTime.Now;
                _start = DateTime.Now;
                _end = DateTime.Now;
            }
        }

        private ICommand _cmdSaveActivities;
        public ICommand CmdSaveActivities
        {
            get
            {
                if (_cmdSaveActivities == null)
                {
                    _cmdSaveActivities = new RelayCommand(
                        execute: _ =>
                        {
                            using IUnitOfWork uow = new UnitOfWork();
                            Activity.ActivityText = ActivityText;
                            Activity.Date = Date;
                            Activity.StartTime = Start;
                            Activity.EndTime = End;
                            if (_create)
                                uow.ActivityRepository.Insert(_activity);
                            else
                                uow.ActivityRepository.Update(_activity);
                            uow.Save();
                            _controller.CloseWindow(this);
                        },
                        canExecute: _ => _activity != null);
                }

                return _cmdSaveActivities;
            }
            set => _cmdSaveActivities = value;
        }

        private ICommand _cmdReturnToEmployees;
        public ICommand CmdReturnToEmployees
        {
            get
            {
                if (_cmdReturnToEmployees == null)
                {
                    _cmdReturnToEmployees = new RelayCommand(
                    execute: _ => _controller.CloseWindow(this),
                    canExecute: _ => true);
                }

                return _cmdReturnToEmployees;
            }
            set => _cmdReturnToEmployees = value;
        }

    }
}
