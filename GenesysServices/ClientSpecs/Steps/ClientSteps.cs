using System.Text.Json;
using Api.Client.Services;
using Moq;
using Xunit;

namespace ClientSpecs.Steps;

[Binding]
public class ClientSteps
{
    private string _clientPhoneNumber = string.Empty;
    private string _dataClientPhoneNumber = string.Empty;
    private readonly ClientApiFactory _api = new();
    private HttpResponseMessage? _response;
    
    [Given(@"a client with phone number (.*)")]
    public void GivenAClientWithPhoneNumber(string callerNumber)
    {
        _clientPhoneNumber = callerNumber;
    }
    
    [Given(@"a database client with an unique client with phone number (.*)")]
    public void GivenADatabaseClientWithAnUniqueClientWithPhoneNumber(string clientNumber)
    {
        _dataClientPhoneNumber = clientNumber;
    }
    
    [Given(@"client id (.*)")]
    public void GivenClientId(string clientId)
    {
        Mock<IDataClientService> service = new();
        service
            .Setup(s => s.GetClientIdByPhoneNumberAsync(_dataClientPhoneNumber))
            .ReturnsAsync(clientId);
        _api.DataClientService = service.Object;
    }
    
    [When(@"I call client service")]
    public async Task WhenICallClientService()
    {
        HttpClient client = _api.CreateDefaultClient(new Uri("http://localhost/"));
        _response = await client.GetAsync($"/api/v1/client?phoneNumber={_clientPhoneNumber}");
    }
    
    [Then(@"I receive this (.*) as a response")]
    public async Task ThenIReceiveThisAsAResponse(string clientIdResponse)
    {
        await using Stream contentStream = await _response!.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);

        Assert.Equal(clientIdResponse, result.GetProperty("clientId").ToString());
    }
}