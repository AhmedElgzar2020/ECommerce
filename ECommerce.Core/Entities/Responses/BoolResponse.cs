

using ECommerce.Core.Enums;
using ECommerce.Core.Interfaces;

namespace ECommerce.Core.Responses
{
    public class BoolResponse : IResponse<bool>
    {
        public BoolResponse()
        {
        }

        public BoolResponse(ResponseStatus status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public BoolResponse(bool data, ResponseStatus status, string message = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public bool Data { get; set; }

        public ResponseStatus Status { get; set; }

        public string Message { get; set; }

        public string InternalMessage { get; set; }

        public int SubStatus { get; set; }
    }
}