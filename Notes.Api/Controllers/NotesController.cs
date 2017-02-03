using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using System.Net.Http;
using System.Web.Http;

namespace Notes.Api.Controllers
{
    public class NotesController : ApiController
    {
        [HttpGet]
        [Route("notes")]
        public IEnumerable<INote> Get()
        {
            return ServiceProxy.GetNotes();
        }

        [HttpGet]
        [Route("notes/{id}")]
        public INote Get(int id)
        {
            var result = ServiceProxy.GetNote(id);

            if (result == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return result;
        }

        [HttpGet]
        [Route("notes/searchTerm")]
        public IEnumerable<INote> Get(string searchTerm)
        {
            return ServiceProxy.SearchNotes(searchTerm);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

    }
}