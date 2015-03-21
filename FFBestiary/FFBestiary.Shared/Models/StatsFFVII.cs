using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Models
{
    [Table("StatsFF7")]
    public class StatsFFVII : IStats
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "enemyId")]
        public int EnemyId { get; set; }
        [DataMember(Name = "level")]
        public int Level { get; set; }
        [DataMember(Name = "hp")]
        public int HP { get; set; }
        [DataMember(Name = "mp")]
        public int MP { get; set; }
        [DataMember(Name = "ap")]
        public int AP { get; set; }
        [DataMember(Name = "exp")]
        public int EXP { get; set; }
        [DataMember(Name = "gil")]
        public int Gil { get; set; }
        [DataMember(Name = "attack")]
        public int Attack { get; set; }
        [DataMember(Name = "mAttack")]
        public int MAttack { get; set; }
        [DataMember(Name = "defense")]
        public int Defense { get; set; }
        [DataMember(Name = "mDefense")]
        public int MDefense { get; set; }
        [DataMember(Name = "defenseP")]
        public int DefenseP { get; set; }
        [DataMember(Name = "dexterity")]
        public int Dexterity { get; set; }
        [DataMember(Name = "luck")]
        public int Luck { get; set; }
    }
}
