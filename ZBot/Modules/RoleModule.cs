using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ZBot.Services;

namespace ZBot.Modules
{
    [Group("role")]
    [Summary("Creates roles")]
    public class RoleModule : ModuleBase
    {
        [Command]
        [Summary("Gives the user a role with specified name and color")]
        public async Task Role(string roleName)
        {
            IGuild guild = Context.Guild;

            foreach(IRole r in guild.Roles)
            {
                if(r.Name == roleName)
                {
                    await (Context.User as IGuildUser).AddRoleAsync(r);
                    await ReplyAsync($"Gave {Context.User.Username} the role {r.Name} with the color {r.Color.R},{r.Color.G},{r.Color.B}");
                    return;
                }
            }

            IRole role = await guild.CreateRoleAsync(roleName);

            await role.ModifyAsync(x =>
            {
                x.Color = Color.LighterGrey;
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (Context.User as IGuildUser).AddRoleAsync(role);

            await ReplyAsync($"Gave {Context.User.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }

        [Command]
        [Summary("Gives the user a role with specified name and color with a name(white, blue...), HEX(#FFFFFF) or RGB(255,255,255)")]
        public async Task Role(string roleColor, [Remainder]string roleName)
        {
            IGuild guild = Context.Guild;

            foreach (IRole r in guild.Roles)
            {
                if (r.Name == roleName)
                {
                    await (Context.User as IGuildUser).AddRoleAsync(r);
                    await ReplyAsync($"Gave {Context.User.Username} the role {r.Name} with the color {r.Color.R},{r.Color.G},{r.Color.B}");
                    return;
                }
            }

            IRole role = await guild.CreateRoleAsync(roleName);

            await role.ModifyAsync(x =>
            {
                x.Color = DiscordColorConverterService.ColorConverter(roleColor);
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (Context.User as IGuildUser).AddRoleAsync(role);

            await ReplyAsync($"Gave {Context.User.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }

        [Command]
        [Summary("Gives the specified user a role with specified name and specifed color with a name(white, blue...), HEX(#FFFFFF) or RGB(255,255,255)")]
        public async Task Role(string roleColor, IUser userName, [Remainder]string roleName)
        {
            IGuild guild = Context.Guild;
            IUser user = userName ?? Context.User;

            foreach (IRole r in guild.Roles)
            {
                if (r.Name == roleName)
                {
                    await (Context.User as IGuildUser).AddRoleAsync(r);
                    await ReplyAsync($"Gave {Context.User.Username} the role {r.Name} with the color {r.Color.R},{r.Color.G},{r.Color.B}");
                    return;
                }
            }

            IRole role = await guild.CreateRoleAsync(roleName);

            await role.ModifyAsync(x =>
            {
                x.Color = DiscordColorConverterService.ColorConverter(roleColor);
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (user as IGuildUser).AddRoleAsync(role);
            await ReplyAsync($"Gave {user.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }
    }
}
