using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BilbasenScrape.Pages.IndexModel;

namespace BilbasenScrape.Pages
{
    public class SamligningModel : PageModel
    {
        public ElementsFromPage Page1 { get; set; }
        
        
        public void OnGet(ElementsFromPage page1)
        {
            Page1 = page1;    
        }
    }
}
