using FFBestiary.Models;
using FFBestiary.Services.FileReaderService;
using FFBestiary.Services.JSONService;
using SQLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

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
                _conn.CreateTableAsync<Game>(),
                _conn.CreateTableAsync<StatsFFVII>()
            };

            Task.WaitAll(createTasks);
            await InsertDataAsync();
        }

        private async Task InsertDataAsync()
        {

            if (await _conn.Table<Enemy>().CountAsync() == 0)
            {
                var enemiesJSON = await _fileReader.ReadFile(Package.Current.InstalledLocation, "enemies.json");
                var enemies = _json.Deserialize<List<Enemy>>(enemiesJSON);

                await _conn.InsertAllAsync(enemies);
            }

            if (await _conn.Table<Game>().CountAsync() == 0)
            {
                var gamesJSON = await _fileReader.ReadFile(Package.Current.InstalledLocation, "games.json");
                var games = _json.Deserialize<List<Game>>(gamesJSON);

                await _conn.InsertAllAsync(games);
            }

            if (await _conn.Table<StatsFFVII>().CountAsync() == 0)
            {
                var statsJSON = await _fileReader.ReadFile(Package.Current.InstalledLocation, "stats_ffvii.json");
                var stats = _json.Deserialize<List<StatsFFVII>>(statsJSON);

                await _conn.InsertAllAsync(stats);
            }

            //await DoInsertDataAsync<Enemy>("enemies.json");
        }

        private async Task DoInsertDataAsync<T>(string fileName)
        {
            var json = await _fileReader.ReadFile(Package.Current.InstalledLocation, fileName);

            var type = typeof(T);
            var sqliteType = typeof(SQLiteAsyncConnection);

            var test = sqliteType.GetTypeInfo().DeclaredMethods;
        }

        public async Task<Enemy> GetEnemyById(int id)
        {
            var enemy = await _conn.FindAsync<Enemy>(id);
            enemy.Stats = await _conn.Table<StatsFFVII>().Where(x => x.EnemyId == enemy.Id).ToListAsync();

            return enemy;
        }

        public async Task<IEnumerable<Enemy>> GetEnemiesByGameId(int gameId)
        {
            var enemies = await _conn.Table<Enemy>().Where(x => x.GameId == gameId).ToListAsync();

            foreach (var enemy in enemies)
            {
                enemy.Stats = await _conn.Table<StatsFFVII>().Where(x => x.EnemyId == enemy.Id).ToListAsync();
            }

            return enemies;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemies()
        {
            return await _conn.Table<Enemy>().ToListAsync();
        }

        public async Task UpdateEnemyImagePath(string enemyName, string path)
        {
            var enemy = await _conn.FindAsync<Enemy>(x => x.Name == enemyName);
            enemy.ImagePath = path;

            await _conn.UpdateAsync(enemy);
        }

        public async Task<Game> GetGameById(int id)
        {
            return await _conn.FindAsync<Game>(id);
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _conn.Table<Game>().ToListAsync();
        }

        public async Task UpdateGameImgurId(string name, string albumId)
        {
            var game = await _conn.FindAsync<Game>(x => x.AbbrName == name);
            game.ImgurId = albumId;

            await _conn.UpdateAsync(game);           
        }

        public async Task ClearLocalDb()
        {
            await _conn.DropTableAsync<Enemy>();
            await _conn.DropTableAsync<Game>();
            await _conn.DropTableAsync<StatsFFVII>();
            await InitDb();
        }
    }
}
