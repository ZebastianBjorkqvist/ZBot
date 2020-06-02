using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZBot.Models;

namespace Zbot.Models
{
    public class LeagueMatchModel
    {
        [JsonProperty("status")]
        public StatusModel Status { get; set; }

        [JsonProperty("gameId")]
        public long GameId { get; set; }

        [JsonProperty("mapId")]
        public long MapId { get; set; }

        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("gameType")]
        public string GameType { get; set; }

        [JsonProperty("gameQueueConfigId")]
        public long GameQueueConfigId { get; set; }

        [JsonProperty("participants")]
        public ParticipantModel[] Participants { get; set; }

        [JsonProperty("observers")]
        public ObserversModel Observers { get; set; }

        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("bannedChampions")]
        public BannedChampionModel[] BannedChampions { get; set; }

        [JsonProperty("gameStartTime")]
        public long GameStartTime { get; set; }

        [JsonProperty("gameLength")]
        public long GameLength { get; set; }
    }
}
