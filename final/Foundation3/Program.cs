using System;
using System.Collections.Generic;
using System.IO;

// class to represent the address of an event
class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

// Base Event class
class Event
{
    private string title;
    private string description;
    private DateTime date;
    private TimeSpan time;
    private Address address;

    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Details:\nTitle: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address}\n";
    }

    public virtual string GetFullDetails()
    {
        return $"{GetStandardDetails()}Type: General Event\n";
    }

    public virtual string GetShortDescription()
    {
        return $"Short Description:\nType: General Event\nTitle: {title}\nDate: {date.ToShortDateString()}\n";
    }
}

class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}Type: Lecture\nSpeaker: {speaker}\nCapacity: {capacity} attendees\n";
    }
}

class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}Type: Reception\nRSVP Email: {rsvpEmail}\n";
    }
}

class OutdoorGathering : Event
{
    private string weatherStatement;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherStatement)
        : base(title, description, date, time, address)
    {
        this.weatherStatement = weatherStatement;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}Type: Outdoor Gathering\nWeather: {weatherStatement}\n";
    }
}

class Program
{
    static void Main()
    {
        // Create addresses
        Address eventAddress1 = new Address { Street = "437 Marlou Ln", City = "Bougainville", State = "CA", Country = "USA" };

        // Create an outdoor event
        OutdoorGathering outdoorEvent = new OutdoorGathering("Park Picnic", "A picnic in the park", new DateTime(2023, 11, 24), new TimeSpan(4, 0, 0), eventAddress1, "Sunny with a chance of rain");

        // generate marketing messages and display results
        Console.WriteLine(outdoorEvent.GetStandardDetails());
        Console.WriteLine(outdoorEvent.GetFullDetails());
        Console.WriteLine(outdoorEvent.GetShortDescription());
    }
}

