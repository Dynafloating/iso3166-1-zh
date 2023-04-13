namespace Processors;

public abstract class ProcessorBase : IProcessor
{
    public abstract string WikiUrl { get; }

    public abstract Task<Country[]> ListCountryAsync();

    public virtual async Task<IEnumerable<string>> ListTableRowsAsync(string url)
    {
        using var httpClient = new HttpClient();
        var webData = await httpClient.GetStringAsync(url);

        var tbody = webData.Split("<table class=\"wikitable sortable\">").Last()
            .Split("</table>").First()
            .Replace("<tbody>", "").Replace("</tbody>", "");

        return tbody.Split("<tr>")
            .Select(o => o.Replace("</tr>", ""))
            .Where(o => o != "\n\n" && !o.StartsWith("\n<th scope=\"col\">"));
    }
}
