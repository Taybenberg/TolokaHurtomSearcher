using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HurtomBotWorker
{
    public class Worker : IHostedService
    {
        private HurtomBot.HurtomBot bot;

        public Worker(string telegramBotApiToken)
        {
            bot = new HurtomBot.HurtomBot(telegramBotApiToken);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            bot.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            bot.Stop();

            return Task.CompletedTask;
        }
    }
}
