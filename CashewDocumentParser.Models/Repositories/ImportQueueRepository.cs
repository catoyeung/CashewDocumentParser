using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IImportQueueRepository : IRepository<ImportQueue>
    {

    }

    public class ImportQueueRepository : Repository<ImportQueue, ApplicationDbContext>, IImportQueueRepository
    {
        public ImportQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
