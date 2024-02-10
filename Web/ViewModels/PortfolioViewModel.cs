namespace Web.ViewModels
{
    public class PortfolioViewModel
    {
        public Guid id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
    }
}
