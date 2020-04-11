using Discord.WebSocket;
using MediatR;

namespace ZBot.Notifications
{
    public class UserUpdatedNotification : INotification
    {
        public UserUpdatedNotification(SocketUser oldUser, SocketUser newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }

        public SocketUser OldUser { get; }
        public SocketUser NewUser { get; }
    }
}