Run Commands from Terminal within GrpcApi Project Folder:

dotnet build
dapr run --app-id HelloDotNet6GrpcApi --app-port 4775 --app-protocol grpc --dapr-grpc-port 50001 -- dotnet run