using Models.Enums;

namespace ViewModels
{
    public class HighBoardVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public JobTitle JobTitle { get; set; }
        public string? Picture { get; set; }
    }
}
