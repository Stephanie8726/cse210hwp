using System;
using System.Collections.Generic;
using System.IO;

// base class for media items
abstract class ContentItem
{
    public string Title { get; set; }
    public string Author { get; set; }
}

// representing a comment
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }
}

// representing a video
class Video : ContentItem
{
    public int LengthInSeconds { get; set; }
    private List<Comment> comments = new List<Comment>();

    //add a comment to the video
    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment
        {
            CommenterName = commenterName,
            Text = commentText
        };
        comments.Add(comment);
    }

    //get the number of comments
    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    //display information about the video and its comments
    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video
        {
            Title = "Introduction to C#",
            Author = "Master Dacullo",
            LengthInSeconds = 300
        };
        video1.AddComment("MattSmith19", "Great video!");
        video1.AddComment("GoodBhoyz8", "I learned a lot.");

        Video video2 = new Video
        {
            Title = "OOP Principles",
            Author = "Wizard Laruga",
            LengthInSeconds = 400
        };
        video2.AddComment("Jiggzjam5", "Very informative!");
        video2.AddComment("Just4Me", "Thanks for explaining.");

        Video video3 = new Video
        {
            Title = "C# Best Practices",
            Author = "Guru Stephanie",
            LengthInSeconds = 350
        };
        video3.AddComment("Jen&Josh", "I wish I knew this earlier.");
        video3.AddComment("IamBeautiful", "Keep up the good work!");

        // Create a list of videos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display information about each video
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}