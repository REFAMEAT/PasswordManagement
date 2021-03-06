﻿using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.SettingFactories.Contracts;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.SettingFactories
{
    public class ThemeSettingsFactory : IThemeSettingsFactory
    {
        private ThemeSettingsViewModel viewModel;
        private SettingMediator mediator;
        private ISettingService<ThemeData> setting;

        public ThemeSettingsFactory(ISettingService<ThemeData> setting)
        {
            this.setting = setting;
        }

        //public ISettingService<ThemeData> OverrideSettingService { get; set; }

        public BindableBase GetViewModel()
        {
            return viewModel ??= new ThemeSettingsViewModel(setting);
        }

        public SettingMediator GetMediator()
        {
            if (mediator == null)
            {
                mediator = new SettingMediator();
                viewModel.SettingMediator = mediator;
            }

            return mediator;
        }

        public string GetHeader()
        {
            return "Theme";
        }
    }
}