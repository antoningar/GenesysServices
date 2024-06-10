using System.Text.Json;
using Api.ISBN.Services;
using Moq;
using Xunit;

namespace ISBNSpecs.Steps;

[Binding]
public class IsbnStep
{
    private string _isbn = string.Empty;
    private readonly ApiFactory _api = new();
    private HttpResponseMessage? _response; 
    
    [Given(@"an ISBN (.*)")]
    public void GivenAnIsbn(string isbn)
    {
        _isbn = isbn;
    }
    
    [Given(@"ISBN soap service return (.*)")]
    public void GivenIsbnSoapServiceReturn(bool valid)
    {
        Mock<IIsbnService> service = new();
        service
            .Setup(s => s.CheckIsbnAsync(It.IsAny<string>()))
            .ReturnsAsync(valid);
        
        _api.IsbnService = service.Object;
    }
    
    [When(@"I check if my ISBN is valid")]
    public async Task WhenICheckIfMyIsbnIsValid()
    {
        HttpClient client = _api.CreateDefaultClient(new Uri("http://localhost/"));
        _response = await client.GetAsync($"/api/v1/isbn?isbn={_isbn}");
    }
    
    [Then(@"I got response (.*)")]
    public async Task ThenIGotResponse(bool valid)
    {        
        await using Stream contentStream = await _response!.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);
        Assert.Equal(valid, result.GetProperty("isValid").GetBoolean());
    }
}