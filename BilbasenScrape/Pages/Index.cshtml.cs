using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace BilbasenScrape.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string URL_1 { get; set; }
        [BindProperty]
        public string URL_2 { get; set; }

        static ScrapingBrowser _scrapingBrowser = new ScrapingBrowser();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            List<string> xPaths = new List<string>();
            xPaths.Add("//*[@id=\"bbVipTitle\"]");
            xPaths.Add("//*[@id=\"bbVipPricePrice\"]/span[2]");
            xPaths.Add("//*[@id=\"bbVipMileage\"]/p/span[2]");
            xPaths.Add("//*[@id=\"bbVipUsage\"]/ p/span/span[1]");
            xPaths.Add("//*[@id=\"bbVipUsage\"]/ p/span/span[2]");
            xPaths.Add("//*[@id=\"bbVipYearInfo\"]/ div[1]/span[1]");
            xPaths.Add("//*[@id=\"bbVipYearInfo\"]/ div[1]/span[2]");
            xPaths.Add("//*[@id=\"bbVipYearInfo\"]/ div[3]/span[2]");
            xPaths.Add("//*[@id=\"bbVipDescription\"]");
            xPaths.Add("//*[@id=\"bannerwrapper\"]/ div/div[1]/div[1]/div[2]/table/tbody/tr[2]/td[3]");
            xPaths.Add("//*[@id=\"bannerwrapper\"]/div/div[1]/div[1]/div[3]/table/tbody/tr[2]/td[3]");
            xPaths.Add("//*[@id=\"bbVipEquipment\"]/ul/li");


            



            GetElementsFromPage(URL_1, xPaths);
            GetElementsFromPage(URL_2, xPaths);


        }

        static ElementsFromPage GetElementsFromPage(string url, List<string> xPaths)
        {
            var pageDetails = new ElementsFromPage();
            List<string> udstyr = new List<string>();


            var html = GetHtml(url);
            pageDetails.Title = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[0]).InnerText;
            pageDetails.Pris = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[1]).InnerText;
            pageDetails.Km = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[2]).InnerText;
            pageDetails.BrændstofType = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[3]).InnerText;
            pageDetails.KmPrL = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[4]).InnerText;
            pageDetails.v3 = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[5]).InnerText;
            pageDetails.RegDato = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[6]).InnerText;
            pageDetails.ModelAar = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[7]).InnerText;
            pageDetails.Beskrivelse = html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[8]).InnerText;
            pageDetails.Udstyr = udstyr;



            var udstyrLi = html.CssSelect("li");

            foreach (var li in udstyrLi)
            {
                if (li.ParentNode.Attributes["class"] != null)
                {
                    if (li.ParentNode.Attributes["class"].Value.Contains("last"))
                    {
                        udstyr.Add(li.InnerText);
                    }
                }
            }

            return pageDetails;
        }

        static HtmlNode GetHtml(string url)
        {
            WebPage web = _scrapingBrowser.NavigateToPage(new Uri(url));
            return web.Html;
        }

        public class ElementsFromPage
        {
            public string Title { get; set; }
            public string Pris { get; set; }
            public string Km { get; set; }
            public string BrændstofType { get; set; }
            public string KmPrL { get; set; }
            public string v3 { get; set; }
            public string RegDato { get; set; }
            public string ModelAar { get; set; }
            public string v6 { get; set; }
            public string Beskrivelse { get; set; }
            public List<string> Udstyr { get; set; }
        }
    }
}