using FFBestiary.Interfaces;
using FFBestiary.Services.SQLiteService;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.ViewModels
{
    public class MainPageViewModel : ViewModel, IMainPageViewModel
    {
        private string _title;
        private ISqlLiteService _localDb;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand GetEnemyCommand { get; set; }

        public MainPageViewModel(ISqlLiteService localDb)
        {
            Title = "Final Fantasy Beastiary";
            _localDb = localDb;

            GetEnemyCommand = new DelegateCommand(ExecuteGetEnemyCommand, () => true);
        }

        private async void ExecuteGetEnemyCommand()
        {
            var enemy = await _localDb.GetEnemyById(1);
            var enemies = await _localDb.GetAllEnemies();
        }
    }
}
