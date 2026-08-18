# WebApiBySupriya

Simple ASP.NET Core Web API using EF Core (SQLite) and Swagger.

Run:
```bash
cd "WebApiBySupriya"
dotnet restore
dotnet run
```

Swagger UI: open the URL shown by `dotnet run` and navigate to `/swagger`.

Postman testing examples:
- GET all: `GET https://localhost:5001/api/Students`
- GET by id: `GET https://localhost:5001/api/Students/1`
- POST new: `POST https://localhost:5001/api/Students` with JSON body `{ "name":"Alice","email":"a@example.com","faculty":"CSIT","gpa":3.5 }`
- PUT update: `PUT https://localhost:5001/api/Students/1` with full student JSON including `id`.
- DELETE: `DELETE https://localhost:5001/api/Students/1`
