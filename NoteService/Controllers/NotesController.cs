using Apworks.Repositories;
using NoteService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NoteService.Controllers
{
    public class NotesController : ApiController
    {
        readonly IRepository<Note> noteRepository;

        public NotesController(IRepository<Note> noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        // GET api/notes
        public IEnumerable<Note> Get()
        {
            return noteRepository.FindAll();
        }

        // GET api/notes/6EE246A5-9E68-4BC9-BA24-7F4EC2B326D4
        public Note Get(Guid id)
        {
            return noteRepository.GetByKey(id);
        }

        // POST api/notes
        public void Post([FromBody]Note value)
        {
            noteRepository.Add(value);
            noteRepository.Context.Commit();
        }

        // PUT api/notes/6EE246A5-9E68-4BC9-BA24-7F4EC2B326D4
        public void Put(Guid id, [FromBody]Note value)
        {
            var note = noteRepository.GetByKey(id);
            note.Title = value.Title;
            note.Content = value.Content;
            note.CreatedDate = value.CreatedDate;
            note.Weather = value.Weather;
            noteRepository.Update(note);
            noteRepository.Context.Commit();
        }

        // DELETE api/notes/6EE246A5-9E68-4BC9-BA24-7F4EC2B326D4
        public void Delete(Guid id)
        {
            var note = noteRepository.GetByKey(id);
            noteRepository.Remove(note);
            noteRepository.Context.Commit();
        }
    }
}