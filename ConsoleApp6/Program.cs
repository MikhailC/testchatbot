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
            Console.WriteLine($"Smth happend {arg2.Message}");
            return null;
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //855155074 ChatID with mc
            Console.WriteLine(update.Type);

            await Chat.GetChat(update).State.DoAction(botClient, update);
            
       

            // if (update.Type == UpdateType.Message)
            // {
            //     if (update.Message.Text == "/start")
            //     {
            //         await MessageRecieved(botClient, update.Message);
            //     }
            //
            //     if (update.Message?.Contact is not null)
            //     {
            //         if (update.Message.From != null && !App.Users.ContainsKey(update.Message.From.Id))
            //         {
            //             App.Users.Add(update.Message.From.Id, update.Message.From);
            //         }
            //     }
            //
            //
            //
            //
            //     if (App.Users.ContainsKey(update.Message.From.Id))
            //     {
            //         await botClient.SendTextMessageAsync(update.Message.Chat.Id, "I know you",
            //             cancellationToken: cancellationToken);
            //
            //         if (update.Message.Text.Equals("/add"))
            //         {
            //             App.Users[update.Message.From.Id].SetState(new );
            //         }
            //     }
            // }

            //botClient.SendTextMessageAsync(update.C)
           /* var handler = update.Type switch
            {
                // UpdateType.Unknown => expr,
                UpdateType.Message => MessageRecieved(botClient, update.Message),
                //UpdateType.InlineQuery => ,
                /* UpdateType.ChosenInlineResult => expr,
                 UpdateType.CallbackQuery => expr,
                 UpdateType.EditedMessage => expr,
                 UpdateType.ChannelPost => expr,
                 UpdateType.EditedChannelPost => expr,
                 UpdateType.ShippingQuery => expr,
                 UpdateType.PreCheckoutQuery => expr,
                 UpdateType.Poll => expr,
                 UpdateType.PollAnswer => expr,
                 UpdateType.MyChatMember => expr,
                 UpdateType.ChatMember => expr,*/
              //  _ => throw new ArgumentOutOfRangeException()
           // };*/

            // if (update.Type == UpdateType.Message)
            // {
            //     handler = MessageRecieved(botClient, update.Message);
            //     await handler;
            // }

        }

        // private static async Task<Message> MessageRecieved(ITelegramBotClient botClient, Message updateMessage)
        // {
        //     Console.WriteLine($"Receive message type: {updateMessage.Type}");
        //     Console.WriteLine($"Message {updateMessage.Text}");
        //
        //     if (updateMessage.Text != null && updateMessage.Text.Trim().Equals("/start"))
        //     {
        //         var replyKeyboard = new ReplyKeyboardMarkup(
        //             new KeyboardButton[]
        //             {
        //                 new KeyboardButton("1") {Text = "Send Send location", RequestContact = true, RequestLocation = true},
        //             }
        //         ) {ResizeKeyboard = true, OneTimeKeyboard = true};
        //
        //         return await botClient.SendTextMessageAsync(updateMessage.Chat.Id, text: "gggg",
        //             replyMarkup: replyKeyboard);
        //     }
        //     else
        //     {
        //         return await botClient.SendTextMessageAsync(updateMessage.Chat.Id, $"Recieved {updateMessage.Contact?.PhoneNumber}");
        //     }
            // var command = (updateMessage.Text.Split(' ').First()) switch
            // {
            //
            // };
            
            //return;
        }
    }
