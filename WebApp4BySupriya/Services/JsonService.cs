using System.Text.Json;
using WebApp4BySupriya.Models;

namespace WebApp4BySupriya.Services
{
    public class JsonService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        // 1. WITHOUT FILE: In-Memory JSON Parsing (Deserialize & Serialize)
        public (string rawJson, List<Book> books, string serializedJson) DemonstrateInMemoryJsonParsing()
        {
            string inMemoryJson = @"[
  { ""Id"": 101, ""Title"": ""Learning ASP.NET Core MVC"", ""Author"": ""Supriya Devkota"", ""Category"": ""Web Dev"", ""Price"": 35.50 },
  { ""Id"": 102, ""Title"": ""C# Programming Fundamentals"", ""Author"": ""Bipin Timalsina"", ""Category"": ""Programming"", ""Price"": 29.99 }
]";

            // Deserialize JSON string into C# objects
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(inMemoryJson, JsonOptions) ?? new List<Book>();

            // Serialize C# objects back into formatted JSON string
            string reSerializedJson = JsonSerializer.Serialize(books, JsonOptions);

            return (inMemoryJson, books, reSerializedJson);
        }

        // 2. WITH FILE: Read JSON from file
        public async Task<List<Book>> ReadBooksFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Book>();
            }

            string jsonContent = await File.ReadAllTextAsync(filePath);
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonContent, JsonOptions) ?? new List<Book>();
            return books;
        }

        // 3. WITH FILE: Write JSON to file
        public async Task<bool> AddBookAndSaveToFileAsync(string filePath, Book newBook)
        {
            List<Book> books = await ReadBooksFromFileAsync(filePath);

            // Assign auto ID
            newBook.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
            books.Add(newBook);

            // Serialize updated list to JSON string
            string updatedJson = JsonSerializer.Serialize(books, JsonOptions);

            // Write updated JSON back to file
            await File.WriteAllTextAsync(filePath, updatedJson);
            return true;
        }
    }
}
