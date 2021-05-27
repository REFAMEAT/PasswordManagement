using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace REFame.PasswordManagement.WpfBase
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        // ReSharper disable once RedundantAssignment
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            NotifyChanged(propertyName);
        }
    }
}