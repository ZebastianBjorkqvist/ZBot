using Discord;
using Discord.Commands;
using System;
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
    }
}