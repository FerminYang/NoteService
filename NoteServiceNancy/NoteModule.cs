using System;
using Apworks.Repositories;
using Nancy;
using NoteServiceNancy.Model;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;

namespace NoteServiceNancy
{
    public class NoteModule : NancyModule
    {
        readonly IRepository<Note> _noteRepository;

        public NoteModule(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;

            Get["/"] = _ => "Hello world";
            Get["api/notes/"] = AllNotesResponse;
            Get["api/notes/{id}"] = GetNoteByIDResponse;
            Post["api/notes/"] = AddResponse;
            Put["api/notes/{id}"] = UpdateResponse;
            Delete["api/notes/{id}"] = DeleteResponse;
        }

        private Negotiator AllNotesResponse(dynamic _)
        {
            var notes = _noteRepository.FindAll();
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(notes);
        }

        private Negotiator GetNoteByIDResponse(dynamic parameters)
        {
            Guid id = parameters.id;
            var note = _noteRepository.GetByKey(id);
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(note);
        }

        private Negotiator AddResponse(dynamic _)
        {
            var note = this.Bind<Note>();
            _noteRepository.Add(note);
            _noteRepository.Context.Commit();
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(note);
        }

        private Negotiator UpdateResponse(dynamic parameters)
        {
            Guid id = parameters.id;
            var note = _noteRepository.GetByKey(id);
            var value = this.Bind<Note>();
            note.Title = value.Title;
            note.Content = value.Content;
            note.CreatedDate = value.CreatedDate;
            note.Weather = value.Weather;
            _noteRepository.Update(note);
            _noteRepository.Context.Commit();
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(note);
        }

        private Negotiator DeleteResponse(dynamic parameters)
        {
            Guid id = parameters.id;
            var note = _noteRepository.GetByKey(id);
            _noteRepository.Remove(note);
            _noteRepository.Context.Commit();
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(note);
        }

    }
}