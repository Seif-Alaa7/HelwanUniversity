namespace ViewModels
{
    public class FacultyVM
    {
        public int Id { get; set; }
        public int DeanId { get; set; }
        public string Name { get; set; } = null!;
        public string? Logo { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public int ViewCount { get; set; }
    }
}
