using FFBestiary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Interfaces
{
    public interface IEnemiesPageViewModel
    {
        Game SelectedGame { get; set; }
        ObservableCollection<Enemy> Enemies { get; set; }
        Enemy SelectedEnemy { get; set; }
        string SelectedSort { get; set; }
    }
}
