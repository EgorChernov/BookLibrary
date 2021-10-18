using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BookLibrary.Models;

namespace BookLibrary.Json
{
    public class BookJsonContext : IBookJsonContext
    {
        private const string FILE_PATH = "booksJson.json";
        private List<Book> _books;

        public List<Book> Books
        {
            get => _books ??= ReadFromJson();
            set => _books = value;
        }

        private static List<Book> ReadFromJson()
        {
            List<Book> books = new();
            try
            {
                var json = File.ReadAllText(FILE_PATH);
                books = JsonSerializer.Deserialize<List<Book>>(json);
            }
            catch
            {
            }

            return books;
        }

        public async Task SaveChangesAsync()
        {
            await using var createStream = File.Create(FILE_PATH);
            await JsonSerializer.SerializeAsync(createStream, Books);
            await createStream.DisposeAsync();
        }
    }

    public interface IBookJsonContext
    {
        public List<Book> Books { get; set; }

        public Task SaveChangesAsync();
    }
}