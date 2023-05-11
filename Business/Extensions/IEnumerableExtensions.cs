namespace Business.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource,bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}
