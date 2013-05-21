using Apworks;
using System;

namespace NoteService.Models
{
    public enum Weather
    {
        Cloudy, Rainy, Sunny, Windy
    }

    public class Note : IAggregateRoot
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Weather Weather { get; set; }
    }
}