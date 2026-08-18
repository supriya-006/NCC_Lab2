using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using WebApp5BySupriya.Models;

namespace WebApp5BySupriya.Data
{
    public class StudentRepository
    {
        private const string ConnectionString = "Data Source=webapp5.db";

        public StudentRepository()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var createTable = @"CREATE TABLE IF NOT EXISTS Students (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT NOT NULL,
                                    Email TEXT NOT NULL,
                                    Faculty TEXT NOT NULL,
                                    Gpa REAL NOT NULL
                                );";
            using var cmd = new SqliteCommand(createTable, connection);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Student> GetAll()
        {
            var list = new List<Student>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var query = "SELECT Id, Name, Email, Faculty, Gpa FROM Students ORDER BY Id;";
            using var cmd = new SqliteCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Faculty = reader.GetString(3),
                    Gpa = reader.GetDouble(4)
                });
            }
            return list;
        }

        public Student? GetById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var query = "SELECT Id, Name, Email, Faculty, Gpa FROM Students WHERE Id = @Id;";
            using var cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Faculty = reader.GetString(3),
                    Gpa = reader.GetDouble(4)
                };
            }
            return null;
        }

        public bool Create(Student s)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var insert = "INSERT INTO Students (Name, Email, Faculty, Gpa) VALUES (@Name, @Email, @Faculty, @Gpa);";
            using var cmd = new SqliteCommand(insert, connection);
            cmd.Parameters.AddWithValue("@Name", s.Name ?? "");
            cmd.Parameters.AddWithValue("@Email", s.Email ?? "");
            cmd.Parameters.AddWithValue("@Faculty", s.Faculty ?? "");
            cmd.Parameters.AddWithValue("@Gpa", s.Gpa);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(Student s)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var update = "UPDATE Students SET Name=@Name, Email=@Email, Faculty=@Faculty, Gpa=@Gpa WHERE Id=@Id;";
            using var cmd = new SqliteCommand(update, connection);
            cmd.Parameters.AddWithValue("@Name", s.Name ?? "");
            cmd.Parameters.AddWithValue("@Email", s.Email ?? "");
            cmd.Parameters.AddWithValue("@Faculty", s.Faculty ?? "");
            cmd.Parameters.AddWithValue("@Gpa", s.Gpa);
            cmd.Parameters.AddWithValue("@Id", s.Id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var del = "DELETE FROM Students WHERE Id=@Id;";
            using var cmd = new SqliteCommand(del, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
