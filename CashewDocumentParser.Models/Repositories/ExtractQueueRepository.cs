using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IExtractQueueRepository : IRepository<ExtractQueue>
    {

    }
    public class ExtractQueueRepository : Repository<ExtractQueue, ApplicationDbContext>, IExtractQueueRepository
    {
        public ExtractQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
