using Newtonsoft.Json;
using Task4.Domain.Commons;
using Task4.Domain.Configurations;
using Task4.Service.Exceptions;

namespace Task4.Service.Extensions;
public static class CollectionExtensions
{
    public static IQueryable<T> ToPagedList<T>(this IQueryable<T> entities, PaginationParams @params)
            where T : Auditable
    {
        var metaData = new PaginationMetaData(entities.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            entities.OrderBy(e => e.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                    throw new TaskException(400, "Please, enter valid numbers");
    }
}
