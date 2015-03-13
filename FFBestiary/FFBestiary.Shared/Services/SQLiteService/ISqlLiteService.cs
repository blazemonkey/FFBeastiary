using FFBestiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Services.SQLiteService
{
    public interface ISqlLiteService
    {
        SQLiteAsyncConnection Conn { get; }
        Task ClearLocalDb();

        Task<Enemy> GetEnemyById(int id);
        Task<IEnumerable<Enemy>> GetAllEnemies();

        Task<IEnumerable<Game>> GetAllGames();
    }
}
