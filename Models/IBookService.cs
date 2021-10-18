using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public interface IBookService
    {
        IEnumerable<Book> Get();
        Task<bool> Add(Book book);
        Task<bool> Edit(Book book);
        Task<bool> Delete(Guid id);
    }
}