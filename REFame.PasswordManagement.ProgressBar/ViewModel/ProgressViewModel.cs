using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.ProgressBar.ViewModel
{
    public class ProgressViewModel : BindableBase
    {
        private int progress;
        private string title;

        public ProgressViewModel()
        {
            
        }

        public int Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}