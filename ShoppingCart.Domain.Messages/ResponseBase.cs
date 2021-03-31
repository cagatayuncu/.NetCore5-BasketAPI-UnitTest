using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Messages
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        protected ResponseBase()
        {
            ErrorDetails = new List<string>();
        }
        public string Message { get; set; }

        public List<string> ErrorDetails { get; set; }


        public virtual void SetSuccess()
        {
            Success = true;
            Message = null;
        }

        public virtual void SetFailure(string message)
        {
            Success = false;
            Message = message;
        }

        public virtual void SetException(Exception ex)
        {
            while (ex != null)
            {
                AddError(ex.Message);
                ex = ex.InnerException;
            }
            Success = false;
        }
        public virtual void AddError(string error)
        {
            ErrorDetails.Add(error);
        }
    }
}
