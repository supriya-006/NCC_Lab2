# NCC_Lab2
# WebApiBySupriya — Step-by-step Project Setup

This README shows every step to create the `WebApiBySupriya` folder and build a simple ASP.NET Core Web API using EF Core (SQLite) and Swagger.

Prerequisites
- .NET 10 SDK installed: https://dotnet.microsoft.com/
- Git (optional) for source control
- (Optional) Docker for container runs

1) Create the folder and new webapi project

```bash
mkdir "WebApiBySupriya"
cd "WebApiBySupriya"
dotnet new webapi -n WebApiBySupriya
cd WebApiBySupriya
```

2) Add required packages

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.EntityFrameworkCore.Design
```

3) Create the `Models` folder and a sample model `Student` (Models/Student.cs)

Example `Student`:

```csharp
public class Student
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Faculty { get; set; }
	public double? GPA { get; set; }
}
```

4) Create the `Data` folder and `AppDbContext` (Data/AppDbContext.cs)

Example `AppDbContext`:

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	public DbSet<Student> Students => Set<Student>();
}
```

5) Update `Program.cs` to register EF Core and Swagger

Key changes to make in `Program.cs`:

- Add `using Microsoft.EntityFrameworkCore;`
- Register the DbContext (example uses SQLite file `app.db`) and add Swagger services.

Example additions:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=app.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
```

6) Create `Controllers/StudentsController.cs` with CRUD endpoints

Example controller (minimal):

```csharp
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
	private readonly AppDbContext _db;
	public StudentsController(AppDbContext db) => _db = db;

	[HttpGet]
	public async Task<IActionResult> GetAll() => Ok(await _db.Students.ToListAsync());

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var s = await _db.Students.FindAsync(id);
		return s is null ? NotFound() : Ok(s);
	}

	[HttpPost]
	public async Task<IActionResult> Create(Student student)
	{
		_db.Students.Add(student);
		await _db.SaveChangesAsync();
		return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, Student student)
	{
		if (id != student.Id) return BadRequest();
		_db.Entry(student).State = EntityState.Modified;
		await _db.SaveChangesAsync();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var s = await _db.Students.FindAsync(id);
		if (s is null) return NotFound();
		_db.Students.Remove(s);
		await _db.SaveChangesAsync();
		return NoContent();
	}
}
```

7) Create and apply EF Core migration (optional but recommended)

Install the EF CLI (if not present) and run migration commands:

```bash
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef migrations add InitialCreate
dotnet ef database update
```

After `dotnet ef database update` a SQLite file `app.db` will be created in the project folder.

8) Run the API

```bash
dotnet restore
dotnet run
```

Open the Swagger UI (URL shown in console) and test endpoints under `/swagger`.

9) Test endpoints (curl examples)

```bash
# Get all
curl http://localhost:5000/api/Students

# Create
curl -X POST -H "Content-Type: application/json" -d '{"name":"Alice","email":"a@example.com","faculty":"CSIT","gpa":3.5}' http://localhost:5000/api/Students
```

10) (Optional) Dockerfile for containerizing

Create a `Dockerfile` with a multi-stage build (example uses .NET 10):

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["WebApiBySupriya.csproj", "./"]
RUN dotnet restore "WebApiBySupriya.csproj"
COPY . .
RUN dotnet publish "WebApiBySupriya.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet","WebApiBySupriya.dll"]
```

Build and run:

```bash
docker build -t webapibysupriya:local .
docker run --rm -p 8080:80 webapibysupriya:local
```

11) Source control (optional)

```bash
git init
git add .
git commit -m "Initial WebApiBySupriya"
```

12) Further notes and troubleshooting
- If ports differ (Kestrel uses 5000/5001 vs Docker 80) adjust curl/browser URLs accordingly.
- If migrations fail, ensure `Microsoft.EntityFrameworkCore.Design` is installed and `dotnet-ef` tool available.
- For production, do not use the default SQLite file for multi-instance deployments. Use a hosted database (Postgres/SQL Server).

If you would like, I can also:
- Generate the sample `Student` model, `AppDbContext`, and `StudentsController` files in the repository.
- Create the `Dockerfile` in this project.
- Run the app locally and capture the console output for you.

