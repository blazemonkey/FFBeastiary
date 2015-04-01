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
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "displayOrder")]
        public string DisplayOrder { get; set; }
        [DataMember(Name = "isBoss")]
        public bool IsBoss { get; set; }
        [Ignore]
        public IEnumerable<IStats> Stats { get; set; }
        [Ignore]
        public IEnumerable<int> RelatedEnemies { get; set; }
        [Ignore]
        [DataMember(Name = "locations")]
        public IEnumerable<Location> Locations { get; set; }
    }
}
