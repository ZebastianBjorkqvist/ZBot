using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zbot.Models;
using ZBot.Modules;

namespace ZBot
{
    public class RiotApiRequests
    {
        private readonly RiotApiHandler _apiHandler;
        public RiotApiRequests(RiotApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<RiotApiResponseSummonerModel> GetSummoner(string summonerName)
        {
            var escapedName = Uri.EscapeUriString(summonerName); //Need to escape the string incase it has a space in the name
            var urlSummoner = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{escapedName}"; //Url to get summoner info with summonername
            
            return await _apiHandler.ApiRequest<RiotApiResponseSummonerModel>(urlSummoner);
        }

        public async Task<RiotApiResponseRankModel[]> GetSummonerRank(string summonerName)
        {
            RiotApiResponseSummonerModel summoner = await GetSummoner(summonerName);
            var urlRank = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summoner.Id}"; //Url to get ranked information of summoner. It only accepts summoner id

            return await _apiHandler.ApiRequest<RiotApiResponseRankModel[]>(urlRank);
        }

        public async Task<LeagueMatchModel> GetMatch(string summonerName)
        {
            RiotApiResponseSummonerModel summoner = await GetSummoner(summonerName);
            var urlRank = $"https://euw1.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{summoner.Id}"; //Url to get match. It only accepts summoner id

            return await _apiHandler.ApiRequest<LeagueMatchModel>(urlRank);
        }
    }
}
