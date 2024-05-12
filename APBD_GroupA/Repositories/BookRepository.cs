using System.Data.SqlClient;
using APBD_GroupA.Models;

namespace APBD_GroupA.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IConfiguration _configuration;

    public BookRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<bool> BookExists(int bookId)
    {
        await using SqlConnection connection =
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        await using SqlCommand command =
            new SqlCommand();

        command.Connection = connection;
        
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@idBook", bookId);

        command.CommandText = "SELECT COUNT(*) FROM Books WHERE PK = @idBook";

        var cmd = (int) await command.ExecuteScalarAsync();

        return cmd > 0;

    }

    public async Task<BookInfo> getGenres(int id)
    {
        await using SqlConnection connection =
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        await using SqlCommand command =
            new SqlCommand();
        
        command.Connection = connection;

        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@idBook", id);
        
        command.CommandText = "SELECT g.name FROM genres g JOIN books_genres bg ON bg.FK_genre = g.PK WHERE bg.FK_book = @idBook";
        
        var reader =  await command.ExecuteReaderAsync();
        
        var listOfGenres = new List<string>();
        
        while (await reader.ReadAsync())
        {
            listOfGenres.Add(
                reader["name"].ToString()
            );
        }

        await reader.CloseAsync();
        
        command.CommandText =
            "SELECT PK, title FROM books WHERE PK = @idBook";

        reader = await command.ExecuteReaderAsync();

        BookInfo bookInfo = null;
                
        while (await reader.ReadAsync())
        {
            bookInfo = new BookInfo()
            {
                id = (int)reader["PK"],
                title = reader["title"].ToString(),
                genres = listOfGenres
            };

        }
        
        await reader.CloseAsync();
                
                
               
        return bookInfo;
        

    }
}