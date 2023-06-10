using System;

namespace Core.Domain.Shared.Contacts
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTime Now { get; }
    }
}
