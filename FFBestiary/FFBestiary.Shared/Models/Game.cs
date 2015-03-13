using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Games")]
    public class Game
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }        
        public string FullName { get; set; }
        public string AbbrName { get; set; }
        public string Numeral { get; set; }
        public string Roman { get; set; }
        public string ImagePath { get; set; }
    }
}
