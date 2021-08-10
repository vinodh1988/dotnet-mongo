using dotnet.Models;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System;

namespace dotnet.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
            _books = database.GetCollection<Book>(Environment.GetEnvironmentVariable("MONGO_COLLECTION_NAME"));
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);
    }
}