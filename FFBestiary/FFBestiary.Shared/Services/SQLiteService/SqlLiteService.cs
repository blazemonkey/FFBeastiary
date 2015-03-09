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
            InitDb();
        }

        private async Task<object> InitDb()
        {
            var createTasks = new Task[]
            {
                _conn.CreateTableAsync<Enemy>()
            };

            Task.WaitAll(createTasks);
            return InsertTestDataAsync();
        }

        private async Task InsertTestDataAsync()
        {

            if (await _conn.Table<Enemy>().CountAsync() == 0)
            {
                await _conn.InsertAllAsync(new[] 
                { 
                    new Enemy {Id = 1, Name = "1st Ray" },
                    new Enemy{ Id = 2, Name = "2-Faced" },
                    new Enemy { Id = 3, Name = "8 eye" }
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

        public async Task<object> ClearLocalDb()
        {
            await _conn.DropTableAsync<Enemy>();
            return await InitDb();
        }
    }
}
