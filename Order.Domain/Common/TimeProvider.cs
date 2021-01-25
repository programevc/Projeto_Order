using System;

namespace Order.Domain.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime utcDateTime() => DateTime.UtcNow;
    }
}
