# PROJECT CONFIGURATION #
Step by step guide to get your application up and running from scratch.

### CREATE AN EMPTY REPOSITORY ###
* Create folder named 'user-management' in your local repository
* Create an empty git repository and add a gitignore file:
```
git init
dotnet new gitignore
```
* Add project's solution file:
```
dotnet new sln --name ExchangeRate
```
* Lists all project templates
```
dotnet new --list
```
* Add new project:
```
dotnet new webapi --auth None -lang "C#" -o ExchangeRate.API
```
* Lists all projects in a solution file:
```
dotnet sln ./ExchangeRate.sln list
```
* Add class library for Service, Data, Model, Common, Interface
```
dotnet new classlib -lang "C#" -f net6.0 -o ExchangeRate.Service
dotnet new classlib -lang "C#" -f net6.0 -o ExchangeRate.Model
dotnet new classlib -lang "C#" -f net6.0 -o ExchangeRate.Interface
dotnet new classlib -lang "C#" -f net6.0 -o ExchangeRate.Infrastructure
```
* Add a projects to the solution file:
```
dotnet sln add ./ExchangeRate.API/ExchangeRate.API.csproj
dotnet sln add ./ExchangeRate.Service/ExchangeRate.Service.csproj
dotnet sln add ./ExchangeRate.Model/ExchangeRate.Model.csproj
dotnet sln add ./ExchangeRate.Interface/ExchangeRate.Interface.csproj
dotnet sln add ./ExchangeRate.Infrastructure/ExchangeRate.Infrastructure.csproj
```
* Add a project's references
```
dotnet add ./ExchangeRate.API/ExchangeRate.API.csproj reference ./ExchangeRate.Service/ExchangeRate.Service.csproj
dotnet add ./ExchangeRate.API/ExchangeRate.API.csproj reference ./ExchangeRate.Model/ExchangeRate.Model.csproj
dotnet add ./ExchangeRate.API/ExchangeRate.API.csproj reference ./ExchangeRate.Interface/ExchangeRate.Interface.csproj
dotnet add ./ExchangeRate.Service/ExchangeRate.Service.csproj reference ./ExchangeRate.Model/ExchangeRate.Model.csproj
dotnet add ./ExchangeRate.Service/ExchangeRate.Service.csproj reference ./ExchangeRate.Interface/ExchangeRate.Interface.csproj
dotnet add ./ExchangeRate.Infrastructure/ExchangeRate.Infrastructure.csproj reference ./ExchangeRate.Model/ExchangeRate.Model.csproj
dotnet add ./ExchangeRate.Infrastructure/ExchangeRate.Infrastructure.csproj reference ./ExchangeRate.Interface/ExchangeRate.Interface.csproj
dotnet add ./ExchangeRate.Service/ExchangeRate.Service.csproj reference ./ExchangeRate.Infrastructure/ExchangeRate.Infrastructure.csproj
dotnet add ./ExchangeRate.Interface/ExchangeRate.Interface.csproj reference ./ExchangeRate.Model/ExchangeRate.Model.csproj
```

### CLEAN UP AND INSTALL ExchangeRate.API PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.Extensions.Logging.Debug
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation
dotnet add package Swashbuckle.AspNetCore
```

### CLEAN UP AND INSTALL ExchangeRate.Model PROJECT PACKAGE DEPENDENCIES
```
dotnet add package AutoMapper
dotnet add package FluentValidation
```

## CLEAN UP AND INSTALL ExchangeRate.Service PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

## CLEAN UP AND INSTALL ExchangeRate.Infrastructure PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.Extensions.Http
dotnet add package Microsoft.Extensions.Http.Polly
dotnet add package Newtonsoft.Json
dotnet add package Microsoft.AspNetCore.WebUtilities
```

### RUN THE ExchangeRate.API PROJECT
```
dotnet run --launch-profile "Development"
```
