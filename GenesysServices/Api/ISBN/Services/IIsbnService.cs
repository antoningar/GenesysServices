namespace Api.ISBN.Services;

public interface IIsbnService
{
    public Task<bool> CheckIsbnAsync(string isbn);
}