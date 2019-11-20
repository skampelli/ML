using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.IO;
using System.Text;

namespace NewsFeedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            GetGoogleNewsFeed();
        }

        static void GetGoogleNewsFeed()
        {
            try
            {
                var newsapiclient = new NewsApiClient("c2e5293629af4a0d9e575e860a53c00e");
                var articleResponse = newsapiclient.GetEverything(new EverythingRequest
                {
                    SortBy = SortBys.PublishedAt,
                    PageSize = 100,
                    Language = Languages.EN,
                    Q = "wells fargo"
                });

                if (articleResponse.Status == Statuses.Ok)
                {
                    var sb = new StringBuilder();
                    foreach (var article in articleResponse.Articles)
                    {
                        var line = $"{article.PublishedAt}\t{article.Title}";
                        sb.AppendLine(line);
                    }

                    using (FileStream fs = File.Create("newsfeed.tsv"))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
