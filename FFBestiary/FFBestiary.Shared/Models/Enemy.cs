using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Enemies")]
    [DataContract]
    public class Enemy
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "gameId")]
        public int GameId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "imagePath")]
        public string ImagePath { get; set; }
        [Ignore]
        public IEnumerable<IStats> Stats { get; set; }
    }
}
