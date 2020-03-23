using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZBot.Modules
{
    [Group("help")]
    [Summary("Helps")]
    public class HelpModule : ModuleBase
    {
        [Summary("Helps")]
        public async Task Help()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in Program._commands.Commands)
            {
                if (command.Name != "HelpSpecific" && command.Name != "CreateAndAssignRole")
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
                if (cmd.Name.ToLower() == command.ToLower())
                {

                    var parameters = new StringBuilder();

                    foreach (ParameterInfo p in cmd.Parameters)
                    {
                        parameters.Append(p);
                    }

                    string embedFieldText = cmd.Summary ?? "No description available\n";

                    embedFieldText += Environment.NewLine;

                    if (parameters.Length < 1)
                    {
                        embedFieldText += "No parameters available\n";
                    }
                    embedFieldText += parameters.ToString();

                    embedBuilder.AddField(cmd.Name, embedFieldText);
                }
                
                
            }
            if(embedBuilder.Length < 2)
            {
                await ReplyAsync($"There is no command called {command}");
                return;
            }
            await ReplyAsync("Here's the command, it's description and parameters: ", false, embedBuilder.Build());
        }
    }
}
