using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IProcessedQueueRepository : IRepository<ProcessedQueue>
    {

    }

    public class ProcessedQueueRepository : Repository<ProcessedQueue, ApplicationDbContext>, IProcessedQueueRepository
    {
        public ProcessedQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
