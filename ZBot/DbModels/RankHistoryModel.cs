using System;
using System.Collections.Generic;
using System.Text;

namespace ZBot.DbModels
{
    public class RankHistoryModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Rank { get; set; }
        public SummonerModel SummonerModel { get; set; }
    }
}
