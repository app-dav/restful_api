using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using Notes.Domain;

namespace Notes.Api
{
    internal class ServiceProxy
    {
        internal static INote GetNote(int Id)
        {
            var repo = GetNoteService();
            return repo.GetNote(Id);
        }

        internal static IEnumerable<INote> GetNotes()
        {
            var repo = GetNoteService();
            return repo.GetNotes();
        }

        internal static IEnumerable<INote> SearchNotes(string searchTerm)
        {
            var repo = GetNoteService();
            return repo.SearchNotes(searchTerm);
        }

        internal static void SaveNote(INote newNote)
        {
            var repo = GetNoteService();
            repo.SaveNote(newNote);
        }

        static INoteService GetNoteService()
        {
            return ServiceFactory.GetNoteService();
        }
    }
}