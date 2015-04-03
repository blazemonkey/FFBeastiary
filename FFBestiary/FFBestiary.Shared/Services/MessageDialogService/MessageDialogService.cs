using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task ShowYesNo(string text, Action executeOnYes)
        {
            var dialog = new MessageDialog(text);
            dialog.Commands.Add(new UICommand("Yes", delegate(IUICommand command)
            {
                executeOnYes.Invoke();
            }));
            dialog.Commands.Add(new UICommand("No"));
            await dialog.ShowAsync();
        }
    }
}
