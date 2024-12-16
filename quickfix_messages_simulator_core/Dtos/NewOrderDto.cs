using QuickFix.Fields;
using quickfix_messages_simulator_core.Utils;

namespace quickfix_messages_simulator_core.Dtos
{
    public class NewOrderDto
    {
        public List<FieldMessageDto> Fields { get; set; }

        public NewOrderDto()
        {
            Fields = new();
        }


    }

     
}
