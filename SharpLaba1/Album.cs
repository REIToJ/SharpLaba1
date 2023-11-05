using System;
using System.Collections.Generic;

public class Album
{
    public string Title { get; set; }
    public Artist Artist { get; set; }
    public List<Song> Songs { get; set; }

    public Album(string title, Artist artist)
    {
        Title = title;
        Artist = artist;
        Songs = new List<Song>();
    }
}
