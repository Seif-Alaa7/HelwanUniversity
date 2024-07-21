using Models.Enums;

namespace ViewModels
{
    internal class DoctorVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public string? Picture { get; set; }
        public string? Address { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
