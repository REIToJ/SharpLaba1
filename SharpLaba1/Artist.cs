using System;
using System.Collections.Generic;

public class Artist
{
    public string Name { get; set; }
    public List<string> Genres { get; set; }
    public List<Album> Albums { get; set; }

    public Artist(string name, List<string> genres)
    {
        Name = name;
        Genres = genres;
        Albums = new List<Album>();
    }
}
