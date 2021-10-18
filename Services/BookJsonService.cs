using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Json;
using BookLibrary.Models;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Services
{
    public class BookJsonService : IBookService
    {
        private readonly IBookJsonContext _context;
        private readonly ILogger<BookJsonService> _logger;

        public BookJsonService(IBookJsonContext jsonContext, ILogger<BookJsonService> logger)
        {
            _context = jsonContext;
            _logger = logger;
        }

        public IEnumerable<Book> Get()
        {
            _logger.LogInformation("Checked in Get()");
            try
            {
                return _context.Books;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong at {ex.Message}  {ex.StackTrace}");
                return new List<Book>();
            }
        }

        public async Task<bool> Add(Book book)
        {
            _logger.LogInformation($"Checked in Add() with {book}");

            if (_context.Books.Any(_ => _.Id == book.Id))
            {
                _logger.LogWarning($"Book {book} already exist in collection");
                return false;
            }

            if (_context.Books.Any((book.Equals)))
            {
                _logger.LogWarning($"Same book {book} already exist in collection");
                return false;
            }

            if (book.Id == Guid.Empty)
            {
                book.Id = Guid.NewGuid();
            }

            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Book {book} added to collection");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong at {ex.Message}  {ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> Edit(Book book)
        {
            _logger.LogInformation($"Checked in Edit() with {book}");
            if (_context.Books.All(_ => _.Id != book.Id))
            {
                _logger.LogWarning($"Book with guid {book.Id} was not found in collection");
                return false;
            }
            
            try
            {
                _context.Books.RemoveAll(_ => _.Id == book.Id);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Book {book} added to collection");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong at {ex.Message}  {ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = _context.Books.SingleOrDefault(_ => _.Id == id);
            if (book == default)
            {
                _logger.LogInformation($"Book id='{id}' not found");
                return false;
            }

            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong at {ex.Message}  {ex.StackTrace}");
                return false;
            }
        }
    }
}