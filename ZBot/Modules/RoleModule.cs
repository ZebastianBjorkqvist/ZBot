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
        [Summary("Gives the user if specified otherwise the user invoking the command a role with specified name with a color in HEX or RGB if specified otherwise Light Grey")]
        public async Task Role(string roleName)
        {
            IGuild guild = Context.Guild;

            var role = await guild.CreateRoleAsync(roleName);

            await role.ModifyAsync(x =>
            {
                x.Color = new Color(Color.LighterGrey.RawValue);
                x.Hoist = true;
                x.Mentionable = true;
            });

            await (Context.User as IGuildUser).AddRoleAsync(role);

            await ReplyAsync($"Gave {Context.User.Username} the role {role.Name} with the color {role.Color.R},{role.Color.G},{role.Color.B}");
        }

        [Command]
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
        public async Task CreateAndAssignRole(IUser userName, string roleName, string roleColor)
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
