using CashewDocumentParser.Enumerations;
using CashewDocumentParser.Models.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashewDocumentParser.Models
{
    public class Point : IEntity
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
