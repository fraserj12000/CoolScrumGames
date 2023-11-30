# CoolScrumGames
CoolScrumGames Repository for SE2

"Games made by ETSU students, played by ETSU students."

<img src="./CoolScrumGames/wwwroot/images/diamond.png" align="right"
     alt="Size Limit logo by Anton Lovchikov" width="200" height="200">

# CoolScrumGames
CoolScrumGames Repository for SE2
Expand
README.md
3 KB
﻿
Heckplot
heckplot
Jake Eggleston
<img src="./CoolScrumGames/wwwroot/images/diamond.png" align="right"
     alt="Size Limit logo by Anton Lovchikov" width="200" height="200">

# CoolScrumGames
CoolScrumGames Repository for SE2

"Games made by ETSU students, played by ETSU students."

## Description

CoolScrumGames is a web application that allows users to play games directly embedded into the browser. 

The web application features a home page, as well as user authentication. 

## Adding a game

Games can be added into the site using Javascript.

1. Create a folder in www/root/js for the game's javascript.
2. Create a corresponding cshtml file for the game page in pages/games.
3. Add the game's javascript as the onclick href in one of the empty buttons on the index page.


## Microservice

The microservice (TestMicroservice1) is a separate ASP.NET Web Core API that runs simultaneously with the website project.
The microservice reads a text file and writes it to the About Us page. 
The microservice connects to the website project with JavaScript on the AboutUs.cshtml page.

<details>
  <summary markdown="span">Expand to view example</summary>
     
```sh
string getText() //This method is what pulls the data from the text file.
{
    string text = "";
    try
    {
        // Sets the path to the text file that you want to read
        string filePath = "AboutUs.txt";
        // Reads the content of the text file
        text = System.IO.File.ReadAllText(filePath);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
    return text;
}

app.MapGet("/AboutUs", () =>
{
    var text = new AboutUsText(getText());
    return text;
})
.WithName("GetAboutUs");

app.Run();


internal record AboutUsText(string text)
{
    string t = text;
}
```
</details>

## Docker Support

This project is designed to be run on a persistant instance, preferably a Docker container.

The project can be containerized manually, but must have the HTTP and HTTPS ports opened.

[It can also be containerized using Visual Studio's Docker Support](https://learn.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2022).
<details>
  <summary markdown="span">Example Dockerfile</summary>

```sh
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoolScrumGames/CoolScrumGames.csproj", "CoolScrumGames/"]
RUN dotnet restore "CoolScrumGames/CoolScrumGames.csproj"
COPY . .
WORKDIR "/src/CoolScrumGames"
RUN dotnet build "CoolScrumGames.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolScrumGames.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolScrumGames.dll"]
```
</details>
