using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleApp6.States
{
    public abstract class ConversationState
    {
        protected readonly Chat CurrentChat = null;
        
        public ConversationState(Chat chat)
        {
            CurrentChat = chat;
            
             }

        private  Dictionary<Func<Update, bool>, ConversationState> _nextStates = null;

        protected virtual void InitStates()
        {
            _nextStates = new Dictionary<Func<Update, bool>, ConversationState>();
            
                //Default commands for all possible states
                AddNextState(
                    x => x.Message != null && 
                                     x.Message.Text != null && 
                                     x.Type ==  UpdateType.Message && 
                                     x.Message.Text.Equals("/start"),
                    new StartState(CurrentChat));
        } 

        //Добавляем возможные переходы
        protected ConversationState AddNextState(Func<Update,bool> action, ConversationState state)
        {
            _nextStates.Add(action, state);
            return this;
        }

        public ConversationState FindNextState(Update message)=>
        
        _nextStates.FirstOrDefault(stateCondition => stateCondition.Key.Invoke(message)).Value ?? this;

        public async Task<ConversationState> DoAction(ITelegramBotClient bot, Update message)
        {
            //Lazy init states
            if (_nextStates is null)
            {
                InitStates();
            }
            CurrentChat.State = FindNextState(message);
            OnBeforeExecuteNewState(CurrentChat.State, message);
            await CurrentChat.State.Execute(bot, message);
            return CurrentChat.State;
        }

        protected abstract Task Execute(ITelegramBotClient bot, Update message);

        protected virtual void OnBeforeExecuteNewState(ConversationState newState, Update message)
        {
            //Nothing doing 
        }

    }
}