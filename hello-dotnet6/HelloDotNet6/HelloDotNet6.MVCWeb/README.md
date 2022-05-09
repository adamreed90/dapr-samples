Run Commands from Terminal within MVCWeb Project Folder after running HelloDotNet6.GrpcApi service first:

`dotnet build`

`dapr run --app-id HelloDotNet6MVCWeb --app-port 4776 -- dotnet run`

Open `http://localhost:4776/` and `http://localhost:4776/Home/Inventory`

