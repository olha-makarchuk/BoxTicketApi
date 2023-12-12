using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.Project.Test
{
    public class ErrorHandelingMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenExceptionThrown_ReturnsInternalServerError()
        {
            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception("Test Exception");
            });

            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream();

            await middleware.Invoke(context);
            context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            Assert.Equal("application/json", context.Response.ContentType);

            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(response);

            Assert.Equal("Test Exception", result.error.Value);
        }

        [Fact]
        public async Task Invoke_WhenNoExceptionThrown_PassesToNextMiddleware()
        {
            bool nextMiddlewareCalled = false;
            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                nextMiddlewareCalled = true;
                return Task.CompletedTask;
            });
            var context = new DefaultHttpContext();

            await middleware.Invoke(context);

            Assert.True(nextMiddlewareCalled);
        }

       
    }
}
