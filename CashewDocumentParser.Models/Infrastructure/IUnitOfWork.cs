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
        IClassificationQueueRepository GetClassificationQueueRepository();
        IExtractQueueRepository GetExtractQueueRepository();
        IImportQueueRepository GetImportQueueRepository();
        IIntegrationQueueRepository GetIntegrationQueueRepository();
        IOCRQueueRepository GetOCRQueueRepository();
        IPreprocessingQueueRepository GetPreprocessingQueueRepository();
        IProcessedQueueRepository GetProcessedQueueRepository();
        IScriptingQueueRepository GetScriptingQueueRepository();
        Task Commit();
        Task Rollback();
    }
}
