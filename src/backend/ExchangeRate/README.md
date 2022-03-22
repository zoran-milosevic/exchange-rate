
### Exchange Rate API REST Service

Exchange Rate is a simple REST service that returns historical exchange rate information.

The service will use a free API provided by the ECB as its data source. This API is documented at https://exchangerate.host/

#### How to run the API

- Clone the repo
- Install prerequisites: .NET 6.0 LTS https://dotnet.microsoft.com/en-us/
- Download Visual Studio Code from https://code.visualstudio.com, simply open a project folder from VSCode /exchange-rate/src/backend/ExchangeRate and run an API by hitting F5 on keyboard to start an API in debugging mode. Browser will appear and Swagger has being loaded automatically (RECOMMENDED).
- Go to the folder /exchange-rate/src/backend/ExchangeRate/ExchangeRate.API
- Restore packages
```
dotnet restore
```
If you're running the API from console, set environment on Windows operating system:
```
setx ASPNETCORE_ENVIRONMENT "Development"
```
Or do it like this, if you are on Linux:
```
export ASPNETCORE_ENVIRONMENT=Development
```
Run ExchangeRate.API project:
```
dotnet run --project "ExchangeRate.API.csproj" --urls="http://localhost:7151"
```

To facilitate testesting or use the only one endpoint implemented in the service, Swagger is included in this API. The Swagger page lists the endpoint along with input controls so a user can provide parameter values, call the endpoint, and view its response. 

The endpoint address of API is https://localhost:7151/api/exchangeratesearch



- Request body example:
```
{
  "baseCurrency": "EUR",
  "targetCurrencies": [
    "USD","GBP"
  ],
  "dates": [
    "2022-03-21",
    "2022-03-20",
    "2022-03-19",
    "2022-03-18",
    "2022-03-17",
    "2022-03-16",
    "2022-03-15",
    "2022-03-14",
    "2022-03-13",
    "2022-03-12",
    "2022-03-11",
    "2022-03-10",
    "2022-03-09",
    "2022-03-08",
    "2022-03-07",
    "2022-03-06",
    "2022-03-05",
    "2022-03-04",
    "2022-03-03",
    "2022-03-02",
    "2022-03-01",
    "2022-02-28",
    "2022-02-27",
    "2022-02-26",
    "2022-02-25",
    "2022-02-24",
    "2022-02-23",
    "2022-02-22",
    "2022-02-21",
    "2022-02-20",
    "2022-02-19",
    "2022-02-18",
    "2022-02-17",
    "2022-02-16",
    "2022-02-15",
    "2022-02-14",
    "2022-02-13",
    "2022-02-12",
    "2022-02-11",
    "2022-02-10",
    "2022-02-09",
    "2022-02-08",
    "2022-02-07",
    "2022-02-06",
    "2022-02-05",
    "2022-02-04",
    "2022-02-03",
    "2022-02-02",
    "2022-02-01"
  ]
}
```