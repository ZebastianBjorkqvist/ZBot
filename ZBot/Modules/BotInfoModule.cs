using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ZBot.Modules
{
    public class BotInfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("source")]
        [Summary("Posts GitHub Link")]
        public async Task Source() => 
            await ReplyAsync("https://github.com/ZebastianBjorkqvist/ZBot/");

        [Command("uptime")]
        [Summary("Displays bot uptime")]
        public async Task Uptime()
        {
            DateTime startup = Process.GetCurrentProcess().StartTime;
            TimeSpan uptime = DateTime.Now - startup;

            var days = uptime.Days + " day" + (uptime.Days != 1 ? "s" : "");
            var hours = uptime.Hours + " hour" + (uptime.Hours != 1 ? "s" : "");
            var mins = uptime.Minutes + " min" + (uptime.Minutes != 1 ? "s" : "");

            await ReplyAsync($"The bot has been up for {days}, {hours} and {mins}");
        }
    }
}