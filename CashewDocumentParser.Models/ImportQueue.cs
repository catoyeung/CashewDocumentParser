using CashewDocumentParser.Enumerations;
using CashewDocumentParser.Models.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashewDocumentParser.Models
{
    public class ImportQueue : IEntity
    {
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public int TemplateId { get; set; }
        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        [Required]
        public string FilenameWithoutExtension { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string Fullpath { get; set; }
        [Required]
        public EnumProcessStage ProcessStage { get; set; }
    }
}
