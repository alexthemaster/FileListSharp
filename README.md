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
    .Query("Name of a show")
    // Everything below is optional
    .Categories([21])
    .Moderated(false)
    .Internal(false)
    .DoubleUp(false)
    .Season(1)
    .Episode(5)
    .FreeLeech(true);

var searchedList = await flClient.SearchAsync(searchParams);
if (searchedList.Count > 0)
{
    Console.WriteLine($"Here's the first result for the torrent you searched for: ${searchedList.First()}");
}

var latestParams = new FileListLatestParams()
    // All are optional
    .Imdb("tt14527626")
    .Limit(20)
    .Categories([21, 20]);
var latest = (await flClient.LatestAsync(latestParams))!.First();
Console.WriteLine($"Just looked up torrent {latest.Name} with the ID of {latest.Id}, size of {latest.Size/1024/1024/1024}GB, uploaded on {latest.UploadDate}. It has {latest.Seeders} seeders and {latest.Leechers} leechers. Is it freeleech? {latest.FreeLeech == 1}");

```

Your passkey can be obtained from [here](https://filelist.io/my.php)<br>
Category IDs can be found [here](https://gist.github.com/alexthemaster/c4a64a718e5db2128a8b179ff1ca86e3)
