using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IPreprocessingQueueRepository : IRepository<PreprocessingQueue>
    {

    }

    public class PreprocessingQueueRepository : Repository<PreprocessingQueue, ApplicationDbContext>, IPreprocessingQueueRepository
    {
        public PreprocessingQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
