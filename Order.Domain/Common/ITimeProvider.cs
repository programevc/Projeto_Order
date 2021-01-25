using System;

namespace Order.Domain.Common
{
    public interface ITimeProvider
    {
        DateTime utcDateTime();
    }
}
