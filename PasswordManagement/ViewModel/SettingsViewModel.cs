using System;
using System.Collections.Generic;
using System.Windows.Documents;
using MaterialDesignThemes.Wpf;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class SettingsViewModel : NotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            List<string> items = new List<string>();
            foreach (BaseTheme item in Enum.GetValues(typeof(BaseTheme)))
            {
                items.Add(item.ToString());
            }

            ThemeItems = items;
        }

        public List<string> ThemeItems { get; set; }
    }
}