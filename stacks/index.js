import ApiStack from "./ApiStack";
import StorageStack from "./StorageStack";
import AuthStack from "./AuthStack"

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });

  const storageStack = new StorageStack(app,"todostorage");

 const apiStack =  new ApiStack(app, "my-stack",{
    table: storageStack.table,
  });

  new AuthStack(app, "auth", {
    api: apiStack.api,
    bucket: storageStack.bucket,
  });
  

  // Add more stacks
}
