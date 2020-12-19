namespace CashewDocumentParser.ViewModels
{
    public class ExtractQueueViewModel
    {
        public int Id { get; set; }
        public int Guid { get; set; }
        public int TemplateId { get; set; }
        public TemplateViewModel Template { get; set; }
        public string Extension { get; set; }
        public string Fullpath { get; set; }
    }
}
