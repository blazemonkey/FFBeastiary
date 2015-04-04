using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Services.MessageDialogService
{
    public interface IMessageDialogService
    {
        void Show(string text);
        Task<bool> ShowYesNo(string text, Action executeOnYes);
    }
}
