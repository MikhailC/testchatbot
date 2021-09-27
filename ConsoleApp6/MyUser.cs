using ConsoleApp6.States;
using Telegram.Bot.Types;

namespace ConsoleApp6
{
    public class MyUser
    {
        public void SetState(IConversationState state)
        {
            State = state;
        }
        
        public string Phone { get; set; }
        public long Id { get; set; }
        public IConversationState State;
    }
}