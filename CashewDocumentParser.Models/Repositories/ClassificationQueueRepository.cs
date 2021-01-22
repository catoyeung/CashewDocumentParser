using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IClassificationQueueRepository : IRepository<ClassificationQueue>
    {

    }

    public class ClassificationQueueRepository : Repository<ClassificationQueue, ApplicationDbContext>, IClassificationQueueRepository
    {
        public ClassificationQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
