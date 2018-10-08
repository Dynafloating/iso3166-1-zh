using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace WebCrawler
{
    class Program
    {
        static readonly string Wiki_TW = "https://zh.wikipedia.org/zh-tw/ISO_3166-1";
        static readonly string Wiki_CN = "https://zh.wikipedia.org/zh-cn/ISO_3166-1";
        static List<Model> Models { get; set; } = new List<Model>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ISO3166-1 web crawler!");
            Console.WriteLine("this console application will help you retrieve data from WIKI page");
            Console.WriteLine("and convert it to .cs file and JSON format.");
            Console.WriteLine("Generated files will save to (please provide folder path): ");
            var path = Console.ReadLine();
            var fullPath = Path.GetFullPath(!string.IsNullOrEmpty(path) ? path : ".");
            if (!Directory.Exists(fullPath))
            {
                Console.WriteLine("Invalid path, directory is not exists!");
                return;
            }

            Console.WriteLine("Retrieving...");
            ProcessTW();
            ProcessCN();

            Console.WriteLine("Generating files...");
            GenerateFile(fullPath);

            Console.WriteLine("Press any key to end");
            Console.ReadLine();
        }

        static void ProcessTW()
        {
            var webClient = new WebClient();
            var webData = webClient.DownloadString(Wiki_TW);
            var tbody = webData.Split("<table class=\"wikitable sortable\">")[1]
                .Split("<dl><dt>說明：</dt></dl>")[0].Replace("<tbody>", "").Replace("</tbody></table>", "");
            var trs = tbody.Split("<tr>")
                .Select(o => o.Replace("</tr>", ""))
                .Where(o => o != "\n\n" && !o.StartsWith("\n<th scope=\"col\">"));
            foreach (var tr in trs)
            {
                var tds = tr.Split("</td>").Select(o => o.Replace("<td>", "").Replace("<td align=\"center\">", "")).ToList();

                var name = tds[0].Replace("\n", "");
                if (name.Contains("</span>"))
                {
                    var array = name.Split("</span>");
                    name = array[array.Length - 2].Split(">")[1];
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

                if (name.StartsWith("Taiwan"))
                {
                    name = name.Split(",")[0];
                    tds[5] = tds[5].Replace("中國台灣省", "台灣");
                }

                Models.Add(new Model()
                {
                    Name = name,
                    TwoLetterCode = tds[1].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                    ThreeLetterCode = tds[2].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                    NumericCode = tds[3].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", ""),
                    TraditionalChineseName = tds[5].Split("<a href=").Last().Replace("</a>", "").Split(">")[1].Replace("\n", ""),
                    Independent = tds[6].Contains("table-yes")
                });
            }
        }

        static void ProcessCN()
        {
            var webClient = new WebClient();
            var webData = webClient.DownloadString(Wiki_CN);
            var tbody = webData.Split("<table class=\"wikitable sortable\">")[1]
                .Split("<dl><dt>说明：</dt></dl>")[0].Replace("<tbody>", "").Replace("</tbody></table>", "");
            var trs = tbody.Split("<tr>")
                .Select(o => o.Replace("</tr>", ""))
                .Where(o => o != "\n\n" && !o.StartsWith("\n<th scope=\"col\">"));
            foreach (var tr in trs)
            {
                var tds = tr.Split("</td>").Select(o => o.Replace("<td>", "").Replace("<td align=\"center\">", "")).ToList();
                var twoLetterCode = tds[1].Split("<tt>")[1].Split("</tt>")[0].Replace("\n", "");
                var model = Models.Where(o => o.TwoLetterCode == twoLetterCode).Single();
                model.SimplifiedChineseName = tds[5].Split("<a href=").Last().Replace("</a>", "").Split(">")[1].Replace("\n", "");
            }
        }

        static void GenerateFile(string fullPath)
        {
            var jsonPath = Path.Combine(fullPath, "iso3166.json");
            var jsonString = JsonConvert.SerializeObject(new { list = Models }, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            File.WriteAllText(jsonPath, jsonString);

            var csPath = Path.Combine(fullPath, "Country.cs");
            var notes = $"/*\r\n * Generated by WebCrawler at {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}\r\n" +
                $" * Data source: {Wiki_TW} and {Wiki_CN}\r\n * License: https://creativecommons.org/licenses/by-sa/3.0/ \r\n */\r\n";
            var countryModelTexts = Models
                .Select(o => $"new CountryModel(\"{o.Name}\", \"{o.TwoLetterCode}\", \"{o.ThreeLetterCode}\", " +
                    $"\"{o.NumericCode}\", \"{o.TraditionalChineseName}\", \"{o.SimplifiedChineseName}\", {(o.Independent ? "true" : "false")})");
            var csString = notes + 
                "using System.Collections.Generic;\r\nnamespace ISO3166\r\n{\r\n" +
                "    public class Country\r\n    {\r\n        public static List<CountryModel> List = new List<CountryModel>()\r\n        {\r\n            " +
                string.Join(",\r\n            ", countryModelTexts) +
                "\r\n        };\r\n    }\r\n}";
            File.WriteAllText(csPath, csString);

            Console.WriteLine("Following files generated:");
            Console.WriteLine(jsonPath);
            Console.WriteLine(csPath);
        }
    }

    class Model
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public string NumericCode { get; set; }
        public string TraditionalChineseName { get; set; }
        public string SimplifiedChineseName { get; set; }
        public bool Independent { get; set; }
    }
}
