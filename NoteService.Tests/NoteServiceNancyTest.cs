﻿using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace NoteService.Tests
{
    [TestClass]
    public class NoteServiceNancyTest
    {
        static readonly HttpClient Client = new HttpClient();

        [ClassInitialize]
        public static void TestInitialize(TestContext context)
        {
            Client.BaseAddress = new Uri("http://localhost:42644");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestMethod]
        public void NancyGetTest()
        {
            var response = Client.GetAsync("api/notes.json").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            var notes = response.Content.ReadAsAsync<IEnumerable<Note>>().Result;
            Assert.IsTrue(notes.Any());
        }

        [TestMethod]
        public void NancyGetByIDTest()
        {
            var guid = new Guid("536c1308-2970-46a8-9aba-4e351f1a2efd");
            var response = Client.GetAsync(string.Format("api/notes/{0}.json", guid)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            var note = response.Content.ReadAsAsync<Note>().Result;
            Assert.AreEqual(guid, note.ID);
        }

        [TestMethod]
        public void NancyPostTest()
        {
            var iid = Guid.NewGuid();
            var note = new Note
            {
                Title = string.Format("My Nancy post {0}", iid),
                Content = string.Format("This is my Nancy post {0}.", iid),
                CreatedDate = DateTime.Now,
                Weather = Weather.Rainy
            };
            var response = Client.PostAsJsonAsync("api/notes/", note).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void NancyPutTest()
        {
            var guid = new Guid("536c1308-2970-46a8-9aba-4e351f1a2efd");
            var response = Client.GetAsync(string.Format("api/notes/{0}", guid)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            var note = response.Content.ReadAsAsync<Note>().Result;
            note.Content = note.Title + " UPDATED ON " + DateTime.Now.ToString(CultureInfo.InvariantCulture);
            response = Client.PutAsJsonAsync(string.Format("api/notes/{0}", guid), note).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void NancyDeleteTest()
        {
            var guid = new Guid("82c6047a-c505-4b0a-b4b2-6938d5f7c3f9");
            var response = Client.DeleteAsync(string.Format("api/notes/{0}", guid)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
