using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace ConsoleApp6
{
    class Program
    {
        static async Task Main(string[] args)
        {


            var botClient = new TelegramBotClient("1979348960:AAEHwE6b3bVjCbZoeS1n4YbxtWMpgeuTJDY");
            var me = await botClient.GetMeAsync();
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            using var cts = new CancellationTokenSource();

            botClient.StartReceiving(UpdateHandler, ExceptionHandler, cancellationToken: cts.Token);

            await botClient.GetUpdatesAsync(cancellationToken: cts.Token);

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();

        }

        private static Task ExceptionHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine($"Something happend {arg2.Message}");
            return null;
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            //Console.WriteLine(update.Type);

            await Chat.GetChat(update).State.DoAction(botClient, update);



        }
    }
}
