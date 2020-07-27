using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashewDocumentParser.Models
{
    public class SampleDocument : IEntity
    {
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public int TemplateId { get; set; }
        [Required]
        public string FilenameWithoutExtension { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string Fullpath { get; set; }
    }
}
