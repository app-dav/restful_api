using System.Collections.Generic;

namespace Notes.Interfaces
{
    public interface IRepository
    {
        void Save(Note note);
        Note Get(int id);
        IEnumerable<Note> Get();
        IEnumerable<Note> Search(string searchTerm);
    }
}
