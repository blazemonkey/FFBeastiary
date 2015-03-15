using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Models
{
    [Table("Games")]
    [DataContract]
    public class Game
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
        [DataMember(Name = "abbrName")]
        public string AbbrName { get; set; }
        [DataMember(Name = "numeral")]
        public string Numeral { get; set; }
        [DataMember(Name = "roman")]
        public string Roman { get; set; }
        [DataMember(Name = "imagePath")]
        public string ImagePath { get; set; }
    }
}
