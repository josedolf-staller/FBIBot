﻿using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace FBIBot.Modules.Mod.ModLog
{
    public static class BanModLog
    {
        public static async Task SendToModLogAsync(SocketGuildUser invoker, SocketGuildUser target, double? timeout, string? reason)
            => await SendToModLogAsync(invoker, target.Id, timeout, reason);

        public static async Task SendToModLogAsync(SocketGuildUser invoker, ulong? target, double? timeout, string? reason)
        {
            await ModLogBase.SendToModLogAsync(
                new ModLogBase.ModLogInfo(
                    new ModLogBase.ModLogInfo.RequiredInfo(
                        invoker,
                        new Color(130, 0, 0),
                        $"Ban User{(timeout != null ? $" for {timeout} {(timeout == 1 ? "day" : "days")}" : "")}",
                        $"<@{target}>"
                    ),
                    new ModLogBase.ModLogInfo.ReasonInfo(
                        reason
                    )
                )
            );
        }
    }
}