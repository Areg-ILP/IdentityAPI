using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using Identity.Infastructure.Application.Utilities.Models.LogModels;

namespace Identity.API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequestAsync(context);
            await _next(context);
            await LogResponseAsync(context);
        }

        private async Task LogRequestAsync(HttpContext context)
        {
            var requestLogModel = new RequestLogModel()
            {
                Path = context.Request.Path.Value,
                Method = context.Request.Method,
                Query = context.Request.QueryString.Value,
                RequestTime = DateTime.Now.ToString(),
                Headers = GetHeaders(context, true),
                Body = await GetBodyData(context, true)
            };

            var jsonRes = JsonConvert.SerializeObject(requestLogModel, Formatting.Indented);
            _logger.LogInformation(jsonRes);
        }

        private async Task LogResponseAsync(HttpContext context)
        {
            var responseLogModel = new ResponseLogModel()
            {
                StatusCode = context.Response.StatusCode,
                Headers = GetHeaders(context, false),
                Body = await GetBodyData(context, false)
            };

            var jsonRes = JsonConvert.SerializeObject(responseLogModel, Formatting.Indented);
            _logger.LogInformation(jsonRes);
        }

        private List<string> GetHeaders(HttpContext context, bool request)
        {
            var headers = new List<string>();

            if (request)
            {
                foreach (var item in context.Request.Headers)
                {
                    headers.Add(item.Value);
                }
            }
            else
            {
                foreach (var item in context.Response.Headers)
                {
                    headers.Add(item.Value);
                }
            }

            return headers;
        }

        private async Task<string> GetBodyData(HttpContext context, bool request)
        {
            var body = string.Empty;
            if (request)
            {
                if (context.Request.Body.CanRead)
                {
                    context.Request.EnableBuffering();
                    body = await new StreamReader(context.Request.Body)
                                                        .ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
            }
            else
            {
                if (context.Response.Body.CanRead)
                {
                    body = await new StreamReader(context.Response.Body)
                                                        .ReadToEndAsync();
                    context.Response.Body.Position = 0;
                }
            }

            return body;
        }
    }
}

