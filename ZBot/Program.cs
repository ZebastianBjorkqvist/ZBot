﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;

namespace ZBot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddHttpClient()
                .AddScoped<RiotApiHandler>()
                .AddScoped<RiotApiRequests>()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            _client.Log += Client_Log;

            await _client.SetGameAsync("Botting | !help");
            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, ConfigurationManager.AppSettings["BotApiKey"]);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public static DiscordSocketClient _client;
        public static CommandService _commands;
        private static IServiceProvider _services;

        private static Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public static async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private static async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;
            
            var argPos = 0;
            if (message.HasCharPrefix('!', ref argPos))
            {
                IResult result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
