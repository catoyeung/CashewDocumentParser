using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface ITemplateRepository : IRepository<Template>
    {

    }
    public class TemplateRepository : Repository<Template, ApplicationDbContext>, ITemplateRepository
    {
        public TemplateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
