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

        public DelegateCommand<Game> GameClickCommand { get; set; }

        public MainPageViewModel(ISqlLiteService localDb, INavigationService navigationService, IImgurService imgur)
        {
            _localDb = localDb;
            _navigationService = navigationService;
            _imgur = imgur;

            Title = "FINAL FANTASY BEASTIARY";
            Games = new ObservableCollection<Game>();            

            GameClickCommand = new DelegateCommand<Game>(ExecuteGameClickCommand, x => true);
        }

        private async Task Initialize()
        {
            var games = await _localDb.GetAllGames();
            games.ForEach(x => Games.Add(x));

            var albums = await _imgur.GetAllAlbums();
            var images = await _imgur.GetAlbumImages(albums.First(x => x.Title == "FFVII").Id);
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
