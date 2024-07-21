using Models.Enums;


namespace ViewModels
{
    internal class UniFileVM
    {
        public int Id { get; set; }
        public string File { get; set; } = null!;
        public Filetype ContentType { get; set; }
    }
}
