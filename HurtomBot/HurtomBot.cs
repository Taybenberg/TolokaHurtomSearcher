using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;

namespace HurtomBot
{
    public class HurtomBot
    {
        private TelegramBotClient bot;

        public void Start() => bot.StartReceiving();

        public void Stop() => bot.StopReceiving();

        public HurtomBot(string TelegramApiToken)
        {
            bot = new TelegramBotClient(TelegramApiToken);

            bot.SetWebhookAsync("");

            bot.OnInlineQuery += async (object updobj, InlineQueryEventArgs iqea) =>
            {
                if (string.IsNullOrEmpty(iqea.InlineQuery.Query) || string.IsNullOrWhiteSpace(iqea.InlineQuery.Query))
                    return;

                var toloka = new TolokaHurtom.Toloka(iqea.InlineQuery.Query).ToArray();

                if (toloka.Length > 0)
                {
                    var inline = new InlineQueryResultArticle[toloka.Length];

                    for (int i = 0; i < toloka.Length; i++)
                    {
                        var content = new InputTextMessageContent($"<b>{toloka[i].title}</b>\n{toloka[i].size} | Роздають: {toloka[i].seeders} | Завантажують: {toloka[i].leechers}\n<i>{toloka[i].forum_parent} / {toloka[i].forum_parent}</i>\n{toloka[i].link}");
                        content.ParseMode = ParseMode.Html;

                        inline[i] = new InlineQueryResultArticle(
                            i.ToString(),
                            toloka[i].title,
                            content);

                        inline[i].Description =
                            toloka[i].size + " | Роздають: " + toloka[i].seeders + " | Завантажують: " + toloka[i].leechers +
                            "\n" + toloka[i].forum_name + " / " + toloka[i].forum_parent;

                        inline[i].ThumbUrl = toloka[i].link;
                    }

                    await bot.AnswerInlineQueryAsync(iqea.InlineQuery.Id, inline);
                }
            };

            bot.OnMessage += async (object updobj, MessageEventArgs mea) =>
            {
                var message = mea.Message;

                if (mea.Message.Type == MessageType.Text)
                {
                    if (message.Text == null)
                        return;

                    var ChatId = message.Chat.Id;

                    string command = message.Text.ToLower().Replace("@hurtombot", "").Replace("/", "");

                    switch (command)
                    {
                        case "start":
                            await bot.SendTextMessageAsync(ChatId, "Вітаю! Я @HurtomBot!\nНапишіть, що ви хочете знайти на Гуртом, і я знайду для вас українські торренти.\nНатисніть '/', щоби обрати команду.");
                            break;

                        case "sendtorrent":
                            await bot.SendTextMessageAsync(ChatId, "Натисніть кнопку та оберіть чат до якого хочете надіслати торрент.", replyMarkup: new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithSwitchInlineQuery("Надіслати") }));
                            break;

                        default:
                            foreach (var torrent in new TolokaHurtom.Toloka(command).ToArray())
                                bot.SendTextMessageAsync(ChatId, $"<b>{torrent.title}</b>\n{torrent.size} | Роздають: {torrent.seeders} | Завантажують: {torrent.leechers}\n<i>{torrent.forum_parent} / {torrent.forum_name}</i>\n{torrent.link}", ParseMode.Html);
                            break;
                    }
                }
            };
        }
    }
}