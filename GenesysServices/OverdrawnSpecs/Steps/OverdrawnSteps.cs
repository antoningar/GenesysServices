using System.Text.Json;
using Api.Overdrawn.Services;
using Moq;
using Xunit;

namespace OverdrawnSpecs.Steps;

[Binding]
public class OverdrawnSteps
{
    private string _clientId = string.Empty;
    private float _clientBalance;
    private readonly OverdrawnApiFactory _api = new();
    private HttpResponseMessage? _response;
    
    [Given(@"a client (.*)")]
    public void GivenAClient(string clientId)
    {
        _clientId = clientId;
    }

    [Given(@"his account balance is (.*)")]
    public void GivenHisAccountBalanceIs(float balance)
    {
        _clientBalance = balance;
    }

    [Given(@"my system already know this client")]
    public void GivenMySystemAlreadyKnowThisClient()
    {
        Mock<IOverdrawnService> service = new();
        service
            .Setup(s => s.GetClientBalanceAsync(_clientId))
            .ReturnsAsync(_clientBalance);
        _api.OverdrawnService = service.Object;
    }

    [When(@"I check if this cliet is overdrawn")]
    public async Task WhenICheckIfThisClietIsOverdrawn()
    {
        HttpClient client = _api.CreateDefaultClient(new Uri("http://localhost/"));
        _response = await client.GetAsync($"/api/v1/overdrawn?clientId={_clientId}");
    }

    [Then(@"I got response (.*)")]
    public async Task ThenIGotResponse(bool isOverdrawn)
    {
        await using Stream contentStream = await _response!.Content.ReadAsStreamAsync();
        JsonElement result = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);

        Assert.Equal(isOverdrawn, result.GetProperty("isOverdrawn").GetBoolean());
    }
}