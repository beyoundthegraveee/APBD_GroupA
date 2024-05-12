using APBD_GroupA.Models;

namespace APBD_GroupA.Repositories;

public interface IBookRepository
{
    
    public Task<bool> BookExists(int bookId);
    public Task<BookInfo> getGenres(int id);
}