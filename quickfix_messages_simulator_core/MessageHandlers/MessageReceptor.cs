using quickfix_messages_simulator_core.Dtos;
using quickfix_messages_simulator_core.Interfaces;
using System.Collections.Concurrent;

namespace quickfix_messages_simulator_core.MessageHandlers
{
    public sealed class MessageReceptor : IMessageHandler<MessageDto>
    {
        private readonly ConcurrentQueue<MessageDto> _messages;
        public MessageReceptor(ConcurrentQueue<MessageDto> messageQueue)
        {
            _messages = messageQueue;
        }
        public void AddNewMessage(MessageDto message) => _messages.Enqueue(message);
    }
}
