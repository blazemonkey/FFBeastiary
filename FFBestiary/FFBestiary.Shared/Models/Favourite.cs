using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Favourites")]
    public class Favourite
    {
        [PrimaryKey]
        public int EnemyId { get; set; }
    }
}
