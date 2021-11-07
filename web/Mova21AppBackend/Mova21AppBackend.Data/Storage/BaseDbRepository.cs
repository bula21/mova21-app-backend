using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova21AppBackend.Data.Storage
{
    public abstract class BaseDbRepository
    {
        public Mova21AppContext Context { get; }

        protected BaseDbRepository(Mova21AppContext context)
        {
            Context = context;
        }
    }
}
