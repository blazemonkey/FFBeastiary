using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Services.NavigationService
{
    public enum Experiences { Main, Enemies }

    public interface INavigationService
    {
        bool Navigate(Experiences experience, object param = null);
        void GoBack();
        bool CanGoBack { get; }
        void ClearHistory();
    }
}
