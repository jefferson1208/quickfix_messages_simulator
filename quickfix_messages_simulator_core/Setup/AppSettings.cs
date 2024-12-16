using quickfix_messages_simulator_core.Enums;

namespace quickfix_messages_simulator_interface.Setup
{
    public class AppSettings
    {
        public List<MessageField> NewOrderSingleScreenFields { get; set; }
        public List<MessageField> ReplaceRequestScreenFields { get; set; }
        public List<MessageField> CancelRequestScreenFields { get; set; }
        public QuickfixSocketType QuickfixSocketType { get; set; }

    }


    public class MessageField
    {
        public string Id { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string SubTypeTag { get; set; }
        public string ControlType { get; set; }
        public string OptionValues { get; set; }
    }

    
}
