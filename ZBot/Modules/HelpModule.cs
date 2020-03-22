using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZBot.Modules
{
    [Group("help")]
    public class HelpModule : ModuleBase
    {
        [Command]
        [Summary("Helps")]
        public async Task Help()
        {


            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in Program._commands.Commands)
            {
                if (command.Name != "HelpSpecific")
                {
                    string embedFieldText = command.Summary ?? "No description available\n";

                    embedBuilder.AddField(command.Name, embedFieldText);
                }

            }

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }

        [Command]
        public async Task HelpSpecific(string command)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo cmd in Program._commands.Commands)
            {
                if (cmd.Name == command)
                {
                    var parameters = new StringBuilder();

                    foreach(ParameterInfo p in cmd.Parameters){
                        parameters.Append(p + " ");
                    }
                    string embedFieldText = cmd.Summary ?? "No description available\n";
                    try { embedFieldText += $"{Environment.NewLine} Parameters: {parameters}"; }
                    catch { embedFieldText += Environment.NewLine + "No parameters available"; };
                    embedBuilder.AddField(cmd.Name, embedFieldText);
                    }

            }

            await ReplyAsync("Here's the command, it's parameters and  description: ", false, embedBuilder.Build());
        }
    }
}
