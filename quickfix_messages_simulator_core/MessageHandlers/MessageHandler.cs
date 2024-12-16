using QuickFix;
using QuickFix.Fields;
using quickfix_messages_simulator_core.Dtos;
using quickfix_messages_simulator_core.Interfaces;

namespace quickfix_messages_simulator_core.MessageHandlers
{
    internal class MessageHandler : IApplication
    {
        private readonly IMessageHandler<MessageDto> _messageReceptor;
        private SessionID _sessionID;
        private bool _isLoggedIn;
        public MessageHandler(MessageReceptor messageReceptor)
        {
            _messageReceptor = messageReceptor;
        }
        public void FromAdmin(Message message, SessionID sessionID)
        {
            _messageReceptor.AddNewMessage(new MessageDto
            {
                Id = Guid.NewGuid().ToString(),
                Message = message,
                SessionID = sessionID,
                Origin = Enums.OriginMessage.ADMIN,
                Direction = Enums.Direction.RECEIVED
            });
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            _messageReceptor.AddNewMessage(new MessageDto
            {
                Id = Guid.NewGuid().ToString(),
                Message = message,
                SessionID = sessionID,
                Origin = Enums.OriginMessage.APP,
                Direction = Enums.Direction.RECEIVED
            });
        }

        public void OnCreate(SessionID sessionID)
        {
            _sessionID = sessionID;
            _isLoggedIn = false;
        }

        public void OnLogon(SessionID sessionID)
        {
            _sessionID = sessionID;
            _isLoggedIn = true;
        }

        public void OnLogout(SessionID sessionID)
        {
            _sessionID = null;
            _isLoggedIn = false;
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            _messageReceptor.AddNewMessage(new MessageDto
            {
                Id = Guid.NewGuid().ToString(),
                Message = message,
                SessionID = sessionID,
                Origin = Enums.OriginMessage.ADMIN,
                Direction = Enums.Direction.SENT
            });
        }

        public void ToApp(Message message, SessionID sessionId)
        {
  
        }

        public bool SendMessage(Message message)
        {
            if(!_isLoggedIn)
            {
                _messageReceptor.AddNewMessage(new MessageDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = message,
                    SessionID = _sessionID,
                    Origin = Enums.OriginMessage.APP,
                    Direction = Enums.Direction.SENT
                });

                return false;
            }

            var sent = Session.SendToTarget(message);

            if (sent)
            {
                _messageReceptor.AddNewMessage(new MessageDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = message,
                    SessionID = _sessionID,
                    Origin = Enums.OriginMessage.APP,
                    Direction = Enums.Direction.SENT
                });
            }

            return sent;
        }
    }
}
