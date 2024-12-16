using quickfix_messages_simulator_core.Dtos;
using quickfix_messages_simulator_core.Interfaces;
using System.Collections.Concurrent;

namespace quickfix_messages_simulator_interface.Setup
{
    public class MessageReceiver
    {
        private ISocket _quickfixSocket;
        private Thread _thread;

        private ConcurrentDictionary<int, MessageDto> _messageChannel;

        public delegate void CallbackChannel(MessageDto message);
        private CallbackChannel _callback;
        public MessageReceiver(ISocket quickfixSocket)
        {
            _messageChannel = new();
            _quickfixSocket = quickfixSocket;
            _thread = new(OnReceive);
            _thread.Start();
        }

        private void OnReceive()
        {
            var index = 0;
            var origin = quickfix_messages_simulator_core.Enums.OriginMessage.ADMIN;
            while (true)
            {
                var direction = quickfix_messages_simulator_core.Enums.Direction.SENT;
                
                if(origin == quickfix_messages_simulator_core.Enums.OriginMessage.ADMIN)
                {
                    origin = quickfix_messages_simulator_core.Enums.OriginMessage.APP;
                }
                else
                {
                    origin = quickfix_messages_simulator_core.Enums.OriginMessage.ADMIN;
                }

                if (index % 2 == 0)
                {
                    direction = quickfix_messages_simulator_core.Enums.Direction.RECEIVED;
                }

                Thread.Sleep(100);

                if (_callback is not null)
                {
                    
                    index++;
                    var bag = _quickfixSocket.GetMessages();

                    while (bag.TryDequeue(out var message))
                    {
                        _callback(message);
                    }
                }
            }

        }

        public void Subscribe(CallbackChannel callback) => _callback = callback;
    }
}
