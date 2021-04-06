using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Physarum.ViewModels
{
    public abstract class Changeable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}