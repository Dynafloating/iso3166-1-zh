# ISO3166-1-ZH
Country list contains code, names and chinese name (both traditional and simplified). Data retrieved from following Wiki page:
- https://zh.wikipedia.org/zh-tw/ISO_3166-1
- https://zh.wikipedia.org/zh-cn/ISO_3166-1


包含代碼、名稱與中文名稱（正體與簡體）的國家清單。資料來自下列 Wiki 頁面：
- https://zh.wikipedia.org/zh-tw/ISO_3166-1
- https://zh.wikipedia.org/zh-cn/ISO_3166-1


## Install package via NuGet (使用 NuGet 安裝此套件)
You can install this package from [NuGet Page](https://www.nuget.org/packages/ISO3166-1-ZH/) or use following command:
```
PM> Install-Package ISO3166-1-ZH
```


您可以透過 [NuGet](https://www.nuget.org/packages/ISO3166-1-ZH/) 下載此套件，或使用下列指令：
```
PM> Install-Package ISO3166-1-ZH
```


## Get the lastest data by WebCrawler (使用 WebCrawler 取得最新資料)
Use WebCrawler to retrieve this lastest country data from Wiki page, and generate iso3166.json and Country.cs.
1. [Download](https://github.com/Dynafloating/iso3166-1-zh/releases/tag/v1.0.1) WebCrawler.
2. Run WebCrawler, an output path (folder) is needed to store generated files; if provided empty, it will store at same folder contains the application.
3. Two files will be generate: iso3166.json and Country.cs.


使用專案內的 WebCrawler 從 Wiki 頁面上取得最新的國家資料，將產出 iso3166.json 和 Country.cs 兩個檔案。
1. [下載](https://github.com/Dynafloating/iso3166-1-zh/releases/tag/v1.0.1) WebCrawler。
2. 執行 WebCrawler，畫面將會詢問要存放產出檔案的資料夾路徑；如果沒有提供，將會產出在執行檔的相同資料夾。
3. 兩個檔案會被產出：iso3166.json 與 Country.cs。
