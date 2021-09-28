using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp6.States;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleApp6
{
    public class Chat
    {
        private static Dictionary<long, Chat> _chats = new Dictionary<long, Chat>();
        public static Chat GetChat(Update update)
        {
           
            long id = update.Type switch
            {
                UpdateType.Message => update.Message!.Chat.Id,
                UpdateType.ChatMember =>update.ChatMember.Chat.Id,
                UpdateType.ChannelPost =>update.ChannelPost.Chat.Id,
                UpdateType.CallbackQuery =>update.CallbackQuery.Message.Chat.Id
                
            };
            
            
         
            
           // update.Message.Document
            
            var value = _chats.FirstOrDefault(x => x.Key == id).Value;
            
            if (value is null)
            {
                Chat newChat = new Chat(id);
                _chats.Add(id, newChat);
                value = newChat;
            }

            return value;
        }

        public Stack Data = new Stack();


        public StartState State { get; set; }

        public ITelegramBotClient bot;
        
        public Chat(long chatId)
        {
            this.Id = chatId;
            State = new StartState(this);
           


        }

        public ChatId Id
        {
            get; set;
        }
        
        
    }
}