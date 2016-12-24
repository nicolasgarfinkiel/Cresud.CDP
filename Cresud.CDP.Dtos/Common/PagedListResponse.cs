using System.Collections.Generic;

namespace Cresud.CDP.Dtos.Common
{
    public class PagedListResponse<T>: Response<IList<T>>
    {
        public int Count { get; set; }
    }
}
