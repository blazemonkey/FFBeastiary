using FFBestiary.Interfaces;
using FFBestiary.Models;
using FFBestiary.Extensions;
using FFBestiary.Services.NavigationService;
using FFBestiary.Services.SQLiteService;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFBestiary.Services.MessageDialogService;

namespace FFBestiary.ViewModels
{
    public class EnemiesPageViewModel : ViewModel, IEnemiesPageViewModel
    {
        private ISqlLiteService _localDb;
        private INavigationService _navigationService;
        private IMessageDialogService _dialog;

        private Game _selectedGame;
        private ObservableCollection<Enemy> _enemies;
        private Enemy _selectedEnemy;
        private IStats _selectedEnemyStats;
        private ObservableCollection<IStats> _enemyStats;
        private string _selectedSort;
        private ObservableCollection<string> _sortOptions;

        public Game SelectedGame
        {
            get { return _selectedGame; }
            set { SetProperty(ref _selectedGame, value); }
        }

        public ObservableCollection<Enemy> Enemies
        {
            get { return _enemies; }
            set { SetProperty(ref _enemies, value); }
        }

        public Enemy SelectedEnemy
        {
            get { return _selectedEnemy; }
            set { SetProperty(ref _selectedEnemy, value); }
        }

        public IStats SelectedEnemyStats
        {
            get { return _selectedEnemyStats; }
            set { SetProperty(ref _selectedEnemyStats, value); }
        }

        public ObservableCollection<IStats> EnemyStats
        {
            get { return _enemyStats; }
            set { SetProperty(ref _enemyStats, value); }
        }

        public string SelectedSort
        {
            get { return _selectedSort; }
            set 
            { 
                SetProperty(ref _selectedSort, value);
                ChangeSorting(value);
            }
        }

        public ObservableCollection<string> SortOptions
        {
            get { return _sortOptions; }
            set { SetProperty(ref _sortOptions, value); }
        }

        public DelegateCommand SortEnemiesCommand { get; set; }
        public DelegateCommand<Enemy> ShowEnemyCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand FavouritesCommand { get; set; }

        public EnemiesPageViewModel(ISqlLiteService localDb, INavigationService navigationService, IMessageDialogService dialog)
        {
            _localDb = localDb;
            _navigationService = navigationService;
            _dialog = dialog;

            Enemies = new ObservableCollection<Enemy>();
            SortOptions = new ObservableCollection<string>(new [] { "A-Z", "ORDER OF APPEARANCE", "LEVEL" });
            SelectedSort = "A-Z";

            SortEnemiesCommand = new DelegateCommand(ExecuteSortEnemiesCommand, () => true);
            ShowEnemyCommand = new DelegateCommand<Enemy>(ExecuteShowEnemyCommand, x => true);
            BackCommand = new DelegateCommand(ExecuteBackCommand, () => true);
            FavouritesCommand = new DelegateCommand(ExecuteFavouritesCommand, () => true);
        }

        private void ChangeSorting(string method)
        {
            if (!(Enemies.Any()))
                return;

            switch (method)
            {
                case "A-Z":
                    Enemies = new ObservableCollection<Enemy>(Enemies.OrderBy(x => x.Name));
                    break;
                case "ORDER OF APPEARANCE":
                    Enemies = new ObservableCollection<Enemy>(Enemies.OrderBy(x => x.DisplayOrder));
                    break;
            }
        }

        private void ExecuteSortEnemiesCommand()
        {
            if (!(Enemies.Any()))
                return;

            Enemies = new ObservableCollection<Enemy>(Enemies.Reverse());
        }

        private void ExecuteShowEnemyCommand(Enemy enemy)
        {
            SelectedEnemy = enemy;
            EnemyStats = new ObservableCollection<IStats>(SelectedEnemy.Stats);
            SelectedEnemyStats = EnemyStats.Any() ? EnemyStats.First() : null;
        }

        private void ExecuteBackCommand()
        {
            _navigationService.GoBack();
        }

        private void ExecuteFavouritesCommand()
        {
            if (SelectedEnemy.IsFavourite)
                RemoveFromFavourites(SelectedEnemy.Id);
            else
                AddToFavourites(SelectedEnemy.Id);            
        }

        private async void AddToFavourites(int enemyId)
        {
            var result = await _dialog.ShowYesNo(string.Format("Add {0} to Favourites?", SelectedEnemy.Name), () => _localDb.AddEnemyToFavourites(enemyId));
            if (result)
                SelectedEnemy.IsFavourite = true;
        }

        private async void RemoveFromFavourites(int enemyId)
        {
            var result = await _dialog.ShowYesNo(string.Format("Remove {0} from Favourites?", SelectedEnemy.Name), () => _localDb.RemoveEnemyFromFavourites(enemyId));
            if (result)
                SelectedEnemy.IsFavourite = false;
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            var gameId = int.Parse(navigationParameter.ToString());
            SelectedGame = await _localDb.GetGameById(gameId);

            var enemies = await _localDb.GetEnemiesByGameId(gameId);
            enemies.OrderBy(x => x.Name).ForEach(x => Enemies.Add(x));

            if (Enemies.Any())
            {
                SelectedEnemy = Enemies.First();
                EnemyStats = new ObservableCollection<IStats>(SelectedEnemy.Stats);
                SelectedEnemyStats = EnemyStats.First();
            }                           

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
