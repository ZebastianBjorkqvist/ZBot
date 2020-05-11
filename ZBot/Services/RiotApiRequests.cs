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

        public async Task<RiotApiResponseSummoner> GetSummoner(string summonerName)
        {
            var escapedName = Uri.EscapeUriString(summonerName); //Need to escape incase it has a space in the name
            string urlSummoner = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{escapedName}"; //Url to get summoner info with summonername
            
            return await _apiHandler.ApiRequest<RiotApiResponseSummoner>(urlSummoner);
        }

        public async Task<RiotApiResponseRank[]> GetSummonerRank(string summonerName)
        {
            var summoner = await GetSummoner(summonerName);
            string urlRank = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summoner.Id}"; //Url to get ranked information of summoner. It only accepts summoner id

            return await _apiHandler.ApiRequest<RiotApiResponseRank[]>(urlRank);
        }

        public async Task<LeagueMatch> GetMatch(string summonerName)
        {
            var summoner = await GetSummoner(summonerName);
            string urlRank = $"https://euw1.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{summoner.Id}"; //Url to get match. It only accepts summoner id

            return await _apiHandler.ApiRequest<LeagueMatch>(urlRank);
        }
    }
}
