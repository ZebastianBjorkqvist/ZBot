using Discord.Commands;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZBot.DbModels;
using ZBot.Services;

namespace ZBot.Modules
{
    public class LeagueRankSubscribeModule : ModuleBase
    {
        [Command("leaguerankedsub")]
        [Summary("Subscribes League summoner to the league-ranks channel.")]
        public async Task LeagueRankedSub([Remainder] [Summary("Summoner name")] string summonerName)
        {
            //using statement disposes of the database when its finished
            using (var db = new SummonerContext())
            {
                //Check if it exists
                if (db.SummonerModels.Any(s => s.SummonerName == summonerName))
                {
                    await ReplyAsync(summonerName + "is already subscribed");
                }
                else
                {
                    db.SummonerModels.Add(new SummonerModel { SummonerName = summonerName });
                    db.SaveChanges();
                    await ReplyAsync(summonerName + "is now subscribed");
                }
            }
        }

        [Command("leaguerankedunsub")]
        [Summary("Removes League summoner to the league-ranks channel.")]
        public async Task LeagueRankedUnsubscribe([Remainder] [Summary("Summoner name")] string summonerName)
        {
            using (var db = new SummonerContext())
            {
                //Check if it exists
                if (db.SummonerModels.Any(s => s.SummonerName == summonerName))
                {
                    db.SummonerModels.Remove(
                        db.SummonerModels
                            .Where(s => s.SummonerName == summonerName)
                            .FirstOrDefault());

                    db.SaveChanges();
                    await ReplyAsync(summonerName + " has been unsubscribed");
                }
                else
                {
                    await ReplyAsync(summonerName + " is not subsribed");
                }
            }
        }
    }
}
