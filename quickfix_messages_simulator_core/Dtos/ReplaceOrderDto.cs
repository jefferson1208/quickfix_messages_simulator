namespace quickfix_messages_simulator_core.Dtos
{
    public record ReplaceOrderDto
    {
        public List<FieldMessageDto> Fields { get; set; }

        public ReplaceOrderDto()
        {
            Fields = new();
        }

        public bool Validate()
        {
            return false;
        }
    }
}
