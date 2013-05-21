using System;
using System.Collections.Generic;
using System.Data.Entity;
using NoteServiceNancy.Model;

namespace NoteServiceNancy
{
    public class NoteServiceInitializer : DropCreateDatabaseIfModelChanges<NoteServiceDbContext>
    {
        protected override void Seed(NoteServiceDbContext context)
        {
            new List<Note>
            {
                new Note
                {
                    Title = "My first note", 
                    Content = "This is my first note.", 
                    CreatedDate = DateTime.Now, 
                    Weather = Weather.Sunny
                },
                new Note
                {
                    Title = "My second note", 
                    Content = "This is my second note.", 
                    CreatedDate = DateTime.Now, 
                    Weather = Weather.Windy
                }
            }.ForEach(p => context.Notes.Add(p));
        }
    }
}