using System;
using System.Collections.Generic;
using System.IO;

// Student Class Implementing IDisposable for Memory Management
class Student : IDisposable
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
    private bool disposed = false;

    public Student(int id, string name, int age, string course)
    {
        ID = id;
        Name = name;
        Age = age;
        Course = course;
    }

    // Destructor
    ~Student()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Free managed resources if needed
            }
            // Free unmanaged resources here if any
            disposed = true;
        }
    }

    public override string ToString()
    {
        return $"{ID}, {Name}, {Age}, {Course}";
    }
}

// Student Management System
class StudentManager
{
    private List<Student> students = new List<Student>();
    private string filePath = "students.txt";

    // Add Student to Collection
    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    // Save Students to File
    public void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine(student);
            }
        }
    }

    // Load Students from File
    public void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(',');
                    students.Add(new Student(
                        int.Parse(data[0]),
                        data[1].Trim(),
                        int.Parse(data[2]),
                        data[3].Trim()
                    ));
                }
            }
        }
    }

    // Display Students
    public void DisplayStudents()
    {
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    // Clear Students and Force Garbage Collection
    public void ClearStudents()
    {
        students.Clear();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}

class Program
{
    static void Main()
    {
        StudentManager manager = new StudentManager();

        // Adding Students
        manager.AddStudent(new Student(1, "Alice Johnson", 20, "Mathematics"));
        manager.AddStudent(new Student(2, "Bob Smith", 22, "Physics"));
        manager.AddStudent(new Student(3, "Charlie Brown", 21, "Computer Science"));

        // Saving and Loading from File
        manager.SaveToFile();
        manager.LoadFromFile();

        // Displaying Students
        Console.WriteLine("Student Records:");
        manager.DisplayStudents();

        // Clearing Students and Forcing Garbage Collection
        manager.ClearStudents();
    }
}
