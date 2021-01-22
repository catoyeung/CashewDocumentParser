using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IOCRQueueRepository : IRepository<OCRQueue>
    {

    }

    public class OCRQueueRepository : Repository<OCRQueue, ApplicationDbContext>, IOCRQueueRepository
    {
        public OCRQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
