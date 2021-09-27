using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;

namespace ConsoleApp6.States
{
    public class UploadFile:ConversationState
    {
        public UploadFile(Chat chat) : base(chat)
        {
        }

        protected override void InitStates()
        {
            base.InitStates();
            
            //Всегда возвращаемся к этому статусу, если не старт
            AddNextState(x => true, this);
        }

        protected override async Task Execute(ITelegramBotClient bot, Update message)
        {
            string fileName = $"files/{message.Message.Text.Trim()}";
            
            
            if (File.Exists(fileName))
            {
                await bot.SendDocumentAsync(CurrentChat.Id, new InputOnlineFile(new FileStream(fileName, FileMode.Open), fileName));
            }
            else
            {
                await bot.SendTextMessageAsync(CurrentChat.Id, "File not found. Please, try again or /start to finish");
            }
        }
    }
}