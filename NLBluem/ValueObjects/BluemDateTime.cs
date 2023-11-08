using NLBluem.Net.Request;
using System.Globalization;

namespace NLBluem.ValueObjects
{
    public class BluemDateTime
    {
        private DateTime _inner;

        public BluemDateTime(DateTime dateTime)
        {
            _inner = dateTime;
        }

        public static explicit operator DateTime(BluemDateTime dateTime)
        {
            return dateTime._inner;
        }

        public override string ToString()
        {
            var info = new DateTimeFormatInfo();
            var culture = new CultureInfo("en-US");

            return _inner.ToString(info.RFC1123Pattern, culture);
        }

        public long Ticks => _inner.Ticks;

        public string Iso8601Timestamp => _inner.ToString("yyyyMMddHHmmssfff");

        public DateTime ToUtc()
        {
            return _inner.ToUtc();
        }
    }
}