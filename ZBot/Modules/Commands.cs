﻿using _02_commands_framework.Services;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ZBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Pings the bot")]
        public async Task Ping() => await ReplyAsync("Pong");

        [Command("say")]
        [Summary("Echoes a message")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string message) => await ReplyAsync(message);

        [Command("userinfo")]
        [Summary("Returns username and creation date")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;
            
            await ReplyAsync(user.ToString() + Environment.NewLine + user.CreatedAt);
        }

        [Command("cat")]
        [Summary("Posts random picture of a cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        [Command("test")]
        [Summary("Returns username and creation date")]
        public async Task Test(IUser user = null)
        {
            user = user ?? Context.User;
            
            
            await ReplyAsync(user.Username + Environment.NewLine + user.CreatedAt + Environment.NewLine + user.Status + Environment.NewLine + user.Activity);
        }
    }
}

