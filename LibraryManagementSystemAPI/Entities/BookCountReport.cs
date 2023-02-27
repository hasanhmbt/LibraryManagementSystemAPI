namespace LibraryManagementSystemAPI.Entities
{
    public class BookCountReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category   { get; set; }
        public int CountOnUser { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }  


    }
}
