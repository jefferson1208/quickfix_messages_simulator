namespace quickfix_messages_simulator_home_broker.Dtos
{
    public record TableStruct
    {
        public string Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Type { get; set; }
        public string SubTypeTag { get; set; }
        public string SubType { get; set; }
        public string OptionValues { get; set; }
        public string FieldType { get; set; }
        public string ControlType { get; set; }
    }
}
