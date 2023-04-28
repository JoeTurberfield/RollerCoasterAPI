RollerCoasterAPI
Author: Joe Turberfield
----------------------------------------------------------------------------------------------------------------------------
RollerCoasterAPI is a C# WebAPI Application. Set up to use MSSQL Server Database using Enity Frameowrk Core 6.

Below are steps to configure the application. (VS Code Set up)
----------------------------------------------------------------------------------------------------------------------------
Install packages

Run below cmds to install EntityFrameworkCore
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design   
dotnet add package Microsoft.EntityFrameworkCore.Tools     
----------------------------------------------------------------------------------------------------------------------------
Install plugins (Extensions)

.NET Extension pack
C# package
C# Extenstions
----------------------------------------------------------------------------------------------------------------------------
Set up connection string 

Add connection to your server (example below). This connection will be where the new database 'RollerCoasterDatabase' will be created.

Add below to - appsettings.json
"ConnectionStrings": {
    "DefaulConnection" : "Server=<SERVER_NAME>;Database=RollerCoasterDatabase;Trusted_Connection=True;TrustServerCertificate=True;User ID=<USER>;pwd=<PASSWORD>"
  }
----------------------------------------------------------------------------------------------------------------------------
CREATE DATABASE STEPS:
This will create the database on the server in connection string.

1. CREATE MIGRATION
dotnet ef migrations add InitialCreate

2. CREATE DATABASE
dotnet ef update database  
----------------------------------------------------------------------------------------------------------------------------
DROP DATABASE STEPS: 
This will remove migrations and drop database on the server.

1. REMOVE MIGRATIONS
dotnet ef migrations remove

2. DROP DATABASE
dotnet ef database drop  
----------------------------------------------------------------------------------------------------------------------------
ADD MIGRATIONS:
This will add migrations which can be used to update the database.
dotnet ef migrations add <NAME>
----------------------------------------------------------------------------------------------------------------------------
START APPLICATION

dotnet clean
dotnet build
dotnet run

For Main API page, Go to: http://<localhost:????>/swagger/index.html
----------------------------------------------------------------------------------------------------------------------------
THIS IS FOR DATABASE FIRST (NOT REWQUIRED REF ONLY)
Scaffold command - To create intial Database
dotnet ef dbcontext scaffold "Server=<SERVER_NAME>;Database=RollerCoasterDatabase;Trusted_Connection=True;TrustServerCertificate=True;User ID={USER};pwd={PASSWORD}" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
----------------------------------------------------------------------------------------------------------------------------