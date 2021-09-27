using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleApp6.States
{
    public class StartState:ConversationState
    {
        public StartState(Chat chat) : base(chat)
        {
           }

        protected override void InitStates()
        {
            base.InitStates();
            AddNextState(
                x => x.Message != null && x.Message.Text is not null &&x.Type == UpdateType.Message && x.Message.Text!.Equals("/add"), 
                new AddTwoNumsGetFirst(CurrentChat));
            AddNextState(
                x => x.Message != null && x.Message.Text is not null &&x.Type == UpdateType.Message && x.Message.Text!.Equals("/listfiles"), 
                new ListFiles(CurrentChat));
            AddNextState(x => x.Message.Text is  null && x.Type == UpdateType.Message && x.Message.Document is not null,
                new GetFile(CurrentChat));
        }
        

        protected override async Task Execute(ITelegramBotClient bot, Update message)
        {
            string ins = "You're using a test bot. You can use next commands:\n" +
                         "/start - this message\n" +
                         "/add - summarize many numbers\n" +
                         "/listfiles - list files in dir\n" +
                         "or simple send a file to save in local bot dir\n" +
                         "to Exit from any state send /start command in any brunch";
                         
                
            await bot.SendTextMessageAsync(CurrentChat.Id, ins);
        }
    }
}