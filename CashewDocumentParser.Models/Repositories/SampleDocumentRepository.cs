using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public class SampleDocumentRepository : Repository<SampleDocument, ApplicationDbContext>
    {
        public SampleDocumentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
