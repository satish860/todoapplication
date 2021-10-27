using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.domain
{
    [DynamoDBTable("ProductCatalog")]
    public class Todo
    {
        [DynamoDBHashKey("userId")]
        public string UserId { get; set; }

        [DynamoDBRangeKey("todoId")]
        public string TodoId { get; set; }

        [DynamoDBProperty]
        public string Content { get; set; }

        [DynamoDBProperty]
        public string Attachment { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }
    }
}
