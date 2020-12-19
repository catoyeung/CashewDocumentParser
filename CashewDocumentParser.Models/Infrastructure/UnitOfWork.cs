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
        public readonly IExtractQueueRepository ExtractQueueRepository;
        public readonly IImportQueueRepository ImportQueueRepository;
        public readonly IIntegrationQueueRepository IntegrationQueueRepository;
        public readonly IPreprocessingQueueRepository PreprocessingRepository;
        public readonly IProcessedQueueRepository ProcessedQueueRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext,
            ITemplateRepository templateRepository,
            IExtractQueueRepository extractQueueRepository,
            IImportQueueRepository importQueueRepository,
            IIntegrationQueueRepository integrationQueueRepository,
            IPreprocessingQueueRepository preprocessingRepository,
            IProcessedQueueRepository processedQueueRepository
            )
        {
            ApplicationDbContext = applicationDbContext;
            TemplateRepository = templateRepository;
            ExtractQueueRepository = extractQueueRepository;
            ImportQueueRepository = importQueueRepository;
            IntegrationQueueRepository = integrationQueueRepository;
            PreprocessingRepository = preprocessingRepository;
            ProcessedQueueRepository = processedQueueRepository;
        }

        public ITemplateRepository GetTemplateRepository()
        {
            return TemplateRepository;
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
        public IPreprocessingQueueRepository GetPreprocessingRepository()
        {
            return PreprocessingRepository;
        }
        public IProcessedQueueRepository GetProcessedQueueRepository()
        {
            return ProcessedQueueRepository;
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
