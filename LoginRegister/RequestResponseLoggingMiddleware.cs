using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace LoginRegister
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                 ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();

        }

       
        public async Task Invoke(HttpContext context)
        {

            await LogRequest(context);
            await LogResponse(context);

        }
        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            //_logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
            //                       $"Schema:{context.Request.Scheme} " +
            //                       $"Host: {context.Request.Host} " +
            //                       $"Path: {context.Request.Path} " +
            //                       $"QueryString: {context.Request.QueryString} ");

            _logger.LogInformation("istek");
            
            context.Request.Body.Position = 0;
        }

     
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            //_logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
            //                       $"Schema:{context.Request.Scheme} " +
            //                       $"Host: {context.Request.Host} " +
            //                       $"Path: {context.Request.Path} " +
            //                       $"QueryString: {context.Request.QueryString} " +
            //                       $"Response Body: {text}");

            _logger.LogInformation("cevap");

            await responseBody.CopyToAsync(originalBodyStream);
        }


    }
}

//        private async Task LogRequest(HttpContext context)
//        {
//            context.Request.EnableBuffering();

//            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
//            await context.Request.Body.CopyToAsync(requestStream);
//            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
//                                   $"Schema:{context.Request.Scheme} " +
//                                   $"Host: {context.Request.Host} " +
//                                   $"Path: {context.Request.Path} " +
//                                   $"QueryString: {context.Request.QueryString} " );
//            context.Request.Body.Position = 0;
//        }

//        private async Task LogResponse(HttpContext context)
//        {
//            var originalBodyStream = context.Response.Body;

//            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
//            context.Response.Body = responseBody;

//            await _next(context);

//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
//            context.Response.Body.Seek(0, SeekOrigin.Begin);

//            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
//                                   $"Schema:{context.Request.Scheme} " +
//                                   $"Host: {context.Request.Host} " +
//                                   $"Path: {context.Request.Path} " +
//                                   $"QueryString: {context.Request.QueryString} " +
//                                   $"Response Body: {text}");

//            await responseBody.CopyToAsync(originalBodyStream);
//        }
//    }
//}
//----------------------------------------------------------------
//var originalBodyStream = context.Response.Body;

//logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
//                      $"Schema:{context.Request.Scheme} " +
//                      $"Host: {context.Request.Host} " +
//                      $"Path: {context.Request.Path} " +
//                      $"QueryString: {context.Request.QueryString} ");



//MemoryStream requestBody = new MemoryStream();
//await context.Request.Body.CopyToAsync(requestBody);
//requestBody.Seek(0, SeekOrigin.Begin);
//string requestText = await new StreamReader(requestBody).ReadToEndAsync();
//requestBody.Seek(0, SeekOrigin.Begin);

//var tempStream = new MemoryStream();
//context.Response.Body = tempStream;


//await next.Invoke(context);

//context.Response.Body.Seek(0, SeekOrigin.Begin);
//string responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
//context.Response.Body.Seek(0, SeekOrigin.Begin);

//await context.Response.Body.CopyToAsync(originalBodyStream);

//logger.LogInformation($"Request:{requestText}");
//logger.LogInformation($"Response:{responseText}");
