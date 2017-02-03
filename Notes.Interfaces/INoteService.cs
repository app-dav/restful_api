using System.Collections.Generic;

namespace Notes.Interfaces
{
    public interface INoteService
    {
        INote GetNote(int Id);

        IEnumerable<INote> GetNotes();


        IEnumerable<INote> SearchNotes(string searchTerm);


        void SaveNote(INote newNote);
        
    }
}
