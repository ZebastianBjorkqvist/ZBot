﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBot.Models
{
    public class StatusModel
    {
        [JsonProperty("status_code")]
        public string Status_code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
