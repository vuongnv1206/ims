
using IMS.Api.Common.Paging;
using System.Collections.Concurrent;

using System.Reflection;
using System.Linq.Dynamic.Core;

namespace IMS.Api.Common.Helpers.Extensions;


public static class SortExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _typePropertiesMapping
        = new ConcurrentDictionary<Type, PropertyInfo[]>();

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> entities, string orderingString)
    {
        if (string.IsNullOrWhiteSpace(orderingString)
            || !entities.Any()
            || orderingString == "undefined")
        {
            return entities;
        }

        if (!_typePropertiesMapping.TryGetValue(typeof(T), out var propertyInfos))
        {
            propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            _typePropertiesMapping[typeof(T)] = propertyInfos;
        }

        var propertiesWithSortOrders = orderingString
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Select(x => (Name: x[0], SortOrder: x.Length > 1 && x[1] == "descend" ? "descending" : "ascending"))
            .Where(x => propertyInfos.Any(pi => pi.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase)))
            .Select(x => $"{x.Name} {x.SortOrder}");

        var ordering = string.Join(',', propertiesWithSortOrders);

        return entities.OrderBy(ordering);
    }

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> pagingList, PagingRequestBase paging) => pagingList.Skip(paging.Skip).Take(paging.Take);
}
