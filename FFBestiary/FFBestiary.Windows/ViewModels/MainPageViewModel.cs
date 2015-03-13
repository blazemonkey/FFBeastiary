using FFBestiary.Interfaces;
using FFBestiary.Models;
using FFBestiary.Services.SQLiteService;
using FFBestiary.Extensions;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.ViewModels
{
    public class MainPageViewModel : ViewModel, IMainPageViewModel
    {        
        private ISqlLiteService _localDb;
        private string _title;
        private ObservableCollection<Enemy> _enemies;
        private ObservableCollection<Game> _games;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<Enemy> Enemies
        {
            get { return _enemies; }
            set { SetProperty(ref _enemies, value); }
        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }

        public DelegateCommand GetEnemyCommand { get; set; }
        public DelegateCommand<Game> GameClickCommand { get; set; }

        public MainPageViewModel(ISqlLiteService localDb)
        {
            _localDb = localDb;
            Title = "FINAL FANTASY BEASTIARY";
            Enemies = new ObservableCollection<Enemy>();
            Games = new ObservableCollection<Game>();
            

            GetEnemyCommand = new DelegateCommand(ExecuteGetEnemyCommand, () => true);
            GameClickCommand = new DelegateCommand<Game>(ExecuteGameClickCommand, x => true);
        }

        private async Task Initialize()
        {
            var enemies = await _localDb.GetAllEnemies();
            var games = await _localDb.GetAllGames();

            enemies.ForEach(x => Enemies.Add(x));
            games.ForEach(x => Games.Add(x));
        }

        private async void ExecuteGetEnemyCommand()
        {
            var enemy = await _localDb.GetEnemyById(1);

        }

        private async void ExecuteGameClickCommand(Game game)
        {

        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            await Initialize();
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
