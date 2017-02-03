using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Notes.Repository
{
    //normally, you would prob wouldn't hard code a number of these values,
    //but in the lean tradition, I am for using the lowest fidelity for this application
    //unless otherwise instructed or required
    internal class Repository
    {
        protected internal Repository(string dbName)
        {
            dbName = !string.IsNullOrEmpty(dbName) ? dbName : "Notes.Repository.DB";
        }

        string db_name;
        Delegate<T> del(T item, LiteCollection<Note> col); 

        protected internal void Save(Note note)
        {
            Execute<object>(delegate (Note noteParam, LiteCollection<Note> notes)
         {
             Note exists = note.id.HasValue ? notes.Find(n => n.id == noteParam.id).FirstOrDefault() : null;

             if (exists != null)
             {
                 exists.body = note.body;
                 notes.Update(exists);
             }
             else
             {
                 note.id = GetNextId(noteParam);
                 notes.Insert(note);
             }
         });
            
        }

        protected internal int GetNextId(LiteCollection<Note> notes)
        {
            int maxId = notes.FindAll().Max(x => x.id.Value);
            return ++maxId;
        }

        internal T Execute<T>( del)
        {

        }

        protected internal IEnumerable<Note> Get(int? Id)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Note>(nameof(Notes.Repository));
                return Id.HasValue ? notes.Find(n => n.id.Value == Id) :
                    notes.FindAll();
            }
        }

        protected internal IEnumerable<Note> Search(string searchTerm)
        {
            using (var db = new LiteDatabase(db_name))
            {
                var notes = db.GetCollection<Note>(nameof(Notes.Repository));
                return string.IsNullOrEmpty(searchTerm) ? new List<Note>() :  notes.Find(n => n.body.Contains(searchTerm));
            }
        }
     
    }
}
