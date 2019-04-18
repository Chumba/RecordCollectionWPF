namespace RecordLibrary.Models
{
    /// <summary>
    /// Data model of a physical music entry into our collection
    /// </summary>
    public class Record
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string ReleaseName { get; set; }
        public string ReleaseYear { get; set; }
    }
}