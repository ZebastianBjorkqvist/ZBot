using Discord;
using Discord.Commands;
using System.Linq;
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
        [Summary("Helps")]
        public async Task Help()
        {
            var embedBuilder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Here's a list of commands and their description"
            };

            foreach (CommandInfo command in _service.Commands)
            {
                if (command.Name != "HelpSpecific" && command.Name != "CreateAndAssignRole" && command.Name != "Hack")
                {
                    string embedFieldText = command.Summary ?? "No description available\n";
                    embedBuilder.AddField(command.Name, embedFieldText);
                }
            }
            await ReplyAsync("", false, embedBuilder.Build());
        }

        [Command]
        [Summary("Helps with specific command")]
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
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\nSummary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }
            await ReplyAsync("", false, builder.Build());
        }
    }
}
