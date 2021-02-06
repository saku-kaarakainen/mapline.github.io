# mapline
This is just my random hobby project. 

 ## Wiki
 The documentation of this project is meant to be at Wiki. This markup file contains overview documentation and the documentation that is useful to access via text editor (VS Code) while developing.

## How to run this?
 1. Open VS Code
 2. Run .NET Core Launch (web)
 3. Open `http://localhost:8080` in the browser of your choice 

## DB Update
### Initialize
To initialize database, run:
```
cd {path-to-project}/src
dotnet ef migrations add InitialMigration -o ../data/CodeFirstMigrations/
dotnet ef migrations script -o ../data/SQL/initial.sql
```
_(Then run update)_
### Update
To update database, run:
```
dotnet ef database update
```

### Reset
```
dotnet ef database drop
dotnet ef migrations remove
```

## Used Techonologies
 - VS Code
     - Extensions: C#, Debugger for Chrome, Docker, Vetur
 - Visual Studio 2019 Community (planned) - due to easy of use.
 - SourceTree (maybe just VS Code plugins in the future?)
 - .NET Core 5.0.2
     - dotnet ef (cli tool)
     - dotnet add package Microsoft.EntityFrameworkCore.Sqlite
 - NuGet
     - NetTopologyASuite 2.1.0
     - Microsoft.EntityFrameworkCore.SqlServer.NetTopologyASuite (5.0.2)
 - Vue CLI 4.0
 - NPM
 - Docker
 - [leaflet (Vue2Leaflet)](https://github.com/vue-leaflet/Vue2Leaflet)
 - (installed under ClientApp folder)
 - Sql Server 2019 Express