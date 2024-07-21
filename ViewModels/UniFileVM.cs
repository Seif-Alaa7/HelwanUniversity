using Models.Enums;


namespace ViewModels
{
    public class UniFileVM
    {
        public int Id { get; set; }
        public string File { get; set; } = null!;
        public Filetype ContentType { get; set; }
    }
}
