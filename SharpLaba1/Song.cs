using System;

public class Song
{
    public string Title { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; }
    public Album Album { get; set; }

    public Song(string title, int duration, string genre, Album album)
    {
        Title = title;
        Duration = duration;
        Genre = genre;
        Album = album;
    }
}
