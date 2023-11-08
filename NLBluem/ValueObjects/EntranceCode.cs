namespace NLBluem.ValueObjects
{
    public class EntranceCode
    {
        private readonly string _inner;

        public EntranceCode()
        {
            _inner = Generate();
        }

        public EntranceCode(string entranceCode)
        {
            _inner = entranceCode;
        }

        public override string ToString()
        {
            return _inner;
        }

        public static string Generate()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}
