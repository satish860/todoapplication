import { expect, haveResource } from "@aws-cdk/assert";
import * as sst from "@serverless-stack/resources";
import MyStack from "../stacks/MyStack";

test("Test Stack", () => {
  const app = new sst.App();
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  // WHEN
  const stack = new MyStack(app, "test-stack");
  // THEN
  expect(stack).to(haveResource("AWS::Lambda::Function"));
});
