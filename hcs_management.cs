using System;
using System.Collections;
using System.Collections.Generic;

// Abstraction: Defining an abstract class for common attributes
abstract class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public abstract void DisplayDetails();
}

// Inheritance: Patient class inherits from Person
class Patient : Person
{
    private int _patientId; // Encapsulation: private field

    public int PatientId
    {
        get { return _patientId; }
        set { _patientId = value; }
    }

    public string Disease { get; set; }

    public override void DisplayDetails()
    {
        Console.WriteLine($"Patient ID: {_patientId}, Name: {Name}, Age: {Age}, Gender: {Gender}, Disease: {Disease}");
    }
}

// Inheritance: Doctor class inherits from Person
class Doctor : Person
{
    public int DoctorId { get; set; }
    public string Specialization { get; set; }

    public override void DisplayDetails()
    {
        Console.WriteLine($"Doctor ID: {DoctorId}, Name: {Name}, Age: {Age}, Gender: {Gender}, Specialization: {Specialization}");
    }
}

// Polymorphism: Method overloading & overriding
class Hospital
{
    private List<Patient> patients = new List<Patient>();
    private List<Doctor> doctors = new List<Doctor>();

    public void AdmitPatient(Patient patient)
    {
        patients.Add(patient);
        Console.WriteLine($"Patient {patient.Name} admitted successfully.");
    }

    public void HireDoctor(Doctor doctor)
    {
        doctors.Add(doctor);
        Console.WriteLine($"Doctor {doctor.Name} hired successfully.");
    }

    // Indexer to access patients
    public Patient this[int index]
    {
        get { return patients[index]; }
        set { patients[index] = value; }
    }

    // Delegate & Event
    public delegate void AppointmentHandler(string message);
    public event AppointmentHandler AppointmentScheduled;

    public void ScheduleAppointment(Patient patient, Doctor doctor)
    {
        string message = $"Appointment scheduled for {patient.Name} with Dr. {doctor.Name}";
        AppointmentScheduled?.Invoke(message);
    }
}

class Program
{
    static void Main()
    {
        Hospital hospital = new Hospital();

        // Subscribe to the event
        hospital.AppointmentScheduled += (message) => Console.WriteLine(message);

        // Using Collections
        List<Person> people = new List<Person>();

        Patient p1 = new Patient { PatientId = 101, Name = "John Doe", Age = 30, Gender = "Male", Disease = "Fever" };
        Doctor d1 = new Doctor { DoctorId = 201, Name = "Dr. Smith", Age = 45, Gender = "Male", Specialization = "Cardiologist" };

        hospital.AdmitPatient(p1);
        hospital.HireDoctor(d1);

        people.Add(p1);
        people.Add(d1);

        // Anonymous Method
        Action greet = delegate
        {
            Console.WriteLine("Welcome to the Healthcare Management System!");
        };

        greet();

        // Scheduling an appointment
        hospital.ScheduleAppointment(p1, d1);

        // Using indexer
        Console.WriteLine("Patient via Indexer: " + hospital[0].Name);

        // Display all persons
        foreach (var person in people)
        {
            person.DisplayDetails();
        }
    }
}