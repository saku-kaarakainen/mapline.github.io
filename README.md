# mapline
This is just my random hobby project. 

 ## Wiki
 The documentation of this project is meant to be at Wiki. This markup file contains overview documentation and the documentation that is useful to access via text editor (VS Code) while developing.

## How to run this?
 1. Open VS Code
 2. Open it's terminal
 3. navigate `{path-to-project}/mapline/src/ClientApp`
 4. run `npm run serve`
 5. navigate to the parent folder `cd ..`
 6. run `dotnet run`

## Update
 To update database, run:
 ```
 dotnet ef migrations add Initial
 dotnet ef update database initialMigration
 ```

## Used Techonologies
 - VS Code
     - Extensions: C#, Debugger for Chrome, Docker, Vetur
 - SourceTree (maybe just VS Code plugins in the future?)
 - .NET Core 5.0.2
     - dotnet ef (cli tool)
     - dotnet add package Microsoft.EntityFrameworkCore.Sqlite
 - Vue CLI 4.0
 - NPM
 - Docker
 - [leaflet (Vue2Leaflet)](https://github.com/vue-leaflet/Vue2Leaflet)
 - (installed under ClientApp folder)
 - Sql Server 2019 Express
 - dotnet ef (cli tool) (v5.0.2)