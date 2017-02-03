using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using System.Threading.Tasks;


namespace Notes.Domain
{
    /// <summary>
    /// Normally, this would be in an assembly of it's own (aka a business layer),
    /// but an app this small, and a layer this light,
    /// it would be a bit of overkill, I think
    /// </summary>
    public class NoteService
    {
        public INote GetNote(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INote> GetNotes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INote> SearchNotes(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void SaveNote(INote newNote)
        {
            throw new NotImplementedException();
        }

    }
}
