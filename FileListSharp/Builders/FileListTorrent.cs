using Newtonsoft.Json;

namespace FileListSharp.Builders;

public class FileListTorrent(
    int id,
    string name,
    string? imdb,
    bool freeleech,
    bool doubleup,
    [JsonProperty("upload_date")] string uploadDate,
    [JsonProperty("download_link")] string downloadLink,
    long size,
    [JsonProperty("internal")] int internalnum,
    int moderated,
    string category,
    int seeders,
    int leechers,
    [JsonProperty("times_completed")] int timesCompleted,
    int comments,
    int files,
    string description,
    [JsonProperty("tv")] 
    Dictionary<string,int?> tv)
{
    /// <summary>
    /// The ID of the torrent
    /// </summary>
    public readonly int Id = id;

    /// <summary>
    /// The full name of the torrent
    /// </summary>
    public readonly string Name = name;

    /// <summary>
    /// The IMDb ID of the torrent's content
    /// </summary>
    public readonly string? Imdb = imdb;

    /// <summary>
    /// Whether this torrent is freeleech
    /// </summary>
    public readonly bool Freeleech = freeleech;

    /// <summary>
    /// Whether this torrent counts as a double upload
    /// </summary>
    public readonly bool Doubleup = doubleup;

    /// <summary>
    /// The upload date of the torrent
    /// </summary>
    public readonly string UploadDate = uploadDate;

    /// <summary>
    /// URL used for downloading this torrent
    /// </summary>
    public readonly string DownloadLink = downloadLink;

    /// <summary>
    /// The size of the torrent in bytes
    /// </summary>
    public readonly long Size = size;

    /// <summary>
    /// Whether this torrent was uploaded by the internal FileList team
    /// </summary>
    public readonly bool Internal = internalnum is 1;

    /// <summary>
    /// Whether this torrent is moderated
    /// </summary>
    public readonly bool Moderated = moderated is 1;

    /// <summary>
    /// The category this torrent is in
    /// </summary>
    public readonly string Category = category;

    /// <summary>
    /// The number of people seeding this torrent
    /// </summary>
    public readonly int Seeders = seeders;

    /// <summary>
    /// The number of people leeching (downloading) this torrent
    /// </summary>
    public readonly int Leechers = leechers;

    /// <summary>
    /// The number of times this torrent was snatched (downloaded)
    /// </summary>
    public readonly int TimesCompleted = timesCompleted;

    /// <summary>
    /// The number of comments left on this torrent
    /// </summary>
    public readonly int Comments = comments;

    /// <summary>
    /// The number of files this torrent contains
    /// </summary>
    public readonly int Files = files;

    /// <summary>
    /// A small description of the torrent
    /// </summary>
    public readonly string Description = description;

    /// <summary>
    /// The season of the TV show contained in this torrent (where applicable) 
    /// </summary>
    public readonly int? TvSeason = tv["season"];

    /// <summary>
    /// The episode of the TV show contained in this torrent (where applicable)
    /// </summary>
    public readonly int? TvEpisode = tv["episode"];
}