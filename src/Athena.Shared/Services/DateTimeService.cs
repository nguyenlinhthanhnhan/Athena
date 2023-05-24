using Athena.Shared.Common;

namespace Athena.Shared.Services;

public class DateTimeService : IDateTime
{
    public DateTime NowUtc => DateTime.UtcNow;
}