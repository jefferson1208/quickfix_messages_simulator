using quickfix_messages_simulator_core.Enums;

namespace quickfix_messages_simulator_core.Dtos
{
    public record MessageDto
    {
        public string Id { get; set; }
        public QuickFix.Message Message { get; set; }
        public QuickFix.SessionID SessionID { get; set; }
        public OriginMessage Origin { get; set; }
        public Direction Direction { get; set; }

        public string GetMessage()
        {

            if (Direction == Direction.RECEIVED) return $"From {Origin} => {Message}";

            return $"To {Origin} => {Message}";
        }
    }
}
