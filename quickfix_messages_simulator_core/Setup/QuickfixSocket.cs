using QuickFix;
using QuickFix.Fields;
using QuickFix.Transport;
using quickfix_messages_simulator_core.Dtos;
using quickfix_messages_simulator_core.Enums;
using quickfix_messages_simulator_core.Interfaces;
using quickfix_messages_simulator_core.MessageHandlers;
using quickfix_messages_simulator_core.Utils;
using quickfix_messages_simulator_interface.Setup;
using System.Collections.Concurrent;

namespace quickfix_messages_simulator_core.Setup
{
    public class QuickfixSocket : ISocket
    {
        private SessionSettings _setings;
        private ThreadedSocketAcceptor _acceptor;
        private SocketInitiator _initiator;

        private readonly MessageReceptor _messageReceptor;
        private readonly ConcurrentQueue<MessageDto> _messages;

        private delegate void SocketCallBack(MessageHandler handler, FileStoreFactory fileStore, FileLogFactory fileLog);

        private delegate void StopCallback();

        private StopCallback _stopCallback;
        private MessageHandler _messageHandler; 
        public QuickfixSocket()
        {
            _messages = new ConcurrentQueue<MessageDto>();
            _messageReceptor = new(_messages);
            _messageHandler = new MessageHandler(_messageReceptor);
        }
        public bool Configure()
        {
            _setings = new SessionSettings("session.cfg");

            var connectionType = _setings.Get().GetString("CONNECTIONTYPE").ToUpper();

            var storeFactory = new FileStoreFactory(_setings);
            var logFactory = new FileLogFactory(_setings);

            var socketCallback = DefSocket(connectionType);

            socketCallback(_messageHandler, storeFactory, logFactory);

            return true;
        }

        public bool SendOrder(NewOrderDto newOrder)
        {
            var message = CreateNewOrderMessage(newOrder);
            return _messageHandler.SendMessage(message);
        }

        private Message CreateNewOrderMessage(NewOrderDto newOrder)
        {
            var helper = new FixMessageFill("D", newOrder.Fields);

            helper.SetMMProtectionReset()
                .SetClOrdID()
                .SetNoPartyIDs()
                .SetAccount()
                .SetAccountType()
                .SetMinQty()
                .SetMaxFloor()
                .SetSymbol()
                .SetSecurityID()
                .SetSecurityIDSource()
                .SetSecurityExchange()
                .SetSide()
                .SetOrderQty()
                .SetOrdType()
                .SetPrice()
                .SetStopPx()
                .SetTimeInForce()
                .SetExpireDate()
                .SetPositionEffect()
                .SetMemo()
                .SetRoutingInstruction()
                .SetSelfTradePreventionInstruction()
                .SetPegPriceType()
                .SetOrdTagID()
                .SetCorrelationClordID()
                .SetScheduleDate();

            return helper.Message;
        }

   

        public bool ReplaceOrder(ReplaceOrderDto replaceOrder)
        {
            //validar e criar mensagem fix

            var message = new QuickFix.Message();
            return _messageHandler.SendMessage(message);
        }

        public bool CancelOrder(CancelOrderDto cancelOrder)
        {
            //validar e criar mensagem fix

            var message = new QuickFix.Message();
            return _messageHandler.SendMessage(message);
        }

        private SocketCallBack DefSocket(string quickfixSocketType)
        {
            return quickfixSocketType switch
            {
                "ACCEPTOR" => new SocketCallBack(ConfigureAcceptor),
                _ => new SocketCallBack(ConfigureInitiator)
            };
        }
        private void ConfigureAcceptor(MessageHandler handler, FileStoreFactory fileStore, FileLogFactory fileLog)
        {
            _acceptor = new ThreadedSocketAcceptor(
                handler,
                fileStore,
                _setings,
                fileLog);

            _stopCallback = new StopCallback(StopAcceptor);

            _acceptor.Start();
        }

        private void ConfigureInitiator(MessageHandler handler, FileStoreFactory fileStore, FileLogFactory fileLog)
        {
            _initiator = new SocketInitiator(
                handler,
                fileStore,
                _setings,
                fileLog);

            _stopCallback = new StopCallback(StopInitiator);

            _initiator.Start();
        }
        private void StopInitiator() => _initiator.Stop();
        private void StopAcceptor() => _acceptor.Stop();
        public void Stop() => _stopCallback();
        public ConcurrentQueue<MessageDto> GetMessages() => _messages;

        private IField GetField(FieldMessageDto field)
        {
            return field.Type switch
            {
                "string" => new StringField(Util.ConvertToInt(field.Tag), field.Value),
                "int" => new IntField(Util.ConvertToInt(field.Tag), Util.ConvertToInt(field.Value)),
                "bool" => new BooleanField(Util.ConvertToInt(field.Tag), field.Value.Equals("0") ? false: true),
                "Qty" => new Quantity(10),
                _ => new StringField(0, "")
            };
        }
    }
}
