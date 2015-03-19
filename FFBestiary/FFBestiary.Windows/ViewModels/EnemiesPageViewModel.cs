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

namespace FFBestiary.ViewModels
{
    public class EnemiesPageViewModel : ViewModel, IEnemiesPageViewModel
    {
        private ISqlLiteService _localDb;
        private INavigationService _navigationService;

        private Game _selectedGame;
        private ObservableCollection<Enemy> _enemies;
        private Enemy _selectedEnemy;
        private IStats _selectedEnemyStats;
        private string _selectedSort;

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

        public string SelectedSort
        {
            get { return _selectedSort; }
            set 
            { 
                SetProperty(ref _selectedSort, value);
                ChangeSorting(value);
            }
        }

        public DelegateCommand SortEnemiesCommand { get; set; }
        public DelegateCommand<Enemy> ShowEnemyCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }

        public EnemiesPageViewModel(ISqlLiteService localDb, INavigationService navigationService)
        {
            _localDb = localDb;
            _navigationService = navigationService;

            Enemies = new ObservableCollection<Enemy>();
            SelectedSort = "A-Z";

            SortEnemiesCommand = new DelegateCommand(ExecuteSortEnemiesCommand, () => true);
            ShowEnemyCommand = new DelegateCommand<Enemy>(ExecuteShowEnemyCommand, x => true);
            BackCommand = new DelegateCommand(ExecuteBackCommand, () => true);
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
            SelectedEnemyStats = SelectedEnemy.Stats.First();
        }

        private void ExecuteBackCommand()
        {
            _navigationService.GoBack();
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
                SelectedEnemyStats = SelectedEnemy.Stats.First();
            }
                

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
