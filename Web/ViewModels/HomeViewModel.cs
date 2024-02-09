using Core.Models;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public Owner Owner { get; set; }
        public List<PortfolioItem> PortfolioItem { get; set; }
    }
}
