using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBot.DbModels
{
    public class SummonerContext : DbContext
    {
        public DbSet<SummonerModel> SummonerModels { get; set; }
        public DbSet<RankHistoryModel> RankHistoryModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=summoners.db").EnableDetailedErrors();//Connection string should be hidden
    }
}
