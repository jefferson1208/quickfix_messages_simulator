using QuickFix;
using QuickFix.Fields;
using quickfix_messages_simulator_core.Dtos;
using quickfix_messages_simulator_core.Utils;

namespace quickfix_messages_simulator_core.MessageHandlers
{
    public class FixMessageFill
    {
        public QuickFix.Message Message { get; private set; }
        private List<FieldMessageDto> _fields;
        public FixMessageFill(SessionID session, string msgType, List<FieldMessageDto> fields)
        {
            Message = new QuickFix.Message();

            Message.Header.SetField(new BeginString("FIX.4.4"));
            Message.Header.SetField(new MsgType(msgType));
            Message.Header.SetField(new TargetCompID(session.TargetCompID));
            Message.Header.SetField(new SenderCompID(session.SenderCompID));

            Message.SetField(new TransactTime(DateTime.Now));

            _fields = fields;
        }
        public FixMessageFill SetMMProtectionReset()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "9773");

            if (field is not null)
                Message.SetField(new BooleanField(field.Tag.ConvertToInt(), field.Value.Equals("0") ? false : true));

            return this;
        }

        public FixMessageFill SetClOrdID()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "11");

            if (field is not null)
                Message.SetField(new ClOrdID(field.Value));

            return this;
        }

        public FixMessageFill SetNoPartyIDs()
        {
            var partyID = _fields.FirstOrDefault(f => f.Tag == "448");
            var partyIDSource = _fields.FirstOrDefault(f => f.Tag == "447");
            var partyRole = _fields.FirstOrDefault(f => f.Tag == "452");

            if (partyID is null && partyIDSource is null && partyRole is null) return this;

            var noPartyIDsGroup = new QuickFix.FIX44.NewOrderSingle.NoPartyIDsGroup();

            if (partyID is not null)
                noPartyIDsGroup.PartyID = new PartyID(partyID.Value);

            if (partyIDSource is not null)
                noPartyIDsGroup.PartyIDSource = new PartyIDSource(partyIDSource.Value.ConvertToChar());

            if (partyRole is not null)
                noPartyIDsGroup.PartyRole = new PartyRole(partyRole.Value.ConvertToInt());

            Message.AddGroup(noPartyIDsGroup);

            return this;
        }

        public FixMessageFill SetAccount()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "1");

            if (field is not null)
                Message.SetField(new Account(field.Value));

            return this;
        }

        public FixMessageFill SetAccountType()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "581");

            if (field is not null)
                Message.SetField(new AccountType(field.Value.ConvertToInt()));

            return this;
        }

        public FixMessageFill SetMinQty()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "110");

            if (field is not null)
                Message.SetField(new MinQty(field.Value.ConvertToDecimal()));

            return this;
        }

        public FixMessageFill SetMaxFloor()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "111");

            if (field is not null)
                Message.SetField(new MaxFloor(field.Value.ConvertToDecimal()));

            return this;
        }

        public FixMessageFill SetSymbol()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "55");

            if (field is not null)
                Message.SetField(new Symbol(field.Value));

            return this;
        }

        public FixMessageFill SetSecurityID()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "48");

            if (field is not null)
                Message.SetField(new SecurityID(field.Value));

            return this;
        }

        public FixMessageFill SetSecurityIDSource()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "22");

            if (field is not null)
                Message.SetField(new SecurityIDSource(field.Value));

            return this;
        }

        public FixMessageFill SetSecurityExchange()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "207");

            if (field is not null)
                Message.SetField(new SecurityExchange(field.Value));

            return this;
        }

        public FixMessageFill SetSide()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "54");

            if (field is not null)
                Message.SetField(new Side(field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetOrderQty()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "38");

            if (field is not null)
                Message.SetField(new OrderQty(field.Value.ConvertToDecimal()));

            return this;
        }

        public FixMessageFill SetOrdType()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "40");

            if (field is not null)
                Message.SetField(new OrdType(field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetPrice()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "44");

            if (field is not null)
                Message.SetField(new Price(field.Value.ConvertToDecimal()));

            return this;
        }

        public FixMessageFill SetStopPx()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "99");

            if (field is not null)
                Message.SetField(new StopPx(field.Value.ConvertToDecimal()));

            return this;
        }

        public FixMessageFill SetTimeInForce()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "59");

            if (field is not null)
                Message.SetField(new TimeInForce(field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetExpireDate()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "432");

            if (field is not null)
                Message.SetField(new ExpireDate(field.Value));

            return this;
        }

        public FixMessageFill SetPositionEffect()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "77");

            if (field is not null)
                Message.SetField(new PositionEffect(field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetMemo()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "5149");

            if (field is not null)
                Message.SetField(new StringField(field.Tag.ConvertToInt(), field.Value));

            return this;
        }

        public FixMessageFill SetRoutingInstruction()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "35487");

            if (field is not null)
                Message.SetField(new CharField(field.Tag.ConvertToInt(), field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetSelfTradePreventionInstruction()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "35539");

            if (field is not null)
                Message.SetField(new IntField(field.Tag.ConvertToInt(), field.Value.ConvertToInt()));

            return this;
        }

        public FixMessageFill SetPegPriceType()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "1094");

            if (field is not null)
                Message.SetField(new PegPriceType(field.Value.ConvertToChar()));

            return this;
        }

        public FixMessageFill SetOrdTagID()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "35505");

            if (field is not null)
                Message.SetField(new IntField(field.Tag.ConvertToInt(), field.Value.ConvertToInt()));

            return this;
        }

        public FixMessageFill SetCorrelationClordID()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "9717");

            if (field is not null)
                Message.SetField(new StringField(field.Tag.ConvertToInt(), field.Value));

            return this;
        }

        public FixMessageFill SetScheduleDate()
        {
            var field = _fields.FirstOrDefault(f => f.Tag == "25502");

            if (field is not null)
                Message.SetField(new StringField(field.Tag.ConvertToInt(), field.Value));

            return this;
        }
    }
}
