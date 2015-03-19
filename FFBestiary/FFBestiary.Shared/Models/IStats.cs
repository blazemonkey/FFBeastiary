using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Models
{
    public interface IStats
    {
        int Id { get; set; }
        int EnemyId { get; set; }
    }
}
