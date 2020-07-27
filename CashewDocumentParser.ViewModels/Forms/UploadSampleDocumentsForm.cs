using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.ViewModels.Forms
{
    public class UploadSampleDocumentsForm
    {
        public int TemplateId { get; set; }
        public List<IFormFile> SampleDocuments { get; set; }
    }
}
