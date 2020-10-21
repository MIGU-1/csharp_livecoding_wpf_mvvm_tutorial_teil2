using ActReport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActReport.ViewModel
{
    public class ActivityCreateAndEditModel : BaseViewModel
    {
        private Activity _activity;
        public ActivityCreateAndEditModel(IController controller, Activity activity):base(controller)
        {
            _activity = activity;
        }
    }
}
