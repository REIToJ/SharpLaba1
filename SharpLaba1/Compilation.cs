using System;
using System.Collections.Generic;

public class Compilation
{
    public string Title { get; set; }
    public List<Song> Songs { get; set; }

    public Compilation(string title)
    {
        Title = title;
        Songs = new List<Song>();
    }
}
