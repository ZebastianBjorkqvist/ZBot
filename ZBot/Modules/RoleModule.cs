using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static ZBot.Services.ColorConverterService;

namespace ZBot.Modules
{
    [Group("role")]
    [Summary("Creates roles")]
    public class RoleModule : ModuleBase
    {
        [Command]
        [Summary("Gives the user a role with specified name and color Light Grey")]
        public async Task Role([Remainder]string roleName)
        {
            IGuild guild = Context.Guild;
            var role = await guild.CreateRoleAsync(roleName);

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
        [Summary("Gives the user a role with specified name and specifed color in HEX(FFFFFF) or RGB(255,255,255)")]
        public async Task CreateAndAssignRole(string roleName, string roleColor)
        {
            IGuild guild = Context.Guild;
            var role = await guild.CreateRoleAsync(roleName);
            int[] rgbResult = HexToRGB(roleColor);

            await role.ModifyAsync(x =>
            {
                x.Color = new Color(rgbResult[0], rgbResult[1], rgbResult[2]);
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (Context.User as IGuildUser).AddRoleAsync(role);

            await ReplyAsync($"Gave {Context.User.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }

        [Command]
        [Summary("Gives the user specified a role with specified name and specifed color in HEX(FFFFFF) or RGB(255,255,255)")]
        public async Task CreateAndAssignRole(string roleName, IUser userName,  string roleColor)
        {
            IGuild guild = Context.Guild;
            var user = userName ?? Context.User;
            var role = await guild.CreateRoleAsync(roleName);
            int[] rgbResult = HexToRGB(roleColor);

            await role.ModifyAsync(x =>
            {
                x.Color = new Color(rgbResult[0], rgbResult[1], rgbResult[2]);
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (user as IGuildUser).AddRoleAsync(role);
            await ReplyAsync($"Gave {user.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }
    }
}
