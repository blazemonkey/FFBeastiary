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
using FFBestiary.Services.NavigationService;
using FFBestiary.Services.ImgurService;

namespace FFBestiary.ViewModels
{
    public class MainPageViewModel : ViewModel, IMainPageViewModel
    {        
        private ISqlLiteService _localDb;
        private INavigationService _navigationService;
        private IImgurService _imgur;
        private string _title;
        private ObservableCollection<Game> _games;
        private ObservableCollection<Enemy> _favourites;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }

        public ObservableCollection<Enemy> Favourites
        {
            get { return _favourites; }
            set { SetProperty(ref _favourites, value); }
        }

        public DelegateCommand<Game> GameClickCommand { get; set; }

        public MainPageViewModel(ISqlLiteService localDb, INavigationService navigationService, IImgurService imgur)
        {
            _localDb = localDb;
            _navigationService = navigationService;
            _imgur = imgur;

            Title = "FINAL FANTASY BEASTIARY";
            Games = new ObservableCollection<Game>();
            Favourites = new ObservableCollection<Enemy>();

            GameClickCommand = new DelegateCommand<Game>(ExecuteGameClickCommand, x => true);
        }

        private async Task Initialize()
        {
            Favourites.Clear();
            var favourites = await _localDb.GetFavourites();
            foreach (var fav in favourites)
            {
                var enemy = await _localDb.GetEnemyById(fav.EnemyId);
                Favourites.Add(enemy);
            }

            if (Games.Any())
                return;

            var games = await _localDb.GetAllGames();
            games.ForEach(x => Games.Add(x));

            await _imgur.Initialize();
        }

        private void ExecuteGameClickCommand(Game game)
        {
            _navigationService.Navigate(Experiences.Enemies, game.Id);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            await Initialize();
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
