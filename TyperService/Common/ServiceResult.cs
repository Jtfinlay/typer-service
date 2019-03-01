using System.Net;

namespace TyperService.Common
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string ETag { get; set; }
    }
}
