using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.ProgressBar.View;
using REFame.PasswordManagement.WpfBase.Localization;

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

        public void SetTitle(TitleType type)
        {
            view.ViewModel.Title = type switch
            {
                TitleType.Installing => Loc.ProgressBar_LabelTitle_Content_Installing,
                TitleType.Downloading => Loc.ProgressBar_LabelTitle_Content_Downloading,
                _ => "#Missing translation#"
            };
        }

        public void Close()
        {
            view.Close();
        }

        public int Progress { get; private set; }
    }
}