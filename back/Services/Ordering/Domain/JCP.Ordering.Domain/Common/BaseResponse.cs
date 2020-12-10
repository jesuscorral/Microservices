using System;

namespace JCP.Ordering.Domain.Common
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public Guid? Id { get; set; }
    }
}
