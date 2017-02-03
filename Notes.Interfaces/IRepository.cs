using System.Collections.Generic;

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
