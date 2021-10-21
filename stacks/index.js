import MyStack from "./MyStack";
import StorageStack from "./StorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });

  new MyStack(app, "my-stack");

  new StorageStack(app,"todostorage");

  // Add more stacks
}
