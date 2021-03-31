using System;

namespace ShoppingCart.Domain.Messages
{
    public class ObjectResponse<T> : HttpResponseBase where T : class
    {
        public ObjectResponse()
        {

        }

        public ObjectResponse(T item)
        {
            Item = item;
            HttpResultCode = HttpResultCode.Ok;
        }
        public T Item { get; set; }

        public void SetSuccess(T item, HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess(httpResultCode);
            Item = item;
        }
    }
}
