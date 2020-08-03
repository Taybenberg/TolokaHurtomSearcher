using System;
using System.IO;
using System.Text.RegularExpressions;
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

        public Worker()
        {
            /*  
             *  Хостинг AppHarbor записує конфігураційні змінні до файлу .config, 
             *  який більше не використовується в .Net Core
             *  Через це доводиться діставати токен за допомогою Regex-виразу
             */

            var regex = new Regex("\"TelegramBotApiToken\" value=\"(.+)\"");

            var match = regex.Match(File.ReadAllText("HurtomBotWorker.dll.config"));

            bot = new HurtomBot.HurtomBot(match.Groups[1].Value);
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
