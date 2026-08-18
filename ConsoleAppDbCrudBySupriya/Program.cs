using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace ConsoleAppDbCrudBySupriya
{
    internal class Program
    {
        private const string ConnectionString = "Data Source=student_db.db";

        static void Main(string[] args)
        {
            InitializeDatabase();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   STUDENT DATABASE CRUD OPERATIONS (ADO.NET)    ");
                Console.WriteLine("   Developed by: Supriya                          ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Add New Student (Insert)");
                Console.WriteLine("2. View All Students (Read)");
                Console.WriteLine("3. View Student by ID (Read)");
                Console.WriteLine("4. Update Student Details (Update)");
                Console.WriteLine("5. Delete Student Record (Delete)");
                Console.WriteLine("6. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Enter your choice (1-6): ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        InsertStudent();
                        break;
                    case "2":
                        ReadAllStudents();
                        break;
                    case "3":
                        ReadStudentById();
                        break;
                    case "4":
                        UpdateStudent();
                        break;
                    case "5":
                        DeleteStudent();
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }

                if (!exit && choice != "6")
                {
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }

        private static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Students (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Faculty TEXT NOT NULL,
                        Gpa REAL NOT NULL
                    );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void InsertStudent()
        {
            Console.WriteLine("--- INSERT NEW STUDENT ---");
            Console.Write("Enter Full Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Enter Faculty (e.g. BIM, CSIT, BCA): ");
            string faculty = Console.ReadLine() ?? "";

            Console.Write("Enter GPA (e.g. 3.75): ");
            if (!double.TryParse(Console.ReadLine(), out double gpa))
            {
                gpa = 0.0;
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO Students (Name, Email, Faculty, Gpa)
                    VALUES (@Name, @Email, @Faculty, @Gpa);";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Faculty", faculty);
                    command.Parameters.AddWithValue("@Gpa", gpa);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("SUCCESS: Student record inserted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Failed to insert student record.");
                    }
                }
            }
        }

        private static void ReadAllStudents()
        {
            Console.WriteLine("--- ALL STUDENT RECORDS ---");

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Id, Name, Email, Faculty, Gpa FROM Students ORDER BY Id;";

                using (var command = new SqliteCommand(selectQuery, connection))
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No records found in database.");
                        return;
                    }

                    Console.WriteLine("{0,-5} | {1,-20} | {2,-25} | {3,-10} | {4,-5}", "ID", "Name", "Email", "Faculty", "GPA");
                    Console.WriteLine(new string('-', 75));

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string email = reader.GetString(2);
                        string faculty = reader.GetString(3);
                        double gpa = reader.GetDouble(4);

                        Console.WriteLine("{0,-5} | {1,-20} | {2,-25} | {3,-10} | {4,-5:F2}", id, name, email, faculty, gpa);
                    }
                }
            }
        }

        private static void ReadStudentById()
        {
            Console.WriteLine("--- SEARCH STUDENT BY ID ---");
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Id, Name, Email, Faculty, Gpa FROM Students WHERE Id = @Id;";

                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("\nRecord Found:");
                            Console.WriteLine($"ID:      {reader.GetInt32(0)}");
                            Console.WriteLine($"Name:    {reader.GetString(1)}");
                            Console.WriteLine($"Email:   {reader.GetString(2)}");
                            Console.WriteLine($"Faculty: {reader.GetString(3)}");
                            Console.WriteLine($"GPA:     {reader.GetDouble(4):F2}");
                        }
                        else
                        {
                            Console.WriteLine($"Student with ID {id} was not found.");
                        }
                    }
                }
            }
        }

        private static void UpdateStudent()
        {
            Console.WriteLine("--- UPDATE STUDENT DETAILS ---");
            Console.Write("Enter ID of student to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            // Check if record exists
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string checkQuery = "SELECT Id, Name, Email, Faculty, Gpa FROM Students WHERE Id = @Id;";

                string currentName = "", currentEmail = "", currentFaculty = "";
                double currentGpa = 0.0;
                bool exists = false;

                using (var checkCommand = new SqliteCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Id", id);
                    using (var reader = checkCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            exists = true;
                            currentName = reader.GetString(1);
                            currentEmail = reader.GetString(2);
                            currentFaculty = reader.GetString(3);
                            currentGpa = reader.GetDouble(4);
                        }
                    }
                }

                if (!exists)
                {
                    Console.WriteLine($"Student with ID {id} not found.");
                    return;
                }

                Console.WriteLine($"Current Details: Name: {currentName}, Email: {currentEmail}, Faculty: {currentFaculty}, GPA: {currentGpa:F2}");
                Console.WriteLine("Leave blank and press Enter to keep current value.\n");

                Console.Write($"New Name [{currentName}]: ");
                string name = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(name)) name = currentName;

                Console.Write($"New Email [{currentEmail}]: ");
                string email = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(email)) email = currentEmail;

                Console.Write($"New Faculty [{currentFaculty}]: ");
                string faculty = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(faculty)) faculty = currentFaculty;

                Console.Write($"New GPA [{currentGpa:F2}]: ");
                string gpaInput = Console.ReadLine() ?? "";
                double gpa = double.TryParse(gpaInput, out double newGpa) ? newGpa : currentGpa;

                string updateQuery = @"
                    UPDATE Students 
                    SET Name = @Name, Email = @Email, Faculty = @Faculty, Gpa = @Gpa 
                    WHERE Id = @Id;";

                using (var updateCommand = new SqliteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", name);
                    updateCommand.Parameters.AddWithValue("@Email", email);
                    updateCommand.Parameters.AddWithValue("@Faculty", faculty);
                    updateCommand.Parameters.AddWithValue("@Gpa", gpa);
                    updateCommand.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("SUCCESS: Student record updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Update failed.");
                    }
                }
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("--- DELETE STUDENT RECORD ---");
            Console.Write("Enter Student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            Console.Write($"Are you sure you want to delete student with ID {id}? (y/n): ");
            string confirm = (Console.ReadLine() ?? "").ToLower();
            if (confirm != "y" && confirm != "yes")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Students WHERE Id = @Id;";

                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"SUCCESS: Student with ID {id} deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Student with ID {id} was not found.");
                    }
                }
            }
        }
    }
}
