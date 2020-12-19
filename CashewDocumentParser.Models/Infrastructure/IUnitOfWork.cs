using CashewDocumentParser.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public interface IUnitOfWork
    {
        ITemplateRepository GetTemplateRepository();
        IExtractQueueRepository GetExtractQueueRepository();
        IImportQueueRepository GetImportQueueRepository();
        IIntegrationQueueRepository GetIntegrationQueueRepository();
        IPreprocessingQueueRepository GetPreprocessingRepository();
        IProcessedQueueRepository GetProcessedQueueRepository();
        Task Commit();
        Task Rollback();
    }
}
