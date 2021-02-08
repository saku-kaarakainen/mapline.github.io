# mapline
This is just my random hobby project. 

 ## Wiki
 The documentation of this project is meant to be at Wiki. This markup file contains overview documentation and the documentation that is useful to access via text editor (VS Code) while developing.

## How to run this?
## Visual studio
1. Open the solution file `{path-to-project/src/Mapline.sln}`
2. restore NPM packages `Tools -> NuGet Package Manager -> Package Manager Console`
```batch
# The script below should add packages to the path: {path-to-project}/src/Mapline.Web/ClientApp/node_modules
cd Mapline.web/ClientApp # Make sure you are in this folder. You can also use --prefix
npm install
npm install leaflet vue2-leaflet --save

# You also need to copy a css file from it's packages. I don't know why my project is doing that, I have been lazy to figure out the reason.
mkdir .\public\lib # this is already in .gitignore
copy .\node_modules\leaflet\dist\leaflet.css .\public\lib\.
```
3. Restore NuGet Packages
4. Run

## VS Code (obsolete)
 1. Open VS Code
 2. Run .NET Core Launch (web)
 3. Open `http://localhost:8080` in the browser of your choice 

## Migrations
Migrations are maintained in the Mapline.Migrations - project.

## Used Techonologies
 - VS Code
     - Extensions: C#, Debugger for Chrome, Docker, Vetur
 - Visual Studio 2019 Community (planned) - due to easy of use.
 - SourceTree (maybe just VS Code plugins in the future?)
 - .NET Core 5.0.2
     - `dotnet tool install --global dotnet-ef` (cli tool)
     - `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
 - NuGet
     - NetTopologyASuite 2.1.0
     - Microsoft.EntityFrameworkCore.SqlServer.NetTopologyASuite (5.0.2)
 - Vue CLI 4.0
 - NPM
 - Docker
 - [leaflet (Vue2Leaflet)](https://github.com/vue-leaflet/Vue2Leaflet)
 - (installed under ClientApp folder)
 - Sql Server 2019 Express