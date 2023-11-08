namespace NLBluem.ValueObjects
{
    public class BluemFile
    {
        public string MimeType { get; set; }

        public string FileType { get; set; }

        public string Filename { get; set; }

        public StringContent File { get; set; }
    }
}