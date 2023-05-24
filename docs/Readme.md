## Migration

#### Before running the command, move to /src folder

### Add migration


```csharp
dotnet ef migrations add Migration-Name --project Athena.DataAccess -o Persistence/Migrations --startup-project Athena.API
```

### Update database

```csharp
dotnet ef database update --project Athena.DataAccess --startup-project Athena.API
```

### Remove migration

```csharp
dotnet ef migrations remove --project Athena.DataAccess --startup-project Athena.API
```

### Remove database

```csharp
dotnet ef database drop --project Athena.DataAccess --startup-project Athena.API
```

## Run

### Run API

```csharp
dotnet run --project Athena.API
```

### Run Tests

```csharp
dotnet test
```

## Build

### Build API

```csharp
dotnet build --project Athena.API
```
