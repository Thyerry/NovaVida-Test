# NovaVida-Test
Technical Test for the NovaVida selection process. It's a Web Crawler application that fetches products from [Kabum](https://www.kabum.com.br), stores them in DB and shows them on a frontend page. I used .NET 7.0 to build the API, using the [Html Agility Pack](https://html-agility-pack.net/) to do the web crawling, while the frontend page was made with [Angular 16](https://angular.io/).

## What do I need run the application in my machine?
* [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
  * It's preferable that you run the application using Visual Studio 2022 or JetBrains Rider.
  * This application uses sqlite. So, don't worry about database connection.
* [Angular CLI](https://angular.io/guide/setup-local)
  * Install it, open the terminal on the frontend folder and run the command ``ng serve``.

## There some specific configuration I need to do before run?
Maybe you will need to change the url that calls the backend on the [ApiService](https://github.com/Thyerry/NovaVida-Test/blob/main/Frontend/src/app/apiService.model.ts) to match the url of the backend  if, for some reason, changes on your machine.
