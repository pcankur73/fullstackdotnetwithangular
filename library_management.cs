using System;
using System.Collections;
using System.Collections.Generic;

// Abstraction
abstract class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int BookID { get; set; }

    public Book(string title, string author, int bookID)
    {
        Title = title;
        Author = author;
        BookID = bookID;
    }

    public abstract void DisplayInfo();
}

// Inheritance and Polymorphism
class EBook : Book
{
    public int FileSize { get; set; }

    public EBook(string title, string author, int bookID, int fileSize)
        : base(title, author, bookID)
    {
        FileSize = fileSize;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"E-Book: {Title} by {Author}, File Size: {FileSize}MB");
    }
}

class PrintedBook : Book
{
    public int Pages { get; set; }

    public PrintedBook(string title, string author, int bookID, int pages)
        : base(title, author, bookID)
    {
        Pages = pages;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Printed Book: {Title} by {Author}, Pages: {Pages}");
    }
}

// Encapsulation with private fields
class Library
{
    private List<Book> books = new List<Book>();

    // Delegate and Event
    public delegate void BookBorrowedHandler(string message);
    public event BookBorrowedHandler BookBorrowed;

    // Indexer
    public Book this[int index]
    {
        get { return books[index]; }
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void BorrowBook(int bookID)
    {
        Book book = books.Find(b => b.BookID == bookID);
        if (book != null)
        {
            books.Remove(book);
            BookBorrowed?.Invoke($"{book.Title} has been borrowed.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void ShowBooks()
    {
        foreach (var book in books)
        {
            book.DisplayInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        library.BookBorrowed += message => Console.WriteLine($"Event: {message}"); // Anonymous Method

        library.AddBook(new EBook("C# Programming", "John Doe", 1, 5));
        library.AddBook(new PrintedBook("Design Patterns", "Gamma et al.", 2, 395));
        library.AddBook(new PrintedBook("Numerology Basics", "Dr. Smith", 3, 250));

        Console.WriteLine("Available Books:");
        library.ShowBooks();

        // Borrowing a book
        library.BorrowBook(2);

        Console.WriteLine("\nAvailable Books After Borrowing:");
        library.ShowBooks();
    }
}
