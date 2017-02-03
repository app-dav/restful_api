using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Repository;
 
namespace Notes.Repository.Test
{
    /// <summary>
    /// I am not a huge fan of mocking. I prefer isolated unit testing for logic and
    ///  integration testing for across boundaries
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntegrationTestDbInsert()
        {
            var proxy = new RepositoryProxy();
            proxy.Save(GetNoteForTest());

            
        }

        Note GetNoteForTest()
        {
            return new Note() { id = 1, body = "this is a test Note" };
        }
    }
}
