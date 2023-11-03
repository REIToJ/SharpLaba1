using System;
using System.Collections.Generic;
using System.Linq;

// Define the Genre class (Optional)
public class Genre
{
    public string Name { get; set; }

    public Genre(string name)
    {
        Name = name;
    }
}

// Define the Artist class
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

// Define the Album class
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

// Define the Song class
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

// Define the Compilation class
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

class MusicCatalog
{
    private List<Artist> artists = new List<Artist>();
    private List<Album> albums = new List<Album>();
    private List<Song> songs = new List<Song>();
    private List<Compilation> compilations = new List<Compilation>();

    public void AddArtist(Artist artist)
    {
        artists.Add(artist);
    }

    public void RemoveArtist(Artist artist)
    {
        artists.Remove(artist);

        // Remove all albums associated with the artist
        var artistAlbums = albums.Where(album => album.Artist == artist).ToList();
        foreach (var album in artistAlbums)
        {
            RemoveAlbum(album);
        }

        // Remove all songs associated with the artist
        var artistSongs = songs.Where(song => song.Album.Artist == artist).ToList();
        foreach (var song in artistSongs)
        {
            RemoveSong(song);
        }
    }

    public void AddAlbum(Album album)
    {
        albums.Add(album);
    }

    public void RemoveAlbum(Album album)
    {
        albums.Remove(album);

        // Remove all songs associated with the album
        var albumSongs = songs.Where(song => song.Album == album).ToList();
        foreach (var song in albumSongs)
        {
            RemoveSong(song);
        }
    }

    public void AddSong(Song song)
    {
        songs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        // Remove the song from the main song list
        songs.Remove(song);

        // Remove the song from all compilations it belongs to
        List<Compilation> compilationsToRemoveFrom = compilations.Where(compilation => compilation.Songs.Contains(song)).ToList();
        foreach (var compilation in compilationsToRemoveFrom)
        {
            compilation.Songs.Remove(song);
        }
    }


    public void AddCompilation(Compilation compilation)
    {
        compilations.Add(compilation);
    }

    public void RemoveCompilation(Compilation compilation)
    {
        compilations.Remove(compilation);
    }

    public List<Artist> SearchArtists(string keyword)
    {
        return artists.Where(artist => artist.Name.Contains(keyword) || artist.Genres.Contains(keyword)).ToList();
    }

    public List<Album> SearchAlbums(string keyword)
    {
        return albums.Where(album => album.Title.Contains(keyword)).ToList();
    }

    public List<Song> SearchSongs(string keyword, string genre, int minDuration, int maxDuration)
    {
        return songs.Where(song =>
            (string.IsNullOrEmpty(keyword) || song.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(genre) || song.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)) &&
            song.Duration >= minDuration &&
            song.Duration <= maxDuration
        ).ToList();
    }
    public List<Compilation> SearchCompilations(string keyword)
    {
        return compilations.Where(compilation => compilation.Title.Contains(keyword)).ToList();
    }
    public List<Compilation> GetCompilations()
    {
        return compilations;
    }
}

class Program
{
    static void Main()
    {
        MusicCatalog catalog = new MusicCatalog();

        // Create artists, albums, songs, and compilations
        Artist artistA = new Artist("Prorok Sunboy", new List<string> { "Rock" });
        Artist artistB = new Artist("Serega Pirat", new List<string> { "Rap" });
        Album album1 = new Album("Na Lunu", artistA);
        Album album2 = new Album("Velikoe", artistB);
        Song song1 = new Song("Ostrov", 360, "Rock", album1);
        Song song2 = new Song("Troika Semerka", 210, "Rock", album1);
        Song song3 = new Song("Moi Bike", 180, "Pop", album2);
        Compilation compilation1 = new Compilation("Poplakat");
        compilation1.Songs.Add(song1);
        compilation1.Songs.Add(song2);

        // Add artists, albums, songs, and compilations to the catalog
        catalog.AddArtist(artistA);
        catalog.AddArtist(artistB);
        catalog.AddAlbum(album1);
        catalog.AddAlbum(album2);
        catalog.AddSong(song1);
        catalog.AddSong(song2);
        catalog.AddSong(song3);
        catalog.AddCompilation(compilation1);

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add Artist");
            Console.WriteLine("2. Remove Artist");
            Console.WriteLine("3. Add Album");
            Console.WriteLine("4. Remove Album");
            Console.WriteLine("5. Add Compilation");
            Console.WriteLine("6. Remove Compilation");
            Console.WriteLine("7. Add Song to Compilation");
            Console.WriteLine("8. Remove Song from Compilation");
            Console.WriteLine("9. Add Song");
            Console.WriteLine("10. Remove Song");
            Console.WriteLine("11. Search for Artists");
            Console.WriteLine("12. Search for Albums");
            Console.WriteLine("13. Search for Compilations");
            Console.WriteLine("14. Search for Songs");
            Console.WriteLine("15. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter artist name: ");
                    string artistName = Console.ReadLine();
                    Console.Write("Enter artist genre(s) (comma-separated): ");
                    List<string> artistGenres = Console.ReadLine().Split(',').Select(s => s.Trim()).ToList();
                    Artist newArtist = new Artist(artistName, artistGenres);
                    catalog.AddArtist(newArtist);
                    Console.WriteLine("Artist added successfully.");
                    break;

                case "2":
                    Console.Write("Enter artist name to remove: ");
                    string artistToRemove = Console.ReadLine();
                    List<Artist> artistsToRemove = catalog.SearchArtists(artistToRemove);
                    if (artistsToRemove.Count == 0)
                    {
                        Console.WriteLine("Artist not found in the catalog.");
                    }
                    else
                    {
                        foreach (var artist in artistsToRemove)
                        {
                            catalog.RemoveArtist(artist);
                            Console.WriteLine($"{artist.Name} removed from the catalog.");
                        }
                    }
                    break;

                case "3":
                    Console.Write("Enter album title: ");
                    string albumTitle = Console.ReadLine();
                    Console.Write("Enter artist name: ");
                    string artistNameForAlbum = Console.ReadLine();
                    Artist artistForAlbum = catalog.SearchArtists(artistNameForAlbum).FirstOrDefault();
                    if (artistForAlbum == null)
                    {
                        Console.WriteLine("Artist not found in the catalog. Please add the artist first.");
                    }
                    else
                    {
                        Album newAlbum = new Album(albumTitle, artistForAlbum);
                        catalog.AddAlbum(newAlbum);
                        Console.WriteLine("Album added successfully.");
                    }
                    break;

                case "4":
                    Console.Write("Enter album title to remove: ");
                    string albumToRemove = Console.ReadLine();
                    List<Album> albumsToRemove = catalog.SearchAlbums(albumToRemove);
                    if (albumsToRemove.Count == 0)
                    {
                        Console.WriteLine("Album not found in the catalog.");
                    }
                    else
                    {
                        foreach (var album in albumsToRemove)
                        {
                            catalog.RemoveAlbum(album);
                            Console.WriteLine($"{album.Title} removed from the catalog.");
                        }
                    }
                    break;

                case "5":
                    Console.Write("Enter compilation title: ");
                    string compilationTitle = Console.ReadLine();
                    Compilation newCompilation = new Compilation(compilationTitle);
                    catalog.AddCompilation(newCompilation);
                    Console.WriteLine("Compilation added successfully.");
                    break;

                case "6":
                    Console.Write("Enter compilation title to remove: ");
                    string compilationToRemove = Console.ReadLine();
                    List<Compilation> compilationsToRemove = catalog.SearchCompilations(compilationToRemove);
                    if (compilationsToRemove.Count == 0)
                    {
                        Console.WriteLine("Compilation not found in the catalog.");
                    }
                    else
                    {
                        foreach (var compilation in compilationsToRemove)
                        {
                            catalog.RemoveCompilation(compilation);
                            Console.WriteLine($"{compilation.Title} removed from the catalog.");
                        }
                    }
                    break;
                case "7":
                    Console.Write("Enter compilation title to add a song: ");
                    string compilationTitleToAddSong = Console.ReadLine();
                    Compilation compilationToAddSong = catalog.SearchCompilations(compilationTitleToAddSong).FirstOrDefault();
                    if (compilationToAddSong == null)
                    {
                        Console.WriteLine("Compilation not found in the catalog.");
                    }
                    else
                    {
                        Console.Write("Enter song title to add to the compilation: ");
                        string songTitleToAdd = Console.ReadLine();
                        if (songTitleToAdd != "")
                        {
                            Song songToAdd = catalog.SearchSongs(songTitleToAdd, "", 0, int.MaxValue).FirstOrDefault();
                            if (songToAdd == null)
                            {
                                Console.WriteLine("Song not found in the catalog.");
                            }
                            else
                            {
                                compilationToAddSong.Songs.Add(songToAdd);
                                Console.WriteLine($"{songToAdd.Title} added to {compilationToAddSong.Title}.");
                            }
                        } else
                        {
                            Console.WriteLine("Please provide a song title.");
                        }
                        
                    }
                    break;

                case "8":
                    Console.Write("Enter compilation title to remove a song: ");
                    string compilationTitleToRemoveSong = Console.ReadLine();
                    Compilation compilationToRemoveSong = catalog.SearchCompilations(compilationTitleToRemoveSong).FirstOrDefault();
                    if (compilationToRemoveSong == null)
                    {
                        Console.WriteLine("Compilation not found in the catalog.");
                    }
                    else
                    {
                        Console.Write("Enter song title to remove from the compilation: ");
                        string songTitleToRemove1 = Console.ReadLine();
                        if (songTitleToRemove1 != "")
                        {
                            Song songToRemove1 = compilationToRemoveSong.Songs.FirstOrDefault(s => s.Title.Equals(songTitleToRemove1, StringComparison.OrdinalIgnoreCase));
                            if (songToRemove1 == null)
                            {
                                Console.WriteLine("Song not found in the compilation.");
                            }
                            else
                            {
                                compilationToRemoveSong.Songs.Remove(songToRemove1);
                                Console.WriteLine($"{songToRemove1.Title} removed from {compilationToRemoveSong.Title}.");
                            }
                        } else
                        {
                            Console.WriteLine("Please provide a song title.");
                        }
                        
                    }
                    break;
                case "9":
                    Console.Write("Enter song title: ");
                    string songTitle = Console.ReadLine();
                    Console.Write("Enter song duration (in seconds): ");
                    int duration = int.Parse(Console.ReadLine());
                    Console.Write("Enter genre: ");
                    string songGenre = Console.ReadLine();
                    Console.Write("Enter album title: ");
                    string albumTitleForSong = Console.ReadLine();
                    Album albumForSong = catalog.SearchAlbums(albumTitleForSong).FirstOrDefault();
                    if (albumForSong == null)
                    {
                        Console.WriteLine("Album not found in the catalog. Please add the album first.");
                    }
                    else
                    {
                        Song newSong = new Song(songTitle, duration, songGenre, albumForSong);
                        catalog.AddSong(newSong);
                        Console.WriteLine("Song added successfully.");
                    }
                    break;
                case "10":
                    Console.Write("Enter song title to remove: ");
                    string songToRemove = Console.ReadLine();
                    List<Song> songsToRemove = catalog.SearchSongs(songToRemove, "", 0, int.MaxValue);
                    if (songsToRemove.Count == 0)
                    {
                        Console.WriteLine("Song not found in the catalog.");
                    }
                    else
                    {
                        foreach (var song in songsToRemove)
                        {
                            catalog.RemoveSong(song);
                            Console.WriteLine($"{song.Title} removed from the catalog.");
                        }
                    }
                    break;

                case "11":
                    Console.Write("Enter a keyword to search for artists: ");
                    string artistKeyword = Console.ReadLine();
                    List<Artist> foundArtists = catalog.SearchArtists(artistKeyword);
                    Console.WriteLine("Found Artists:");
                    foreach (var artist in foundArtists)
                    {
                        Console.WriteLine(artist.Name);
                    }
                    break;

                case "12":
                    Console.Write("Enter a title to search for albums: ");
                    string albumKeyword = Console.ReadLine();
                    List<Album> foundAlbums = catalog.SearchAlbums(albumKeyword);
                    Console.WriteLine("Found Albums:");
                    foreach (var album in foundAlbums)
                    {
                        Console.WriteLine(album.Title);
                    }
                    break;
                case "13":
                    Console.Write("Enter a title to search for compilations: ");
                    string compilationKeyword = Console.ReadLine();
                    List<Compilation> foundCompilations = catalog.SearchCompilations(compilationKeyword);
                    Console.WriteLine("Found Compilations:");
                    foreach (var compilation in foundCompilations)
                    {
                        Console.WriteLine(compilation.Title);
                        foreach (var song in compilation.Songs)
                        {
                            Console.WriteLine(song.Title);
                        }
                    }
                    break;
                case "14":
                    Console.Write("Enter a title to search for songs: ");
                    string songKeyword = Console.ReadLine();
                    Console.Write("Enter genre (leave blank to ignore): ");
                    string songGenreFilter = Console.ReadLine();
                    Console.Write("Enter minimum duration (seconds, leave blank to ignore): ");
                    int minDuration = 0;
                    string minDurHelp = Console.ReadLine();
                    if (minDurHelp != "")
                    {
                        minDuration = int.Parse(minDurHelp);
                    }
                    Console.Write("Enter maximum duration (seconds, leave blank to ignore): ");
                    int maxDuration = 9999;
                    string maxDurHelp = Console.ReadLine();
                    if (maxDurHelp != "")
                    {
                        maxDuration = int.Parse(maxDurHelp);
                    }
                    List<Song> foundSongs = catalog.SearchSongs(songKeyword, songGenreFilter, minDuration, maxDuration);
                    Console.WriteLine("Found Songs:");
                    foreach (var song in foundSongs)
                    {
                        Console.WriteLine($"{song.Title} ({song.Genre}), Duration: {song.Duration} seconds, Album: {song.Album.Title}");
                    }
                    break;
                
                case "15":
                    Environment.Exit(0);
                    break;
                
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

