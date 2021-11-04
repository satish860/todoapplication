import * as iam from "@aws-cdk/aws-iam"
import * as sst from "@serverless-stack/resources"

export default class AuthStack extends sst.Stack {
    auth;

    constructor(scope,id,props){
       super(scope,id,props)

       const {api,bucket} = props

       this.auth = new sst.Auth(this,"Auth",{
           cognito: {
               userPool: {
                   signInAlias: {email : true}
               },
           },
       });

       this.auth.attachPermissionsForAuthUsers([
        // Allow access to the API
        api,
        // Policy granting access to a specific folder in the bucket
        new iam.PolicyStatement({
          actions: ["s3:*"],
          effect: iam.Effect.ALLOW,
          resources: [
            bucket.bucketArn + "/private/${cognito-identity.amazonaws.com:sub}/*",
          ],
        }),
      ]);

      this.addOutputs({
          Region:scope.region,
          UserPoolId: this.auth.cognitoUserPool.userPoolId,
          IdentityPoolId: this.auth.cognitoCfnIdentityPool.ref,
         UserPoolClientId: this.auth.cognitoUserPoolClient.userPoolClientId,
      })
    }
}