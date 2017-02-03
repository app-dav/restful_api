using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Notes.Interfaces;

namespace Notes.Repository
{
    //normally, you would prob wouldn't hard code a number of these values,
    //but in the lean tradition, I am for using the lowest fidelity for this application
    //unless otherwise instructed or required
    public class Repository : Notes.Interfaces.IRepository
    {        
        public Repository(string dbName = "Notes.Repository.DB")
        {
            db_name = dbName ;
        }

        string db_name ;

        #region Interface API
        
        public void Save(INote note)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<INote>(nameof(Notes.Repository));
                Save(note, notes);
            }
        }

        public INote Get(int id)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<INote>(nameof(Notes.Repository));
                return Get(id, notes).FirstOrDefault();
            }
        }

        public IEnumerable<INote> Get()
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<INote>(nameof(Notes.Repository));
                return Get(null,  notes);
            }
        }

        IEnumerable<INote> IRepository.Search(string searchTerm)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<INote>(nameof(Notes.Repository));
                return SearchNotes(searchTerm, notes);
            }
        }

        #endregion

        protected internal void Save(INote note, LiteCollection<INote> notes)
        {

            INote exists = note.id.HasValue ? notes.Find(n => n.id == note.id).FirstOrDefault() : null;

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

        protected internal int GetNextId(LiteCollection<INote> notes)
        {
            if (notes != null && notes.Count() > 0)
            {
                int maxId = notes.FindAll().Max(x => x.id.Value);
                return ++maxId;
            }
            else
                return 1;
        }


        protected internal IEnumerable<INote> Get(int? Id, LiteCollection<INote> notes)
        {
            return Id.HasValue ? notes.Find(n => n.id == Id.Value) :
                notes.FindAll();
        }

        protected internal IEnumerable<INote> SearchNotes(string searchTerm, LiteCollection<INote> notes)
        {
            return string.IsNullOrEmpty(searchTerm) ? new List<INote>() : notes.Find(n => n.body.Contains(searchTerm));
        }

    }
}
