using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IIntegrationQueueRepository : IRepository<IntegrationQueue>
    {

    }

    public class IntegrationQueueRepository : Repository<IntegrationQueue, ApplicationDbContext>, IIntegrationQueueRepository
    {
        public IntegrationQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
