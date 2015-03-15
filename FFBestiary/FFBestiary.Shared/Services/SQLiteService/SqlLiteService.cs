using FFBestiary.Models;
using FFBestiary.Services.FileReaderService;
using FFBestiary.Services.JSONService;
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
        private readonly IFileReaderService _fileReader;
        private readonly IJSONService _json;

        public SQLiteAsyncConnection Conn
        {
            get { return _conn; }
        }

        public SqlLiteService(IFileReaderService fileReader, IJSONService json)
        {
            _conn = new SQLiteAsyncConnection("ffbeastiary.db");
            _fileReader = fileReader;
            _json = json;
        }

        public async Task InitDb()
        {            
            var createTasks = new Task[]
            {
                _conn.CreateTableAsync<Enemy>(),
                _conn.CreateTableAsync<Game>()
            };

            Task.WaitAll(createTasks);
            await InsertDataAsync();
        }

        private async Task InsertDataAsync()
        {

            if (await _conn.Table<Enemy>().CountAsync() == 0)
            {
                var enemiesFF7JSON = await _fileReader.ReadFile("ffvii.json");
                var enemiesFF7 = _json.Deserialize<List<Enemy>>(enemiesFF7JSON);

                await _conn.InsertAllAsync(enemiesFF7);
            }

            if (await _conn.Table<Game>().CountAsync() == 0)
            {
                var gamesJSON = await _fileReader.ReadFile("games.json");
                var games = _json.Deserialize<List<Game>>(gamesJSON);

                await _conn.InsertAllAsync(games);
            }
        }

        public async Task<Enemy> GetEnemyById(int id)
        {
            return await _conn.FindAsync<Enemy>(id);
        }

        public async Task<IEnumerable<Enemy>> GetEnemiesByGameId(int gameId)
        {
            return await _conn.Table<Enemy>().Where(x => x.GameId == gameId).ToListAsync();
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemies()
        {
            return await _conn.Table<Enemy>().ToListAsync();
        }

        public async Task<Game> GetGameById(int id)
        {
            return await _conn.FindAsync<Game>(id);
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
