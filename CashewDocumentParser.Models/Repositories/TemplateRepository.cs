using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public class TemplateRepository : Repository<Template, ApplicationDbContext>
    {
        public TemplateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
