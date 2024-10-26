
# Technical Documentation for `CSEData Worker` Project

## 1. Introduction
The `CSEData Worker` project is a .NET-based worker service designed for data collection and management tasks related to stock data. The service includes scraping functionalities, database handling, and periodic data updates. It leverages Autofac for dependency injection, Serilog for logging, and Entity Framework for database operations.

## 2. Repository Structure

### 2.1 Solution Files
- **`DemoWorker.sln`**: Visual Studio solution file containing references to all projects in the repository.
  
### 2.2 Project Directories
- **`CSEData.Worker`**: Main worker project containing the primary application code.
- **`Domain`**: Contains entity models and interfaces representing the business domain.
- **`Persistance`**: Houses the database context and configurations for data persistence.

### 2.3 Key Files and Directories
- **Configuration Files**: 
  - `CSEData.Worker/appsettings.Development.json`
  - `CSEData.Worker/appsettings.json`

- **Entity Classes** (within `Domain/Entities`):
  - `Company.cs`
  - `StockPrice.cs`

- **Database Migrations** (under `CSEData.Worker/Migrations`):
  - `20230806170213_StockMigration.cs`
  - `20230806170213_StockMigration.Designer.cs`
  - `StockDbContextModelSnapshot.cs`

- **Service and Utility Classes**:
  - `DbHandler.cs`: Handles database operations related to stock and company data.
  - `Scraper.cs`: Retrieves data from the specified URL.
  - `Tracker.cs`: Tracks process rotations, appending rotation count to a log file.

## 3. Configuration and Setup

### 3.1 AppSettings
**`CSEData.Worker/appsettings.json`**: Stores configuration settings.
- **ConnectionStrings**: Specifies the database connection for SQL Server.
- **Logging**: Configures Serilog for logging to file with daily rolling intervals.

Example configuration:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=BS-1027\\SQLEXPRESS;Database=StockDb;Trusted_Connection=True;Encrypt=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Serilog": {
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/worker-log-.log",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```

### 3.2 Dependency Injection and Modules
- **`CSEData.Worker/WorkerModule.cs`**: Configures Autofac dependencies.
- **`Persistance/PersistanceModule.cs`**: Configures `StockDbContext` for database operations with parameters for connection string and migration assembly.

## 4. Functional Components

### 4.1 Database Handler (`DbHandler.cs`)
Handles CRUD operations for `Company` and `StockPrice` data. Implements a method `DataHandler()` that iterates over scraped data and updates the database accordingly.

### 4.2 Web Scraper (`Scraper.cs`)
Scrapes stock data from a specified URL (`https://www.cse.com.bd/market/current_price`). Utilizes the HtmlAgilityPack library to parse HTML content and extract stock data for processing.

### 4.3 Rotational Tracker (`Tracker.cs`)
Keeps track of process rotations by writing rotation counts to a specified log file (`rotation.txt`), primarily for operational logging and debugging purposes.

### 4.4 Worker Service (`Worker.cs`)
Defines the core worker functionality, executing the following:
- Logs current status at each rotation.
- Calls the `Scraper` and `DbHandler` to fetch and store data.
- Delays execution for 60 seconds between rotations.

## 5. Database Entities

### 5.1 Company Entity
**File**: `Domain/Entities/Company.cs`

```csharp
public class Company : IEntity<int>
{
    public int Id { get; set; }
    public string CompantyName { get; set; }
    public List<StockPrice> Stocks { get; set; }
}
```

### 5.2 StockPrice Entity
**File**: `Domain/Entities/StockPrice.cs`

```csharp
public class StockPrice : IEntity<int>
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public DateTime Time { get; set; }
}
```

## 6. Migration and Database Context

### 6.1 Migrations
Migration files (`StockMigration.cs`, `StockMigration.Designer.cs`) create and update tables based on the `Company` and `StockPrice` entities.

### 6.2 Database Context (`StockDbContext.cs`)
Implements `DbContext` for interacting with the database. Configures Entity Framework to use SQL Server with migrations.

```csharp
public class StockDbContext : DbContext, IStockDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString, x => x.MigrationsAssembly(_migrationAssembly));
    }
}
```

## 7. Additional Files

### 7.1 Git Ignore and Attributes
- **`.gitignore`**: Specifies files and directories to exclude from source control.
- **`.gitattributes`**: Configures Git’s behavior for line endings, diff, and merge drivers.

---

## 8. Build and Deployment

### Build Process
To build the project:
```bash
dotnet build DemoWorker.sln
```

### Database Migration
To apply database migrations:
```bash
dotnet ef database update --project CSEData.Worker --context StockDbContext
```

### Running the Worker Service
The service can be run as a Windows service or directly from the command line:
```bash
dotnet run --project CSEData.Worker
```

---

## 9. Logging and Troubleshooting

Logs are generated daily in the `Logs` folder, specified in `appsettings.json`. Each operation rotation is logged, including errors encountered during scraping or database operations.
