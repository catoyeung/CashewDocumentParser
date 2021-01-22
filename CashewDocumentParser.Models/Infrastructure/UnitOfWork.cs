using CashewDocumentParser.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public readonly ITemplateRepository TemplateRepository;
        public readonly IClassificationQueueRepository ClassificationQueueRepository;
        public readonly IExtractQueueRepository ExtractQueueRepository;
        public readonly IImportQueueRepository ImportQueueRepository;
        public readonly IIntegrationQueueRepository IntegrationQueueRepository;
        public readonly IOCRQueueRepository OCRQueueRepository;
        public readonly IPreprocessingQueueRepository PreprocessingRepository;
        public readonly IProcessedQueueRepository ProcessedQueueRepository;
        public readonly IScriptingQueueRepository ScriptingQueueRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext,
            ITemplateRepository templateRepository,
            IClassificationQueueRepository classificationQueueRepository,
            IExtractQueueRepository extractQueueRepository,
            IImportQueueRepository importQueueRepository,
            IIntegrationQueueRepository integrationQueueRepository,
            IOCRQueueRepository ocrQueueRepository,
            IPreprocessingQueueRepository preprocessingRepository,
            IProcessedQueueRepository processedQueueRepository,
            IScriptingQueueRepository scriptingQueueRepository
            )
        {
            ApplicationDbContext = applicationDbContext;
            TemplateRepository = templateRepository;
            ClassificationQueueRepository = classificationQueueRepository;
            ExtractQueueRepository = extractQueueRepository;
            ImportQueueRepository = importQueueRepository;
            IntegrationQueueRepository = integrationQueueRepository;
            OCRQueueRepository = ocrQueueRepository;
            PreprocessingRepository = preprocessingRepository;
            ProcessedQueueRepository = processedQueueRepository;
            ScriptingQueueRepository = scriptingQueueRepository;
        }

        public ITemplateRepository GetTemplateRepository()
        {
            return TemplateRepository;
        }
        public IClassificationQueueRepository GetClassificationQueueRepository()
        {
            return ClassificationQueueRepository;
        }
        public IExtractQueueRepository GetExtractQueueRepository()
        {
            return ExtractQueueRepository;
        }
        public IImportQueueRepository GetImportQueueRepository()
        {
            return ImportQueueRepository;
        }
        public IIntegrationQueueRepository GetIntegrationQueueRepository()
        {
            return IntegrationQueueRepository;
        }
        public IOCRQueueRepository GetOCRQueueRepository()
        {
            return OCRQueueRepository;
        }
        public IPreprocessingQueueRepository GetPreprocessingQueueRepository()
        {
            return PreprocessingRepository;
        }
        public IProcessedQueueRepository GetProcessedQueueRepository()
        {
            return ProcessedQueueRepository;
        }
        public IScriptingQueueRepository GetScriptingQueueRepository()
        {
            return ScriptingQueueRepository;
        }
        public async Task Commit()
        { 
            await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task Rollback()
        { 
            await ApplicationDbContext.DisposeAsync(); 
        }
    }
}
