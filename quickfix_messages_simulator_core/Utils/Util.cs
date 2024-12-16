namespace quickfix_messages_simulator_core.Utils
{
    public static class Util
    {
        public static int ConvertToInt(this string value)
        {
            int x = 0;

            Int32.TryParse(value, out x);

            return x;
        }

        public static char ConvertToChar(this string value)
        {
            char ch = '\0';

            char.TryParse(value, out ch);

            return ch;
        }

        public static decimal ConvertToDecimal(this string value)
        {
            decimal x = 0;
            
            decimal.TryParse(value, out x);

            return x;
        }
    }
}
