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
        public IEnumerable<Note> Get()
        {
            return ServiceProxy.GetNotes();
        }

        [HttpGet]
        [Route("notes/{id}")]
        public Note Get(int id)
        {
            var result = ServiceProxy.GetNote(id);

            if (result == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            return result;
        }

        [HttpGet]
        [Route("notes")]
        public IEnumerable<Note> Get(string query)
        {
            return ServiceProxy.SearchNotes(query);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("notes")]
        public HttpResponseMessage Post([FromBody]Note note)
        {
            try
            {
                ServiceProxy.SaveNote(note);
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

    }
}