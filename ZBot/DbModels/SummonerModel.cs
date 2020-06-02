using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBot.DbModels
{
    public class SummonerModel
    {
        public int Id { get; set; }
        public string SummonerName { get; set; }
    }
}
