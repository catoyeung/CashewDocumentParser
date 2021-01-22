using CashewDocumentParser.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashewDocumentParser.Models.Repositories
{
    public interface IScriptingQueueRepository : IRepository<ScriptingQueue>
    {

    }

    public class ScriptingQueueRepository : Repository<ScriptingQueue, ApplicationDbContext>, IScriptingQueueRepository
    {
        public ScriptingQueueRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
