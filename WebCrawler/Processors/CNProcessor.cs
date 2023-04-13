namespace Processors;

public class CNProcessor : ProcessorBase
{
    public override string WikiUrl => "https://zh.wikipedia.org/zh-cn/ISO_3166-1";

    public override async Task<Country[]> ListCountryAsync()
    {
        var list = new List<Country>();
        foreach (var tr in await ListTableRowsAsync(WikiUrl))
        {
            var tds = tr.Split("</td>").Select(o => o.Replace("<td>", "").Replace("<td align=\"center\">", "")).ToArray();

            var name = tds[0].Replace("\n", "");
            if (name.Contains("</span>"))
            {
                var array = name.Split("</span>");
                name = array[^2].Split(">")[1];
            }

            if (name.Contains(" ("))
            {
                var array = name.Split(" (");
                name = array[0];
                if (array.Length > 1)
                {
                    name = $"{name}, {array[1]}";
                }
            }

            list.Add(new Country()
            {
                Name = name,
                TwoLetterCode = tds[1].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                ThreeLetterCode = tds[2].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                NumericCode = tds[3].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                SimplifiedChineseName = tds[5].Split("<a href=").Last().Replace("</a>", "").Split(">")[1].Replace("\n", ""),
                Independent = tds[6].Contains("table-yes")
            });
        }

        return list.ToArray();
    }
}