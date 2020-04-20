using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Linq;

namespace ZBot
{
    public class DeleteMessagesModule : ModuleBase
    {
        public DeleteMessagesModule()
        {
            Program._client.MessageReceived += HandleMessage;
        }

        private async Task HandleMessage(SocketMessage arg)
        {
            int argPos = 0;
            var message = arg as SocketUserMessage;
            var channel = message.Channel;
            const string reply = ";; Is a music bot command. Please use the #music-bot channel";

            if (message.Author.IsBot) return;
            
            //Makes sure that musicbot commands are removed if they arent in music-bot channel
            if (message.HasStringPrefix(";;", ref argPos) && message.Channel.Name != "music-bot")
            {
                await message.DeleteAsync();
                var previousMessages = await channel.GetMessagesAsync(3).FlattenAsync();

                if (previousMessages.Any(x => x.Content != reply))
                {
                    await channel.SendMessageAsync(reply);
                }
            }
        }
    }
}
