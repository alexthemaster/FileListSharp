using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace FileListSharp.Builders;

public record FileListTorrent
{
    /// <summary>
    /// The ID of the torrent
    /// </summary>
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    /// <summary>
    /// The full name of the torrent
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The IMDb ID of the torrent's content
    /// </summary>
    [JsonPropertyName("imdb")]
    public string? Imdb { get; init; }

    /// <summary>
    /// Whether this torrent is freeleech
    /// </summary>
    [JsonPropertyName("freeleech")]
    public required int FreeLeech { get; init; }

    /// <summary>
    /// Whether this torrent counts as a double upload
    /// </summary>
    [JsonPropertyName("doubleup")]
    public required int DoubleUp { get; init; }

    /// <summary>
    /// The upload date of the torrent
    /// </summary>
    [JsonPropertyName("upload_date")]
    public required string UploadDate { get; init; }

    /// <summary>
    /// URL used for downloading this torrent
    /// </summary>
    [JsonPropertyName("download_link")]
    public required string DownloadLink { get; init; }

    /// <summary>
    /// The size of the torrent in bytes
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; init; }

    /// <summary>
    /// Whether this torrent was uploaded by the internal FileList team
    /// </summary>
    [JsonPropertyName("internal")]
    public int Internal { get; init; }

    /// <summary>
    /// Whether this torrent is moderated
    /// </summary>
    [JsonPropertyName("moderated")]
    public int Moderated { get; init; }

    /// <summary>
    /// The category this torrent is in
    /// </summary>
    [JsonPropertyName("category")]
    public required string Category { get; init; }

    /// <summary>
    /// The number of people seeding this torrent
    /// </summary>
    [JsonPropertyName("seeders")]
    public int Seeders { get; init; }

    /// <summary>
    /// The number of people leeching (downloading) this torrent
    /// </summary>
    [JsonPropertyName("leechers")]
    public int Leechers { get; init; }

    /// <summary>
    /// The number of times this torrent was snatched (downloaded)
    /// </summary>
    [JsonPropertyName("times_completed")]
    public int TimesCompleted { get; init; }

    /// <summary>
    /// The number of comments left on this torrent
    /// </summary>
    [JsonPropertyName("comments")]
    public int Comments { get; init; }

    /// <summary>
    /// The number of files this torrent contains
    /// </summary>
    [JsonPropertyName("files")]
    public int Files { get; init; }

    /// <summary>
    /// A small description of the torrent
    /// </summary>
    public string? Description { get; init; }

    [JsonPropertyName("tv")] public Tv? Tv { get; init; }
}

public record Tv
{
    /// <summary>
    /// The season of the TV show contained in this torrent (where applicable) 
    /// </summary>   
    [JsonPropertyName("season")]
    public int? Season { get; init; }

    /// <summary>
    /// The episode of the TV show contained in this torrent (where applicable)
    /// </summary>
    [JsonPropertyName("episode")]
    public int? Episode { get; init; }
}