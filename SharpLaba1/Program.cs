using System;
using System.Collections.Generic;
using System.Linq;


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

