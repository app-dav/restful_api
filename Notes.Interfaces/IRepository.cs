using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces
{
    public interface IRepository
    {
        void Save(INote note);
        INote Get(int id);
        IEnumerable<INote> Get();
        IEnumerable<INote> Search(string searchTerm);
    }
}
