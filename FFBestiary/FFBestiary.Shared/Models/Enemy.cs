using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Enemies")]
    public class Enemy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
