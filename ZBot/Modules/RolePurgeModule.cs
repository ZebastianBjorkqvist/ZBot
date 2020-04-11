using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBot.Modules
{
    [Group("purgeroles")]
    [Summary("Removes roles from discord server")]
    [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
    public class RolePurgeModule : ModuleBase
    {
        [Command]
        [Summary("Removes all roles that have no users")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task PurgeRole()
        {
            var result = new StringBuilder();
            result.Append("Deleted roles: ");

            foreach (SocketRole role in Context.Guild.Roles)
            {
                if(!role.Members.Any())
                {
                    result.Append(role.Name + Environment.NewLine);

                    await role.DeleteAsync();
                }
            }
            if(result.Length < 1)
            {
                await ReplyAsync("No roles to delete");
                return;
            }
            await ReplyAsync(result.ToString());
        }

        [Command]
        [Summary("Removes specified group")]
        [RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
        public async Task PurgeRole(string roleName)
        {
            var user = Context.User;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == roleName);
           
            await role.DeleteAsync();

            await ReplyAsync($"Deleted role {role.Name}");
        }
    }
}
