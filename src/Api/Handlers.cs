using System.Collections.Generic;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System;
using Api.domain;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;

// Assembly attribute to concert the Lambda function's JSON input to a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Api
{
    public class Handlers
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient(RegionEndpoint.APSouth1);
        private DynamoDBContext dynamoDBContext;

        public Handlers()
        {
            dynamoDBContext = new DynamoDBContext(client);
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> ListTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            AsyncSearch<Todo> search = dynamoDBContext.QueryAsync<Todo>("123", new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            });
            List<Todo> todo_list = new List<Todo>();
            do
            {
                var searchedTodo = await search.GetNextSetAsync();
                todo_list.AddRange(searchedTodo);
            } while (!search.IsDone);

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize<List<Todo>>(todo_list),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> CreateTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            try
            {
                Todo todo = JsonSerializer.Deserialize<Todo>(request.Body);
                todo.UserId = "123";
                todo.TodoId = Guid.NewGuid().ToString();
                var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");

                await dynamoDBContext.SaveAsync(todo, new DynamoDBOperationConfig
                {
                    OverrideTableName = tableName
                });
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = todo.TodoId,
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
            catch (Exception ex)
            {
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = ex.InnerException.Message,
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }

        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> GetTodo(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
        {
            var todoId = request.PathParameters["id"];
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            context.Logger.LogLine($"Table Name used : {tableName}");
            var todo = await dynamoDBContext.LoadAsync<Todo>("123", todoId, new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            });

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize<Todo>(todo),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> UpdateTodo(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
        {
            Todo todo = JsonSerializer.Deserialize<Todo>(request.Body);
            todo.UserId = "123";
            if (string.IsNullOrEmpty(todo.TodoId))
            {
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Todo ID is mandatory for Updating the Todo.",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            await dynamoDBContext.SaveAsync(todo, new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            });
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Todo Updated with the relevant details.",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> DeleteTodo(APIGatewayHttpApiV2ProxyRequest request)
        {
            var todoId = request.PathParameters["id"];
            var tableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            await dynamoDBContext.DeleteAsync<Todo>("123", todoId, new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            });
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Todo with {todoId} deleted Successfully",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
    }
}
