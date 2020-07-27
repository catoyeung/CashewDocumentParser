using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Template> TemplateRepository { get; }
        IRepository<SampleDocument> SampleDocumentRepository { get; }
        Task Commit();
        Task Rollback();
    }
}
