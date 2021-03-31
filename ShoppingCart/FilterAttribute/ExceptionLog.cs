using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Domain.Model.Enum;

namespace ShoppingCart.FilterAttribute
{
    public class ExceptionLog : TypeFilterAttribute
    {
        public ExceptionLog(short[] logTypes) : base(typeof(ExceptionHandling))
        {
            Arguments = new object[] { logTypes };
        }

        private class ExceptionHandling : ExceptionFilterAttribute
        {
            private readonly short[] _logTypes;
            public ExceptionHandling(short[] logTypes)
            {
                _logTypes = logTypes;
            }

            public override void OnException(ExceptionContext context)
            {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                //exception oluşan metotun namespace bilgisi 
                var methodDescriptor =
                    $"{controllerActionDescriptor.MethodInfo.ReflectedType.Namespace}.{controllerActionDescriptor.MethodInfo.ReflectedType.Name}.{controllerActionDescriptor.MethodInfo.Name}";


                Type type = context.Exception.GetType();

                


                if (_logTypes.Contains((short)LoggerType.DatabaseLogger))
                {
                    StringBuilder sb = new StringBuilder();
                    var logType = nameof(LoggerType.DatabaseLogger);
                    sb.Append($"methodDescriptor:{methodDescriptor}-LogType:{logType}-ExceptionTypeName:{type.Name}");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }

                if (_logTypes.Contains((short)LoggerType.FileLogger))
                {
                    StringBuilder sb = new StringBuilder();
                    var logType = nameof(LoggerType.FileLogger);
                    sb.Append($"methodDescriptor:{methodDescriptor}-LogType:{logType}-ExceptionTypeName:{type.Name}");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }

                if (_logTypes.Contains((short)LoggerType.EmailLogger))
                {
                    StringBuilder sb = new StringBuilder();
                    var logType = nameof(LoggerType.EmailLogger);
                    sb.Append($"methodDescriptor:{methodDescriptor}-LogType:{logType}-ExceptionTypeName:{type.Name}");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }


            }

        }
    }

}
