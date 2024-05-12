using APBD_GroupA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APBD_GroupA.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class BookController : ControllerBase
{
    
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    [HttpGet("{id}/genres)")]

    public async Task<IActionResult> GetGenres(int id)
    {
        if ( !await _bookRepository.BookExists(id))
        {
            return NotFound("Not exists");
        }

        var book = _bookRepository.getGenres(id);

        return Ok(book);
    }

}