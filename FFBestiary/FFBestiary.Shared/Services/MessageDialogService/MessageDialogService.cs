using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;

namespace FFBestiary.Services.MessageDialogService
{
    public class MessageDialogService : IMessageDialogService
    {
        public async void Show(string text)
        {
            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }
    }
}
