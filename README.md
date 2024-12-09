## A simple FileList wrapper made for .NET

### ⚠️ Important to know: FileList.io's API has a rate limit of 150 requests an hour, so use with caution! When the rate limit is hit the library will throw an exception.

## [Install from NuGet](https://www.nuget.org/packages/FileListSharp)

## Usage
FileListSharp uses builders for your convenience! These are FileListSearchParams and FileListLatestParams in particular.

Usage example:
```csharp
using FileListSharp;
using FileListSharp.Builders;

var flClient = new FileList("username", "passkey");

var searchParams = new FileListSearchParams()
    .Type("name")
    .Query("The Haunting of Hill House")
    // Everything below is optional
    .Categories([21])
    .Moderated(false)
    .Internal(false)
    .DoubleUp(false)
    .Season(1)
    .Episode(5)
    .FreeLeech(true);

var searchedList = await flClient.SearchAsync(searchParams);

var latestParams = new FileListLatestParams().Imdb("tt14527626").Categories([21, 20]);
var latest = (await flClient.LatestAsync(latestParams))!.First();
Console.WriteLine($"Just looked up torrent {latest.Name} with the ID of {latest.Id}, size of {latest.Size/1024/1024/1024}GB, uploaded on {latest.UploadDate}. It has {latest.Seeders} seeders and {latest.Leechers} leechers.");

```

Your passkey can be obtained from [here](https://filelist.io/my.php)<br>
Category IDs can be found [here](https://gist.github.com/alexthemaster/c4a64a718e5db2128a8b179ff1ca86e3)