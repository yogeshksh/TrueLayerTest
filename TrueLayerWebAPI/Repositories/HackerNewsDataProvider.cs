using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrueLayerWebAPI.Models;

namespace TrueLayerWebAPI.Repositories
{
    public class HackerNewsDataProvider
    {
        static string urlToLoad = "https://news.ycombinator.com/news?p={0}";
      //  static int MaxCount = 100;
      //Assumption based on current website posts, max posts are 30 per page
        const int MaxPostPerPage = 30;

        /// <summary>
        /// return the data from the Hacker news site
        /// </summary>
        /// <param name="numberofposts"></param>
        /// <returns></returns>
        public static List<Post> GetData(int numberofposts)
        {
           // if (numberofposts == 0) { numberofposts = MaxCount; }
            List<Post> posts = new List<Post>();
            int numberofpages = (numberofposts / MaxPostPerPage) + 1;

            //Executing number of pages requests in parallel for better processing speed
            Parallel.For(1, numberofpages+1, pageIndex =>
            {
                HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.OptionFixNestedTags = true;

                HttpWebRequest request = HttpWebRequest.Create(string.Format(urlToLoad, pageIndex)) as HttpWebRequest;
                request.Method = "GET";

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");

                Console.WriteLine(request.RequestUri.AbsoluteUri);
                WebResponse response = request.GetResponse();

                htmlDoc.Load(response.GetResponseStream(), true);
                response?.Dispose();

                if (htmlDoc.DocumentNode != null)
                {
                    var articleNodes = htmlDoc.DocumentNode.SelectNodes("/html/body/center/table/tr[3]/td/table/tr");

                    if (articleNodes.Count > 2)
                    {

                        for (int i = 0; i < (articleNodes.Count - 3); i += 3)
                        {
                            var item1 = articleNodes[i + 0];
                            var rank = item1?.ChildNodes[1]?.InnerText.Trim().Replace(".", "");
                            var title = item1?.ChildNodes[4]?.InnerText.Trim();
                            var uridata = item1?.ChildNodes[4]?.InnerHtml;
                            string uri = string.Empty;

                            if (uridata.IndexOf("href=") > 0 && uridata.Length > 6)
                            {
                                if (uridata.IndexOf("class=") > 10)
                                {
                                    uri = uridata.Substring(uridata.IndexOf("href=") + 6, uridata.IndexOf("class=") - 11);
                                }
                                else
                                {
                                    uri = uridata.Substring(uridata.IndexOf("href=") + 6);
                                }
                            }

                            var pointsauthorcomments = articleNodes[i + 1].InnerText.Trim();
                            int points = 0;
                            if (pointsauthorcomments.IndexOf("points") > 0)
                            {
                                var pointsdata = pointsauthorcomments.Substring(0, pointsauthorcomments.IndexOf("points")).Trim();
                                int.TryParse(pointsdata, out points);
                            }

                            var authorcommentsdata = pointsauthorcomments.Substring(pointsauthorcomments.IndexOf("points by ") + 10);
                            string author = authorcommentsdata;
                            if (authorcommentsdata.IndexOf(" ") > 0)
                            {
                                author = authorcommentsdata.Substring(0, authorcommentsdata.IndexOf(" ")).Trim();
                            }

                            int comments = 0;
                            if (authorcommentsdata.IndexOf("comments") > 0)
                            {
                                authorcommentsdata = authorcommentsdata.Substring(authorcommentsdata.IndexOf("hide | ") + 7);
                                string[] numbers = Regex.Split(authorcommentsdata, @"\D+");
                                int.TryParse(numbers[0], out comments);
                            }
                            var post = new Post(title, author, uri, points, comments, Convert.ToInt32(rank));
                            posts.Add(post);
                        }

                    }
                }
            });

            return posts.Take(numberofposts).ToList();
        }

    }
}
