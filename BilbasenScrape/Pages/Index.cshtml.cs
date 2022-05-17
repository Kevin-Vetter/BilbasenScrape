using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


            ElementsFromPage page1 = GetElementsFromPage(URL_1, xPaths);
            ElementsFromPage page2 = GetElementsFromPage(URL_2, xPaths);

        }

        static ElementsFromPage GetElementsFromPage(string url, List<string> xPaths)
        {
            List<string> udstyr = new List<string>();

            HtmlNode? html = GetHtml(url);
            ElementsFromPage? pageDetails = new ElementsFromPage(
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[0]).InnerText, //Title
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[1]).InnerText, //
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[2]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[3]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[4]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[5]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[6]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[7]).InnerText,
            html.OwnerDocument.DocumentNode.SelectSingleNode(xPaths[8]).InnerText,
            udstyr
                );


            IEnumerable<HtmlNode>? udstyrLi = html.CssSelect("li");

            foreach (HtmlNode? li in udstyrLi)
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
            /// <summary>
            /// Contains all elements extraced from an html page
            /// </summary>
            /// <param name="title"></param>
            /// <param name="pris"></param>
            /// <param name="km"></param>
            /// <param name="brændstofType"></param>
            /// <param name="kmPrL"></param>
            /// <param name="v3"></param>
            /// <param name="regDato"></param>
            /// <param name="modelAar"></param>
            /// <param name="beskrivelse"></param>
            /// <param name="udstyr"></param>
            public ElementsFromPage(string title, string pris, string km, string brændstofType, string kmPrL, string v3, string regDato, string modelAar, string beskrivelse, List<string> udstyr)
            {
                Title = title;
                Pris = pris;
                Km = km;
                BrændstofType = brændstofType;
                KmPrL = kmPrL;
                this.v3 = v3;
                RegDato = regDato;
                ModelAar = modelAar;
                Beskrivelse = beskrivelse;
                Udstyr = udstyr;
            }

            public string Title { get; set; }
            public string Pris { get; set; }
            public string Km { get; set; }
            public string BrændstofType { get; set; }
            public string KmPrL { get; set; }
            public string v3 { get; set; }
            public string RegDato { get; set; }
            public string ModelAar { get; set; }
            public string Beskrivelse { get; set; }
            public List<string> Udstyr { get; set; }
        }
    }
}