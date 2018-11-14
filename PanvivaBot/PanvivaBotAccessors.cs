using System;
using Microsoft.Bot.Builder;

namespace PanvivaBot
{
    public class PanvivaBotAccessors
    {
        public PanvivaBotAccessors(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }

        public ConversationState ConversationState { get; }
    }
}
