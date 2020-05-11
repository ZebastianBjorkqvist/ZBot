using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ZBot.Modules
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
            await ReplyAsync(user.Username + Environment.NewLine + user.CreatedAt + Environment.NewLine + user.Status + Environment.NewLine + user.Activity);
        }

        [Command("cat")]
        [Summary("Posts random picture of a cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetPictureAsync("https://cataas.com/cat");
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        [Command("catgif")]
        [Summary("Posts random gif of a cat")]
        public async Task CatGifAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetPictureAsync("https://cataas.com/cat/gif");
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.gif");
        }

        [Command("test")]
        [Summary("nothing")]
        public async Task Test(IUser user = null)
        {
            user = user ?? Context.User;
            if(user.Username == "Kobey")
            {
                await ReplyAsync($"stopped  {user.Mention}");
                return;

            }
            await ReplyAsync($"no! {user.Username} {user.Mention}");
        }

        [Command("hack")]
        [Summary("nothing")]
        public async Task Hack()
        {
            await ReplyAsync("IM BEING HACKED.Hello there");
        }

        [Command("source")]
        [Summary("Posts GitHub Link")]
        public async Task Source() => await ReplyAsync("https://github.com/ZebastianBjorkqvist/ZBot/");

        [Command("uptime")]
        [Summary("Displays bot uptime")]
        public async Task Uptime()
        {
            DateTime startup = Process.GetCurrentProcess().StartTime;
            TimeSpan uptime = DateTime.Now - startup;

            var days = uptime.Days + " day" + (uptime.Days != 1 ? "s" : "") + ", ";
            var hours = uptime.Hours + " hour" + (uptime.Hours != 1 ? "s" : "") + " and ";
            var mins = uptime.Minutes + " min" + (uptime.Minutes != 1 ? "s" : "");
            
            await ReplyAsync($"The bot has been up for {days}{hours}{mins}");
        }
    }
}