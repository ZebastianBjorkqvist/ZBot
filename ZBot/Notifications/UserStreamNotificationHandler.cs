using Discord;
using Discord.WebSocket;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZBot.Notifications
{
    public class UserStreamNotificationHandler : INotificationHandler<UserUpdatedNotification>
    {
        private readonly IDiscordClient _client;

        public UserStreamNotificationHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.OldUser.Activity is StreamingGame)
                return;

            if (notification.NewUser.Activity is StreamingGame sg)
            {
                foreach (var guild in notification.NewUser.MutualGuilds)
                {
                    //"Main" or "General" channel has the same ID as the guild
                    var channel = await _client.GetChannelAsync(guild.Id) as ITextChannel;
                    await channel.SendMessageAsync($"{notification.NewUser.Mention} is streaming! Check them out at {sg.Url}");
                }
            }
        }
    }
}
