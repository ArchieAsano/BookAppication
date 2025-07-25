﻿using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;

        }
        [HttpGet("GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
            var booklist = await _bookService.GetAllBooks();
            return new OkObjectResult(booklist);
        }
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById([FromQuery] int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null) return new NotFoundObjectResult("Can not found book");
            return new OkObjectResult(book);
        }
    }
}
