using System.Collections.Generic;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System;

// Assembly attribute to concert the Lambda function's JSON input to a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Api
{
    public class Handlers
    {
        public APIGatewayHttpApiV2ProxyResponse ListTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received and will forward to {tableName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse CreateTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received and will forward to {tableName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse GetTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received and will forward to {tableName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse UpdateTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received and will forward to {tableName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse DeleteTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Hello, World! Your request was received and will forward to {tableName}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
