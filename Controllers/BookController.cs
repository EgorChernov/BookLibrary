using System;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            if (book is null || !ModelState.IsValid) return BadRequest($"Input value {book} was incorrect");

            var result = await _bookService.Add(book);

            if (result) return Ok();
            return BadRequest("Internal Server Error");
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Book book)
        {
            if (book is null || !ModelState.IsValid) return BadRequest($"Input value {book} was incorrect");

            var result = await _bookService.Edit(book);
            if (result) return Ok();
            return BadRequest("Internal Server Error");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id should not be empty");

            var result = await _bookService.Delete(id);

            if (result) return Ok();

            return BadRequest("Internal Server Error");
        }
    }
}