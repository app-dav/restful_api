using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using Notes.Domain;

namespace Notes.Api
{
    internal class ServiceProxy
    {
        internal static Note GetNote(int Id)
        {
            var repo = GetNoteService();
            return repo.GetNote(Id);
        }

        internal static IEnumerable<Note> GetNotes()
        {
            var repo = GetNoteService();
            return repo.GetNotes();
        }

        internal static IEnumerable<Note> SearchNotes(string searchTerm)
        {
            var repo = GetNoteService();
            return repo.SearchNotes(searchTerm);
        }

        internal static void SaveNote(Note newNote)
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