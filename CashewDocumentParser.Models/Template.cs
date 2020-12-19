using CashewDocumentParser.Models.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CashewDocumentParser.Models
{
    public class Template : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
