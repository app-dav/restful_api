using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notes.Repository
{
    /// <summary>
    /// Normally, this would be in an assembly of it's own (aka a business layer),
    /// but an app this small, and a layer this light,
    /// it would be a bit of overkill, I think
    /// </summary>
    public class NoteService
    {
        public Note GetNote(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> GetNotes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> SearchNotes(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void SaveNote(Note newNote)
        {
            throw new NotImplementedException();
        }

    }
}
