using System.Collections.Generic;

namespace Notes.Interfaces
{
    public interface INoteService
    {
        Note GetNote(int Id);

        IEnumerable<Note> GetNotes();


        IEnumerable<Note> SearchNotes(string searchTerm);


        void SaveNote(Note newNote);
        
    }
}
