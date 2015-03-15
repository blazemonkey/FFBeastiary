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
    public class EnemiesPageViewModel : IEnemiesPageViewModel
    {
        public Game SelectedGame { get; set; }
        public ObservableCollection<Enemy> Enemies { get; set; }
        public Enemy SelectedEnemy { get; set; }
        public string SelectedSort { get; set; }

        public EnemiesPageViewModel()
        {
            SelectedGame = new Game()
            {
                Id = 1,
                FullName = "Final Fantasy VII",
                Roman = "VII",
                Numeral = "7",
                AbbrName = "FFVII"
            };

            Enemies = new ObservableCollection<Enemy>() 
            {
                new Enemy() { Id = 1, GameId = 7, Name = "MEOW" },
                new Enemy() { Id = 2, GameId = 7, Name = "WOOF" },
                new Enemy() { Id = 3, GameId = 7, Name = "moo" }
            };

            SelectedEnemy = new Enemy
            {
                Id = 1,
                GameId = 7,
                Name = "Sephiroth"
            };

            SelectedSort = "A-Z";
        }
    }
}
