using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Models
{
    [Table("EnemyLocations")]
    [DataContract]
    public class EnemyLocation
    {
        [DataMember(Name = "enemyId")]
        public int EnemyId { get; set; }
        [DataMember(Name = "locationId")]
        public int LocationId { get; set; }
    }
}
