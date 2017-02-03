using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Notes.Interfaces;

namespace Notes.Repository
{
    //normally, you would prob wouldn't hard code a number of these values,
    //but in the lean tradition, I am for using the lowest fidelity for this application
    //unless otherwise instructed or required
    public class Repository : Notes.Interfaces.IRepository
    {        
        protected internal Repository(string dbName = "Notes.Repository.DB")
        {
            db_name = dbName ;
        }

        public Repository()  {
            db_name = "Notes.Repository.DB";
        }

        string db_name ;

        #region Interface API
        
        public void Save(Interfaces.Note note)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Interfaces.Note>(nameof(Notes.Repository));
                Save(note, notes);
            }
        }

        public Note Get(int id)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Interfaces.Note>(nameof(Notes.Repository));
                return Get(id, notes).FirstOrDefault();
            }
        }

        public IEnumerable<Interfaces.Note> Get()
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Interfaces.Note>(nameof(Notes.Repository));
                return Get(null,  notes);
            }
        }

        IEnumerable<Interfaces.Note> IRepository.Search(string searchTerm)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Interfaces.Note>(nameof(Notes.Repository));
                return SearchNotes(searchTerm, notes);
            }
        }

        #endregion

        protected internal void Save(Interfaces.Note note, LiteCollection<Interfaces.Note> notes)
        {

            Interfaces.Note exists = note.id.HasValue ? notes.Find(n => n.id == note.id).FirstOrDefault() : null;

            if (exists != null)
            {
                exists.body = note.body;
                notes.Update(exists);
            }
            else
            {
                note.id = GetNextId(notes);
                notes.Insert(note);
            }

        }

        protected internal int GetNextId(LiteCollection<Interfaces.Note> notes)
        {
            if (notes != null && notes.Count() > 0)
            {
                int maxId = notes.FindAll().Max(x => x.id.Value);
                return ++maxId;
            }
            else
                return 1;
        }


        protected internal IEnumerable<Interfaces.Note> Get(int? Id, LiteCollection<Interfaces.Note> notes)
        {
            return Id.HasValue ? notes.Find(n => n.id == Id.Value) :
                notes.FindAll();
        }

        protected internal IEnumerable<Interfaces.Note> SearchNotes(string searchTerm, LiteCollection<Interfaces.Note> notes)
        {
            return string.IsNullOrEmpty(searchTerm.Trim()) ? new List<Interfaces.Note>() : notes.Find(n => n.body.Contains(searchTerm));
        }

    }
}
