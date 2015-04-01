using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Locations")]
    [DataContract]
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "gameId")]
        public int GameId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
