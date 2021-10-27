import * as sst from "@serverless-stack/resources";

export default class ApiStack extends sst.Stack {

  api;

  constructor(scope, id, props) {
    super(scope, id, props);

    const { table } = props

    // Create a HTTP API
    this.api = new sst.Api(this, "Api", {
      defaultFunctionProps: {
        srcPath: "src/Api",
        environment: {
          TABLE_NAME : table.tableName,
        }
      },
      routes: {
        "GET /todo": "Api::Api.Handlers::ListTodo",
        "POST /todo": "Api::Api.Handlers::CreateTodo",
        "GET /todo/{id}":  "Api::Api.Handlers::GetTodo",
        "PUT /todo":  "Api::Api.Handlers::UpdateTodo",
        "Delete /todo/{id}": "Api::Api.Handlers::DeleteTodo"
      }
    });
    
    // This line attaches the permissions to DynamoDB
    this.api.attachPermissions([table])

    // Show the endpoint in the output
    this.addOutputs({
      "ApiEndpoint": this.api.url,
    });
  }
}
