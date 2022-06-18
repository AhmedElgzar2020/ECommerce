
using ECommerce.Core.Enums;
using ECommerce.Core.Interfaces;
using System.Collections.Generic;

namespace ECommerce.Core.Responses
{
    public class CollectionResponse<T> : IResponse<List<T>>
    {
        public List<T> Data { get; set; }

        public ResponseStatus Status { get; set; }

        public string Message { get; set; }

        public string InternalMessage { get; set; }

        public int SubStatus { get; set; }
    }
}