using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ConsoleApp6.States
{
    public class ListFiles:ConversationState
    {
        public ListFiles(Chat chat) : base(chat)
        {
        }

        protected override void InitStates()
        {
            base.InitStates();
            AddNextState(x => File.Exists($"files/{x.Message.Text.Trim()}"), new UploadFile(CurrentChat));
        }

        protected override async Task Execute(ITelegramBotClient bot, Update message)
        {
            var sw = new StringWriter();
            
            await sw.WriteLineAsync("Files in directory:");
            //await bot.SendTextMessageAsync(CurrentChat.Id, "directory files:");
            foreach (var file in Directory.GetFiles("files"))
            {
                var fi = new FileInfo(file);
                await sw.WriteLineAsync($"{fi.Name} Size: {fi.Length}");
            
            }

            await sw.WriteLineAsync("Enter file name to get the file to chat or /start to cancel");

            await bot.SendTextMessageAsync(CurrentChat.Id, sw.ToString());
            
        }
    }
}