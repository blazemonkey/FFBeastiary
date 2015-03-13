using FFBestiary.Interfaces;
using FFBestiary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.DesignTime
{
    public class MainPageViewModel : IMainPageViewModel
    {
        public string Title { get; set; }
        public ObservableCollection<Enemy> Enemies { get; set; }
        public ObservableCollection<Game> Games { get; set; }

        public MainPageViewModel()
        {
            Title = "FINAL FANTASY BEASTIARY";
            Enemies = new ObservableCollection<Enemy>();
            Games = new ObservableCollection<Game>()
            {
                new Game { FullName = "Final Fantasy VII", ImagePath = "ms-appx:///Images/Series/FFVII.png" }
            };

            Enemies.Add(new Enemy { Id = 1, Name = "Nana" });            
        }


    }
}
