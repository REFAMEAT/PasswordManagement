using REFame.PasswordManagement.ProgressBar.View;

namespace REFame.PasswordManagement.ProgressBar.Contracts
{
    public class ProgressBar : IProgressBar
    {
        private ProgressView view;

        public ProgressBar()
        {
            view = new ProgressView();
        }

        public void Show(int startValue = 0)
        {
            view.ViewModel.Progress = startValue;
            Progress = startValue;
            view.Show();
        }

        public void SetProgress(int value)
        {
            view.Dispatcher.Invoke(() => view.ViewModel.Progress = value);
            Progress = value;
        }

        public void Close()
        {
            view.Close();
        }

        public int Progress { get; private set; }
    }
}