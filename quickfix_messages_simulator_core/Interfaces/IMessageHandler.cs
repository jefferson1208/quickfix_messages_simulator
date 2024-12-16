namespace quickfix_messages_simulator_core.Interfaces
{
    internal interface IMessageHandler<T>
    {
        public void AddNewMessage(T message);
    }
}
