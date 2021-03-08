namespace REFame.PasswordManagement.ProgressBar.Contracts
{
    public interface IProgressBar
    {
        void Show(int startValue = 0);
        void Close();
        void SetProgress(int value);
        void SetTitle(TitleType type);
        int Progress { get; }
    }
}