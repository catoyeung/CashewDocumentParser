using CashewDocumentParser.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IRepository<Template> _templateRepository;
        private IRepository<SampleDocument> _sampleDocumentRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext; 
        }

        public IRepository<Template> TemplateRepository
        {
            get { return _templateRepository = _templateRepository ?? new TemplateRepository(_applicationDbContext); }
        }

        public IRepository<SampleDocument> SampleDocumentRepository
        {
            get { return _sampleDocumentRepository = _sampleDocumentRepository ?? new SampleDocumentRepository(_applicationDbContext); }
        }

        public async Task Commit()
        { 
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Rollback()
        { 
            await _applicationDbContext.DisposeAsync(); 
        }
    }
}
