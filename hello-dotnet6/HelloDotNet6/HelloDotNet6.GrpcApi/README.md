Run Commands from Terminal within GrpcApi Project Folder:

`dotnet build`

`docker run --name dapr-mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mariadb:latest`

`dapr run --app-id HelloDotNet6GrpcApi --app-port 4775 --app-protocol grpc --dapr-grpc-port 50001 -- dotnet run`

