using FFBestiary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.DesignTime
{
    public class MainPageViewModel : IMainPageViewModel
    {
        public MainPageViewModel()
        {
            Title = "Final Fantasy Beastiary";
        }

        public string Title { get; set; }
    }
}
