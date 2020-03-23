using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBot.Modules
{
    [Group("help")]
    [Summary("Helps")]
    public class HelpModule : ModuleBase
    {
        private readonly CommandService _service;

        public HelpModule(CommandService service)
        {
            _service = service;
        }

        [Command]
        public async Task Help()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in _service.Commands)
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
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
                              $"Summary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync("", false, builder.Build());
        }
        
        /*
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
            
            if (embedBuilder.Length < 2)
            {
                await ReplyAsync($"There is no command called {command}");
                return;
            }
            await ReplyAsync("Here's the command, it's description and parameters: ", false, embedBuilder.Build());
        }*/
    }
}
