# Boulder.API

### Running the Entity Framework Migrations

#### Using the Nugget Package Manager
In Visual Studio, select `Tools > Nugget Package Manager > Packet Manager Console`

In the Package Manager Console make sure the Default project is set to `BoulderPOS.API`

Run the following command : 
```pm
PM> Update-Database
```

#### Using dotnet Command Line Interface

With the project selected.
Run the following command : 
```c
> dotnet ef database update
```

## More EF Core Commands

### Adding new Migrations
With the Nugget Package Manger
```pm
PM> add-migration NewMigrationName
```
with dot net CLI :
```
> dotnet ef migrations add NewMigrationName
```

*Make sure to run the update aftwerads to alter the database*

### Removing the last migration
```
PM > Remove-Migration
```

```
> dotnet ef migrations remove
```
### Reverting to an old Migration
```
PM > Update-database MigrationName
```

```
> dotnet ef database update MigrationName.
```