using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.ProgressBar.ViewModel
{
    public class ProgressViewModel : BindableBase
    {
        private int progress;

        public ProgressViewModel()
        {
            
        }

        public int Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }
    }
}