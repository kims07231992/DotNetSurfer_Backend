# .NETSurfer Backend

.NETSurfer Backend is an API using ASP.NET Core 3.0 to support [.NETSurfer Frontend](https://github.com/kims07231992/DotNetSurfer_Frontend) API requests

### Overview

![Alt text](https://github.com/kims07231992/DotNetSurfer_Backend/blob/master/README_Picture1.PNG)

### Swagger Page

![Alt text](https://github.com/kims07231992/DotNetSurfer_Backend/blob/master/README_Picture2.PNG)

## Features

* API based resource assess by ASP.NET Core framework
* Simple database maintenance by including database project
* Role based authentication
* Swagger
* Unit Testing
* Consistent HttpResponse handling
* Exception handling with user-define exception
* Image resource management with CDN
* Running on linux container using Docker
* Caching strategy to utilize database access



## Stacks
* ASP.NET Core 3.0
* Entity Framework Core
* NLog
* JWT Authentication
* Swagger
* xUnit
* Docker
* Azure Blob
* Azure Depolyment

## Installation

### Database

.NETSurfer requires MSSQL to run.
```
cd DotNetSurfer_Backend\database\DotNetSurfer_Backend.Database
MSBuild /t:Build;Deploy DotNetSurfer_Backend.Database.sqlproj
```


### Configuration

Connect your server to MSSQL and CDN by setting proper values in [appsettings.json](https://github.com/kims07231992/DotNetSurfer_Backend/blob/master/DotNetSurfer_Backend/src/Worker/DotNetSurfer_Backend.API/appsettings.Development.json)


### Run
```
cd DotNetSurfer_Backend
dotnet build
dotnet run

https://localhost:44300/
```

## License

MIT
