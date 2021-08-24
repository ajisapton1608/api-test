using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moduit_api.Helpers
{
    public static class Helper
    {
        public static Func<T, object> GetOrderByExpression<T>(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = (data => pinfo.GetValue(data, null));
                }
            }
            return orderByExpr;
        }

        public static List<T> OrderByDir<T>(IEnumerable<T> source, string dir, Func<T, object> OrderByColumn)
        {
            if (string.IsNullOrEmpty(dir))
                dir = "asc";
            return dir.ToUpper() == "ASC" ? source.OrderBy(OrderByColumn).ToList() : source.OrderByDescending(OrderByColumn).ToList();
        }

        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            page = page - 1;
            return en.Skip(page * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            page = page - 1;
            return en.Skip(page * pageSize).Take(pageSize);
        }
    }
}
