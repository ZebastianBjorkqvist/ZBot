using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo) => ReplyAsync(echo);


    }
}

