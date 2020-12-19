using CashewDocumentParser.Enumerations;
using CashewDocumentParser.Models.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashewDocumentParser.Models
{
    public class Rule : IEntity
    {
        public int Id { get; set; }
        [Required]
        public int TemplateId { get; set; }
        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        [Required]
        public EnumRuleType RuleType { get; set; }
        public Point FixedPositionPoint1 { get; set; }
        public Point FixedPositionPoint2 { get; set; }
        public Point FixedPositionPoint3 { get; set; }
        public Point FixedPositionPoint4 { get; set; }
        public Point VariablePositionPoint1 { get; set; }
        public Point VariablePositionPoint2 { get; set; }
        public Point VariablePositionPoint3 { get; set; }
        public Point VariablePositionPoint4 { get; set; }
        public Point VariablePositionPoint5 { get; set; }
        public Point VariablePositionPoint6 { get; set; }
        public Point VariablePositionPoint7 { get; set; }
        public Point VariablePositionPoint8 { get; set; }
    }
}
