﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using JetBrains.Annotations;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.App.Model
{
    public class PasswordDataDisplay : PasswordData, INotifyPropertyChanged
    {
        private readonly Timer displayTimer;
        private bool display;
        private string passwordDisplay;

        public PasswordDataDisplay(PasswordData baseData)
        {
            Password = baseData.Password;
            Comments = baseData.Comments;
            Description = baseData.Description;
            displayTimer = new Timer(2000) {AutoReset = false};
            displayTimer.Elapsed += (sender, args) => Display = false;
            Display = false;
        }

        public string PasswordDisplay
        {
            get => passwordDisplay;
            set => SetProperty(ref passwordDisplay, value);
        }

        public bool Display
        {
            get => display;
            set
            {
                displayTimer.Enabled = value;
                if (value)
                {
                    PasswordDisplay = Encryption.DecryptString(Password, App.loginPw);
                }
                else
                {
                    PasswordDisplay = string.Empty;
                    for (int i = Encryption.DecryptString(Password, App.loginPw).Length - 1; i >= 0; i--)
                    {
                        PasswordDisplay += '•';
                    }
                }

                SetProperty(ref display, value);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        // ReSharper disable once RedundantAssignment
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion
    }
}