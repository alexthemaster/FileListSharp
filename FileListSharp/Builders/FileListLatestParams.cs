using System.Dynamic;
using System.Text.RegularExpressions;

namespace FileListSharp.Builders;

public partial class FileListLatestParams
{
    private int? _limit;
    private string? _imdb;
    private int[]? _category;

    public override string ToString()
    {
        var final = "";

        if (_limit is not null) final += $"&limit={_limit}";
        if (_imdb is not null) final += $"&imdb={_imdb}";
        if (_category is not null) final += $"&category={string.Join(",", _category)}";

        return final;
    }

    /// <summary>
    /// Maximum number of torrents displayed in the request. Can be 1-100.
    /// </summary>
    /// <param name="limit">Integer ranging from 1-100</param>
    /// <returns>FileListLatestParams</returns>
    /// <exception cref="ArgumentException">Invalid limit provided</exception>
    public FileListLatestParams Limit(int limit)
    {
        if (limit is < 1 or > 100)
            throw new ArgumentException($"Invalid limit provided in {GetType().Name}. Valid limits are 1-100");
        _limit = limit;
        return this;
    }

    /// <summary>
    /// Defines an IMDb ID for filtering recent torrents
    /// </summary>
    /// <param name="imdb"></param>
    /// <returns>FileListLatestParams</returns>
    /// <exception cref="ArgumentException">In case incorrect IMDb ID is provided</exception>
    public FileListLatestParams Imdb(string imdb)
    {
        if (!ImdbRegex().IsMatch(imdb))
        {
            throw new ArgumentException(
                "When using the IMDb search type you can only use IMDb IDs (as tt0000000 or 0000000)");
        }

        _imdb = imdb;
        return this;
    }

    /// <summary>
    /// Defines an array of categories for filtering recent torrents, for information on that IDs are which access <see href="https://gist.github.com/alexthemaster/c4a64a718e5db2128a8b179ff1ca86e3">this</see>
    /// </summary>
    /// <param name="categories">An array of catogories</param>
    /// <returns>FileListLatestParams</returns>
    /// <exception cref="ArgumentException">If incorrect category ID is provided</exception>
    public FileListLatestParams Categories(int[] categories)
    {
        if (categories.All(v => v is < 1 or > 27))
        {
            throw new ArgumentException("Invalid category number provided. Valid categories are 1-27");
        }

        _category = categories;
        return this;
    }

    [GeneratedRegex(@"(tt[0-9]*)")]
    private static partial Regex ImdbRegex();
}