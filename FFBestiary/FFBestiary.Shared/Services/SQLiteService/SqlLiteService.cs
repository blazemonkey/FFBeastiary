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
                    new Enemy {Id = 1, GameId = 7, Name = "1st Ray", ImagePath = "1st Ray.png" },
                    new Enemy{ Id = 2, GameId = 7, Name = "2-Faced", ImagePath = "2-Faced.png" },
                    new Enemy { Id = 3, GameId = 7, Name = "8 Eye", ImagePath = "8 Eye.png" },
                    new Enemy { Id = 4, GameId = 7, Name = "Acrophies", ImagePath = "Acrophies.png"},  
                    new Enemy { Id =5 , GameId = 7, Name = "Adamantai", ImagePath = "Adamantai.png"}, 
                    new Enemy { Id =6 , GameId = 7, Name = "Aero Combatant", ImagePath = "Aero Combatant.png"}, 
                    new Enemy { Id =7 , GameId = 7, Name = "Allemagne", ImagePath = "Allemagne.png"}, 
                    new Enemy { Id =8 , GameId = 7, Name = "Ancient Dragon", ImagePath = "Ancient Dragon.png"}, 
                    new Enemy { Id =9 , GameId = 7, Name = "Ark Dragon", ImagePath = "Ark Dragon.png"}, 
                    new Enemy { Id =10 , GameId = 7, Name = "Armored Golem", ImagePath = "Armored Golem.png"}, 
                    new Enemy { Id =11 , GameId = 7, Name = "Attack Squad", ImagePath = "Attack Squad.png"}, 
                    new Enemy { Id =12 , GameId = 7, Name = "Bad Rap", ImagePath = "Bad Rap.png"}, 
                    new Enemy { Id =13 , GameId = 7, Name = "Bagnadrana", ImagePath = "Bagnadrana.png"}, 
                    new Enemy { Id =14 , GameId = 7, Name = "Bahba Velamyu", ImagePath = "Bahba Velamyu.png"}, 
                    new Enemy { Id =15 , GameId = 7, Name = "Bandersnatch", ImagePath = "Bandersnatch.png"}, 
                    new Enemy { Id =16 , GameId = 7, Name = "Bandit", ImagePath = "Bandit.png"}, 
                    new Enemy { Id =17 , GameId = 7, Name = "Battery Cap", ImagePath = "Battery Cap.png"}, 
                    new Enemy { Id =18 , GameId = 7, Name = "Beachplug", ImagePath = "Beachplug.png"}, 
                    new Enemy { Id =19 , GameId = 7, Name = "Behemoth", ImagePath = "Behemoth.png"}, 
                    new Enemy { Id =20 , GameId = 7, Name = "Bizarre Bug", ImagePath = "Bizarre Bug.png"}, 
                    new Enemy { Id =21 , GameId = 7, Name = "Black Bat", ImagePath = "Black Bat.png"}, 
                    new Enemy { Id =22 , GameId = 7, Name = "Bloatfloat", ImagePath = "Bloatfloat.png"}, 
                    new Enemy { Id =23 , GameId = 7, Name = "Blood Taste", ImagePath = "Blood Taste.png"}, 
                    new Enemy { Id =24 , GameId = 7, Name = "Blue Dragon", ImagePath = "Blue Dragon.png"}, 
                    new Enemy { Id =25 , GameId = 7, Name = "Blugu", ImagePath = "Blugu.png"}, 
                    new Enemy { Id =26 , GameId = 7, Name = "Bomb", ImagePath = "Bomb.png"}, 
                    new Enemy { Id =27 , GameId = 7, Name = "Boundfat", ImagePath = "Boundfat.png"}, 
                    new Enemy { Id =28 , GameId = 7, Name = "Brain Pod", ImagePath = "Brain Pod.png"}, 
                    new Enemy { Id =29 , GameId = 7, Name = "Bullmotor", ImagePath = "Bullmotor.png"}
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
