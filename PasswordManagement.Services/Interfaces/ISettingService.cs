using System;
using System.Collections.Generic;
using PasswordManagement.Model.Setting;

namespace PasswordManagement.Services.Interfaces
{
    public interface ISettingService<T>
    {
        T Load();
        void Save(T data);
        void OnSaved(EventArgs args);

        event EventHandler Saved;
    }
}