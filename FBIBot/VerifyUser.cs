﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBIBot.Modules.Config;

namespace FBIBot
{
    public class VerifyUser
    {
        public static async Task<bool> IsAdmin(SocketGuildUser user)
        {
            bool isValidAdmin = user == user.Guild.Owner || user == user.Guild.CurrentUser;
            foreach (SocketRole r in await AddAdminRole.GetAdminRolesAsync(user.Guild))
            {
                if (isValidAdmin)
                {
                    break;
                }
                isValidAdmin = user.Roles.Contains(r);
            }
            return await Task.Run(() => isValidAdmin);
        }

        public static async Task<bool> IsMod(SocketGuildUser user)
        {
            bool isValidMod = user == user.Guild.Owner || await IsAdmin(user);
            foreach (SocketRole r in await AddModRole.GetModRolesAsync(user.Guild))
            {
                if (isValidMod)
                {
                    break;
                }
                isValidMod = user.Roles.Contains(r);
            }
            return await Task.Run(() => isValidMod);
        }

        public static async Task<bool> InvokerIsHigher(SocketGuildUser invoker, SocketGuildUser target)
        {
            bool isHigher = target.Hierarchy < invoker.Hierarchy
                && (invoker == invoker.Guild.Owner || invoker == invoker.Guild.CurrentUser || (!await IsMod(target) && !await IsAdmin(target)));
            return await Task.Run(() => isHigher);
        }

        public static async Task<bool> BotIsHigher(SocketGuildUser bot, SocketGuildUser target)
        {
            bool isHigher = target.Hierarchy < bot.Hierarchy;
            return await Task.Run(() => isHigher);
        }
    }
}
