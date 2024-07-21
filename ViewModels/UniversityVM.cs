namespace ViewModels
{
    public class UniversityVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string FacebookPage { get; set; } = null!;
        public string LinkedInPage { get; set; } = null!;
        public string MainPage { get; set; } = null!;
        public int ViewCount { get; set; }
    }
}
