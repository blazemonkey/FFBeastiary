using FFBestiary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Interfaces
{
    public interface IMainPageViewModel
    {
        string Title { get; set; }        
        ObservableCollection<Game> Games { get; set; }
    }
}
