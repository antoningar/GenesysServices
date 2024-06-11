using System.Text.Json;
using Xunit;

namespace ClientSpecs.Steps;

[Binding]
public class ClientSteps
{
    private string _clientPhoneNumber = string.Empty;
    private string _dataClientPhoneNumber = string.Empty;
    // private readonly ApiFactory _api = new();
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
        //Mock<IDataClientService> service = new();
        //service
        //.Setup(s => s.GetClientIdByPhoneNumberAsync(_dataClientPhoneNumber))
        //.ReturnAsync(clientId);
        //_api.ClientService = service.Object;
        ScenarioContext.StepIsPending();
    }
    
    [When(@"I call client service")]
    public void WhenICallClientService()
    {        
        // HttpClient client = _api.CreateDefaultClient(new Uri("http://localhost/"));
        // _response = await client.GetAsync($"/api/v1/isbn?isbn={_isbn}");
        ScenarioContext.StepIsPending();
    }
    
    [Then(@"I receive this (.*) as a response")]
    public async Task ThenIReceiveThisAsAResponse(string clientIdResponse)
    {
        await using Stream contentStream = await _response!.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);

        Assert.Equal(clientIdResponse, result.GetProperty("Id").ToString());
    }
}