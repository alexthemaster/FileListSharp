using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using FileListSharp.Builders;

namespace FileListSharp;

public class FileList
{
    private const string ApiUrl = "https://filelist.io/api.php";
    private readonly HttpClient _client = new();

    /// <summary>
    /// Create an instance of FileListSharp
    /// </summary>
    /// <param name="username">Your filelist.io username</param>
    /// <param name="passkey">Your filelist.io passkey, can be found <see href="https://filelist.io/my.php">here</see></param>
    public FileList(string username, string passkey)
    {
        // Convert username and passkey to base64 to use for authentication
        var bytes = Encoding.ASCII.GetBytes($"{username}:{passkey}");
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
    }

    /// <summary>
    /// Search for a torrent on filelist.io
    /// </summary>
    /// <param name="searchParams">Created <c>FileListSearchParams</c> class</param>
    /// <example>
    /// <code>
    /// using FileListSharp.Builders;
    /// var searchParams = new FileListSearchParams().Query("The Haunting of Hill House").Categories([21]).FreeLeech(true);
    /// </code>
    /// </example>
    /// <exception cref="Exception">If the API returns an error or rate limit is reached</exception>
    /// <returns>FileListTorrent</returns>
    public async Task<List<FileListTorrent>?> SearchAsync(FileListSearchParams searchParams)
    {
        var parameters = new StringBuilder(searchParams.ToString());
        parameters.Append("&action=search-torrents&output=json");
        return await _Query(parameters.ToString());
    }

    /// <summary>
    /// Look up the latest torrents uploaded to filelist.io
    /// </summary>
    /// <param name="searchParams">Created <c>FileListLatestParams</c> class</param>
    /// <example>
    /// <code>
    /// using FileListSharp.Builders;
    /// var latestParams = new FileListLatestParams().Imdb("tt14527626").Categories([21]);
    /// </code>
    /// </example>
    /// <exception cref="Exception">If the API returns an error or rate limit is reached</exception>
    /// <returns>FileListTorrent</returns>
    public async Task<List<FileListTorrent>?> LatestAsync(FileListLatestParams searchParams)
    {
        var parameters = new StringBuilder(searchParams.ToString());
        parameters.Append("&action=latest-torrents&output=json");
        return await _Query(parameters.ToString());
    }

    private async Task<List<FileListTorrent>?> _Query(string parameters)
    {
        var url = $"{ApiUrl}?{HttpUtility.UrlDecode(parameters)}";
        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new Exception("Rate limit reached, try again later.");
            }

            var error = JsonSerializer.Deserialize<FileListError>(await response.Content.ReadAsStringAsync());
            throw new Exception($"Failed to successfully query the API, error: {error.Error ?? "Unknown"}");
        }

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<FileListTorrent>>(content);
    }
}

internal struct FileListError(string? error)
{
    public readonly string? Error = error;
}