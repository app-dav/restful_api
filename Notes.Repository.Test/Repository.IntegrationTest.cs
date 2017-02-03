using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Interfaces;
using Notes.Repository;
using LiteDB;
using System.Linq;

namespace Notes.Repository.Test
{
    /// <summary>
    /// I am not a huge fan of mocking. I prefer isolated unit testing for logic and
    ///  integration testing for across boundaries
    ///  
    /// This isn't necessarily an exhaustive set of test, but I think you see where I'm going
    /// </summary>
    [TestClass]
    public class RepositoryIntegrationTests
    {
        string db_name = nameof(RepositoryIntegrationTests);
        string collection_name = "Notes";

        [TestMethod]
        public void IntegrationTestDbInsert()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    Assert.IsTrue(notes.Count() == 0, "Db is not empty");
                    note.id = null;

                    repo.Save(note, notes);
                }

                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    Assert.IsTrue(notes.Count() == 1, "Db note count 1");
                    Assert.IsTrue(notes.Exists(x => x.id == 1 && x.body == note.body), "Note is not in collection");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        [TestMethod]
        public void IntegrationTestDbUpdate()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    notes.Insert(note);
                    Assert.IsTrue(notes.Count() == 1, "Note is not inserted");

                    note.body = "this is a test Note2";
                    repo.Save(note, notes);
                }

                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    Assert.IsTrue(notes.Count() == 1, "Db note count 1");
                    Assert.IsTrue(notes.Exists(x => x.id == note.id && x.body == note.body), "Note is not in collection");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        [TestMethod]
        public void IntegrationTestGetAll()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    notes.Insert(note);

                    note.body = "this is a test Note2";
                    note.id = 2;
                    notes.Insert(note);
                    Assert.IsTrue(notes.Count() == 2, $"Notes collection setup incorrectly");
                }

                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    var all = repo.Get(null, notes);
                    Assert.IsTrue(all.Count() == notes.Count(), "Db note count note the same");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        [TestMethod]
        public void IntegrationTestGetAllWithEmptyCollection()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    var all = repo.Get(null, notes);
                    Assert.IsNotNull(all, "getAll returned null");
                    Assert.IsTrue(all.Count() == 0, "Db note not zero");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }


        [TestMethod]
        public void IntegrationTestDbGetNote()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    notes.Insert(note);
                    Assert.IsTrue(notes.Count() == 1, "Note is not inserted");
                }

                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    var retrievedNote = repo.Get(note.id.Value, notes).First();
                    Assert.IsTrue(retrievedNote.id == note.id && retrievedNote.body == note.body, "Note is not in collection");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        [TestMethod]
        public void IntegrationTestDbGetNoteWithEmptyCollection()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    var retrievedNote = repo.Get(note.id.Value, notes);
                    Assert.IsTrue(retrievedNote.Count() == 0, "should return empty");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }


        [TestMethod]
        public void GetNextIdTestWithEmptyCollection()
        {
            Cleanup();
            var repo = new Repository(null);

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    var result = repo.GetNextId(notes);

                    Assert.AreEqual(1, result);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        [TestMethod]
        public void GetNextIdTestWithNonEmptyCollection()
        {
            Cleanup();
            var repo = new Repository(null);
            var note = GetNoteForTest();

            try
            {
                using (var db = new LiteDatabase(db_name))
                {
                    var notes = db.GetCollection<Interfaces.Note>(collection_name);
                    notes.Insert(note);

                    var result = repo.GetNextId(notes);

                    Assert.AreEqual(note.id + 1, result);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                Cleanup();
            }
        }

        Note GetNoteForTest()
        {
            return new Note() { id = 1, body = "this is a test Note" };
        }


        [TestInitialize]
        [TestCleanup]
        void Cleanup()
        {
            using (var db = new LiteDatabase(db_name))
            {
                if (db.CollectionExists(collection_name))
                    db.DropCollection(collection_name);
            }
        }
    }
}
