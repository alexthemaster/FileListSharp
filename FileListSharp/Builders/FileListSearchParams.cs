using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace FileListSharp.Builders;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public partial class FileListSearchParams
{
    private string _type = "name";
    private string? _query;
    private int[]? _category = [];
    private int? _moderated;
    private int? _internal;
    private int? _freeleech;
    private int? _doubleup;
    private int? _season;
    private int? _episode;

    public override string ToString()
    {
        if (_query is null) throw new ArgumentNullException("Query", $"The query for {GetType().Name} cannot be null.");
        var final = $"type={_type}&query={_query}";
            
        if (_category is not null) final += $"&category={string.Join(',', _category)}";
        if (_moderated is not null) final += $"&moderated={_moderated}";
        if (_internal is not null) final += $"&internal={_internal}";
        if (_freeleech is not null) final += $"&freeleech={_freeleech}";
        if (_doubleup is not null) final += $"&doubleup={_doubleup}";
        if (_season is not null) final += $"&season={_season}";
        if (_episode is not null) final += $"&episode={_episode}";
        
        return final;
    }

    /// <summary>
    /// Whether to search torrents based on its name or its tagged IMDb ID <br/> Defaults to <c>name</c>
    /// </summary>
    /// <param name="type">name or imdb</param>
    /// <returns>FileListSearchParams</returns>
    /// <exception cref="ArgumentException">If type is neither name nor imdb</exception>
    public FileListSearchParams Type(string type)
    {
        type = type.ToLower();
        if (type is "name" or "imdb")
        {
            _type = type;
        }
        else
        {
            throw new ArgumentException("The type can only be name or imdb.");
        }

        return this;
    }

    /// <summary>
    /// The query for the search (name of the torrent or an IMDb ID)
    /// </summary>
    /// <param name="query"></param>
    /// <returns>FileListSearchParams</returns>
    /// <exception cref="ArgumentException">If search type is IMDb but no valid IMDb ID is provided</exception>
    public FileListSearchParams Query(string query)
    {
        {
            if (_type is "imdb")
            {
                if (ImdbRegex().IsMatch(query))
                {
                    _query = query;
                }
                else
                {
                    throw new ArgumentException(
                        "When using the IMDb search type you can only use IMDb ids (as tt0000000 or 0000000)");
                }
            }
            else
            {
                _query = query;
            }
        }

        return this;
    }

    /// <summary>
    /// Defines an array of categories for filtering the searched torrents, for information on that IDs are which access <see href="https://gist.github.com/alexthemaster/c4a64a718e5db2128a8b179ff1ca86e3">this</see>
    /// </summary>
    /// <param name="categories">An array of catogories</param>
    /// <returns>FileListSearchParams</returns>
    /// <exception cref="ArgumentException">If incorrect category ID is provided</exception>
    public FileListSearchParams Categories(int[] categories)
    {
        if (categories.All(v => v is < 1 or > 27))
        {
            throw new ArgumentException("Invalid category number provided. Valid categories are 1-27");
        }

        _category = categories;
        return this;
    }
    
    /// <summary>
    /// Whether the searched torrent should be moderated
    /// </summary>
    /// <param name="moderated">True or False</param>
    /// <returns>FileListSearchParams</returns>
    public FileListSearchParams Moderated(bool moderated)
    {
        _moderated = moderated ? 1 : 0;
        return this;
    }

    /// <summary>
    /// Whether the searched torrent should be one uploaded by the internal team
    /// </summary>
    /// <param name="internalbool">True or False</param>
    /// <returns>FileListSearchParams</returns>
    public FileListSearchParams Internal(bool internalbool)
    {
        _internal = internalbool ? 1 : 0;
        return this;
    }

    /// <summary>
    /// Whether the searched torrent should be freeleech
    /// </summary>
    /// <param name="freeleech">True or False</param>
    /// <returns>FileListSearchParams</returns>
    public FileListSearchParams FreeLeech(bool freeleech)
    {
        _freeleech = freeleech ? 1 : 0;
        return this;
    }

    /// <summary>
    /// Whether the searched torrent should have the double upload property or not
    /// </summary>
    /// <param name="doubleup">True or False</param>
    /// <returns>FileListSearchParams</returns>
    public FileListSearchParams DoubleUp(bool doubleup)
    {
        _doubleup = doubleup ? 1 : 0;
        return this;
    }

    /// <summary>
    /// If searching for a specific TV show season, provide it here
    /// </summary>
    /// <param name="season"></param>
    /// <returns>FileListSearchParams</returns>
    public FileListSearchParams Season(int season)
    {
        _season = season;
        return this;
    }

    /// <summary>
    /// If searching for a specific TV show episode, provide it here
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    public FileListSearchParams Episode(int episode)
    {
        _episode = episode;
        return this;
    }

    [GeneratedRegex(@"(tt[0-9]*)")]
    private static partial Regex ImdbRegex();
}