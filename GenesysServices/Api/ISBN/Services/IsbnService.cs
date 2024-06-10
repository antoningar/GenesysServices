using Api.ISBNService;

namespace Api.ISBN.Services;

public class IsbnService : IIsbnService
{
    public async Task<bool> CheckIsbnAsync(string isbn)
    {
        SBNServiceSoapTypeClient client = new(SBNServiceSoapTypeClient.EndpointConfiguration.ISBNServiceSoap12);
        
        if (isbn.Length == 10)
        {
            IsValidISBN10Response? response10 = await client.IsValidISBN10Async(isbn);
            return response10.Body.IsValidISBN10Result;
        }
        IsValidISBN13Response? response13 = await client.IsValidISBN13Async(isbn);
        return response13.Body.IsValidISBN13Result;
    }
}