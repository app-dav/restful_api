using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using System.Threading.Tasks;


namespace Notes.Domain
{
    /// <summary>
    /// 
    /// This layer is overkill in this app, isn't it
    ///                                     
    /// </summary>
    public class NoteService : INoteService
    {
        public Note GetNote(int Id)
        {
            var repo = GetRepository();
            return repo.Get(Id);
        }

        public IEnumerable<Note> GetNotes()
        {
            var repo = GetRepository();
            return repo.Get();
        }

        public IEnumerable<Note> SearchNotes(string searchTerm)
        {
            var repo = GetRepository();
            return repo.Search(searchTerm);
        }

        public void SaveNote(Note newNote)
        {
            var repo = GetRepository();
            repo.Save(newNote);
        }

        IRepository GetRepository()
        {
            return Repository.RepositoryFactory.GetRepository();
        }

    }
}
