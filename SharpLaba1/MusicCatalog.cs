using System;
using System.Collections.Generic;
using System.Linq;

public class MusicCatalog
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
