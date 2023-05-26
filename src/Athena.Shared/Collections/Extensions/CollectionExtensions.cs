namespace Athena.Shared.Collections.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Checks whatever given collection object is null or has no item.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
    {
        return source is not { Count: > 0 };
    }
}