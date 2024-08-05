namespace ViewModels
{
    public class BifurcationRequestVM
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public List<int> DepartmentIds { get; set; } = new List<int>();
    }
}
