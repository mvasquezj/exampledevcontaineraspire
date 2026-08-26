var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddAzureSqlServer("sql")
                       .RunAsContainer();

// 2. Define your specific database inside that container instance
var sqlDb = sqlServer.AddDatabase("my-app-db");

builder.AddProject<Projects.TodoApi>("todo-api")
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

builder.Build().Run();