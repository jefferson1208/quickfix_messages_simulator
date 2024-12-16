using quickfix_messages_simulator_core.Dtos;
using System.Collections.Concurrent;

namespace quickfix_messages_simulator_core.Interfaces
{
    public interface ISocket
    {
        public bool SendOrder(NewOrderDto newOrder);
        public bool ReplaceOrder(ReplaceOrderDto replaceOrder);
        public bool CancelOrder(CancelOrderDto cancelOrder);
        public bool Configure();
        public void Stop();
        public ConcurrentQueue<MessageDto> GetMessages();
    }
}
