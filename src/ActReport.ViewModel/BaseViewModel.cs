using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ActReport.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IController _controller;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(IController controller)
        {
            this._controller = controller;
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
