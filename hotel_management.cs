using System;
using System.Collections.Generic;

// Abstract class for Room
abstract class Room
{
    public int RoomNumber { get; set; }
    public double Price { get; set; }
    public bool IsBooked { get; set; }

    public abstract void BookRoom();
    public virtual void ShowDetails()
    {
        Console.WriteLine($"Room {RoomNumber}: ${Price}, Booked: {IsBooked}");
    }
}

// Inheritance: Derived classes
class StandardRoom : Room
{
    public override void BookRoom()
    {
        IsBooked = true;
        Console.WriteLine("Standard Room booked.");
    }
}

class DeluxeRoom : Room
{
    public override void BookRoom()
    {
        IsBooked = true;
        Console.WriteLine("Deluxe Room booked.");
    }
}

// Encapsulation: Hotel class with private member
class Hotel
{
    private List<Room> rooms = new List<Room>();
    public string Name { get; set; }

    public event Action<string> RoomBooked; // Event

    public Hotel(string name)
    {
        Name = name;
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public void BookRoom(int roomNumber)
    {
        Room room = rooms.Find(r => r.RoomNumber == roomNumber);
        if (room != null && !room.IsBooked)
        {
            room.BookRoom();
            RoomBooked?.Invoke($"Room {roomNumber} booked successfully!");
        }
        else
        {
            throw new Exception("Room not available or already booked.");
        }
    }

    // Indexer to access rooms by index
    public Room this[int index]
    {
        get { return rooms[index]; }
        set { rooms[index] = value; }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Hotel hotel = new Hotel("Grand Palace");
            hotel.RoomBooked += msg => Console.WriteLine($"Event: {msg}"); // Anonymous method

            // Collections: List of Rooms
            hotel.AddRoom(new StandardRoom { RoomNumber = 101, Price = 100 });
            hotel.AddRoom(new DeluxeRoom { RoomNumber = 102, Price = 200 });

            // Using indexer
            Console.WriteLine("Before Booking:");
            hotel[0].ShowDetails();

            // Book a room
            hotel.BookRoom(101);

            Console.WriteLine("After Booking:");
            hotel[0].ShowDetails();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
