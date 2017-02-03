using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Repository.Test
{
    internal class RepositoryProxy : Notes.Repository.Repository
    {
        internal RepositoryProxy() : base(nameof(RepositoryProxy))
        {}
    }
}
