using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using TungstenNetworkTask.Models;

namespace TungstenNetworkTask.Controllers
{
    public class ReadXMLController : Controller
    {
        public IActionResult ReadXml()
        {
            XmlDocument xmlDocOnline = new XmlDocument();
            xmlDocOnline.Load("https://www.tungsten-network.com/rss-resource-library/");

            XmlNodeList nodeList = xmlDocOnline
                .DocumentElement
                .SelectNodes("//item/category[contains(.,'Friction')]/..");

            List<XmlNode> xmlNodesList = new List<XmlNode>();
            ArticleCollection articleCollection = new ArticleCollection();
            articleCollection.Collection = new List<Article>();

            foreach (XmlNode node in nodeList)
            {
                var heading = node["title"].InnerText;
                var date = node["pubDate"].InnerText;
                var enclosure = node["enclosure"].Attributes["url"].Value;
                var category = node["category"].InnerText;
                var article = new Article() { Heading = heading, Date = date, ImageUrl = enclosure, Category = category };
                articleCollection.Collection.Add(article);
            }

            return View(articleCollection);
        }

    }
}