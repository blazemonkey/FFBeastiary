using FFBestiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Services.SQLiteService
{
    internal class SqlLiteService : ISqlLiteService
    {
        private readonly SQLiteAsyncConnection _conn;

        public SQLiteAsyncConnection Conn
        {
            get { return _conn; }
        }

        public SqlLiteService()
        {
            _conn = new SQLiteAsyncConnection("ffbeastiary.db");
        }

        public async Task InitDb()
        {            
            var createTasks = new Task[]
            {
                _conn.CreateTableAsync<Enemy>(),
                _conn.CreateTableAsync<Game>()
            };

            Task.WaitAll(createTasks);
            await InsertTestDataAsync();
        }

        private async Task InsertTestDataAsync()
        {

            if (await _conn.Table<Enemy>().CountAsync() == 0)
            {
                await _conn.InsertAllAsync(new[] 
                { 
                    new Enemy {Id = 1, Name = "1st Ray", ImagePath = "1stray.png" },
                    new Enemy{ Id = 2, Name = "2-Faced", ImagePath = "2faced.png" },
                    new Enemy { Id = 3, Name = "8 eye", ImagePath = "8eye.png" },
                    new Enemy { Id = 4, Name = "Acrophies", ImagePath = "acrophies.png"},                    
                });
            }

            if (await _conn.Table<Game>().CountAsync() == 0)
            {
                await _conn.InsertAllAsync(new[] 
                {
                    new Game { Id = 1, AbbrName = "FFI", FullName = "Final Fantasy I", Numeral = "1", Roman = "I", ImagePath = "FFI.png" },
                    new Game { Id = 2, AbbrName = "FFII", FullName = "Final Fantasy II", Numeral = "2", Roman = "II", ImagePath = "FFII.png" },
                    new Game { Id = 3, AbbrName = "FFIII", FullName = "Final Fantasy III", Numeral = "3", Roman = "III", ImagePath = "FFIII.png" },
                    new Game { Id = 4, AbbrName = "FFIV", FullName = "Final Fantasy IV", Numeral = "4", Roman = "IV", ImagePath = "FFIV.png" },
                    new Game { Id = 5, AbbrName = "FFV", FullName = "Final Fantasy V", Numeral = "5", Roman = "V", ImagePath = "FFV.png" },
                    new Game { Id = 6, AbbrName = "FFVI", FullName = "Final Fantasy VI", Numeral = "6", Roman = "VI", ImagePath = "FFVI.png" },
                    new Game { Id = 7, AbbrName = "FFVII", FullName = "Final Fantasy VII", Numeral = "7", Roman = "VII", ImagePath = "FFVII.png" },
                    new Game { Id = 8, AbbrName = "FFVIII", FullName = "Final Fantasy VIII", Numeral = "8", Roman = "VIII", ImagePath = "FFVIII.png" },
                    new Game { Id = 8, AbbrName = "FFIX", FullName = "Final Fantasy IX", Numeral = "9", Roman = "IX", ImagePath = "FFIX.png" },
                    new Game { Id = 8, AbbrName = "FFX", FullName = "Final Fantasy X", Numeral = "10", Roman = "X", ImagePath = "FFX.png" },
                    new Game { Id = 8, AbbrName = "FFXI", FullName = "Final Fantasy XI", Numeral = "11", Roman = "XI", ImagePath = "FFXI.png" }
                });
            }
        }

        public async Task<Enemy> GetEnemyById(int id)
        {
            return await _conn.FindAsync<Enemy>(id);
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemies()
        {
            return await _conn.Table<Enemy>().ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _conn.Table<Game>().ToListAsync();
        }

        public async Task ClearLocalDb()
        {
            await _conn.DropTableAsync<Enemy>();
            await _conn.DropTableAsync<Game>();
            await InitDb();
        }
    }
}
