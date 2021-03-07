using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace REFame.PasswordManagement.WpfBase
{
    public class PwmWindow : Window
    {
        protected PwmWindow()
        {
            Resources.MergedDictionaries.Clear();


            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                Resources.MergedDictionaries.Add(new CustomColorTheme
                {
                    BaseTheme = BaseTheme.Light,
                    PrimaryColor = Colors.Blue,
                    SecondaryColor = Colors.Green
                });
                Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source =
                        new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
                });
            }
            else
            {
                foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
                {
                    if (!Resources.MergedDictionaries.Contains(dictionary))
                    {
                        Resources.MergedDictionaries.Add(dictionary);
                    }
                }
            }
        }
    }
}