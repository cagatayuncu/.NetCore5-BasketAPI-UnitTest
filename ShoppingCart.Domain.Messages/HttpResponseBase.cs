using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Messages
{
    public class HttpResponseBase : ResponseBase
    {
        public HttpResultCode HttpResultCode { get; set; }

        public void SetSuccess(HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess();
            HttpResultCode = httpResultCode;
        }

        public void SetFailure(string message, HttpResultCode httpResultCode = HttpResultCode.BadRequest)
        {
            base.SetFailure(message);
            HttpResultCode = httpResultCode;
        }
    }
}
