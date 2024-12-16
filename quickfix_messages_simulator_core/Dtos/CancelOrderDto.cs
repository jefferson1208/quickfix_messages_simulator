namespace quickfix_messages_simulator_core.Dtos
{
    public record CancelOrderDto
    {
        public List<FieldMessageDto> Fields { get; set; }
        public CancelOrderDto()
        {
            Fields = new List<FieldMessageDto>();
        }

        public bool Validate()
        {
            return false;
        }
    }

    
}
