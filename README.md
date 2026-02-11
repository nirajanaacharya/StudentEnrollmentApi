StudentEnrollmentAPI system is a RESTful API built with ASP.NET Core with EF core for managing student records, secured with Identity and JWT.
prerequisites : .NetSdk : 10.0.102, SqlServer: LocalDb, Code Editor : Vscode:::
clone the repo and redirect to the dotnet folder; To clone: git clone https://github.com/your-username/StudentEnrollmentApi.git ,To redirect to project;cd StudentEnrollmentApi (in vs code terminal):::
Restore the dependencies specified in the .csproj using : dotnet restore ::
Configure the DatabseConnection--, (Start the sql Server First!) inside the appsetting.json update the connectioon string, set the server to your server name ; :::
now run :dotnet ef database update , to apply database migration :::
Now You need to specify the JWT key as i have hidden it using the usersecret locally in my computer :use this command: dotnet user-secrets init dotnet user-secrets set "Jwt:Key" "YourkeySHouldbehereandlonger", (Ensure Key is Longer):::
Run The Application using: dotnet run :::
Now Test the App in swagger ; make sure to switch to swagger ui using: http://localhost:xxx/swagger:::
Register first and then login , to get the jwt token:::
Use the token to authrize and perform the action:::
