using Moq;
using WaterShortageApi.Models;
using WaterShortageApi.Services;
using Xunit;

namespace WeatherShortageSpecs.Steps;

[Binding]
public class ShortageStep
{
    private readonly ApiFactory _api = new();
    private HttpResponseMessage? _response; 
    
    [Given(@"SIREN api is down")]
    public void GivenSirenApiIsDown()
    {
        Mock<ISirenService> mockService = new();
        mockService
            .Setup(s => s.GetCodeRegionBySirenAsync(It.IsAny<string>()))
            .Throws(new Exception("External SIREN service down"));
        _api.SirenService = mockService.Object;
    }

    [When(@"I would like to know if my companies is vulnerable to water shortage in my region")]
    public async Task WhenIWouldLikeToKnowIfMyCompaniesIsVulnerableToWaterShortageInMyRegion()
    {
        HttpClient client = _api.CreateDefaultClient(new Uri("http://localhost/"));
        _response = await client.GetAsync("/api/watershortage?siren=972105410&api-version=1.0");
    }

    [Given(@"Water api is down")]
    public void GivenWaterApiIsDown()
    {
        Mock<IWaterService> mockService = new();
        mockService
            .Setup(s => s.GetRegionGravityByCodeAsync(It.IsAny<string>()))
            .Throws(new Exception("External water service down"));
        _api.WaterService = mockService.Object;
    }

    [Given(@"My company operate in (.*)")]
    public void GivenMyCompanyOperateIn(string postalCode)
    {
        Mock<ISirenService> mockService = new();
        mockService
            .Setup(s => s.GetCodeRegionBySirenAsync(It.IsAny<string>()))
            .Returns(Task.FromResult("972105410"));
        _api.SirenService = mockService.Object;
    }

    [Given(@"There is a a (.*) shortage")]
    public void GivenThereIsAaShortage(string gravity)
    {
        Mock<IWaterService> mockService = new();
        mockService
            .Setup(s => s.GetRegionGravityByCodeAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new ShortageResponse("Pyrénées-Atlantiques", true, gravity)));
        _api.WaterService = mockService.Object;
    }

    [Then(@"I got a response (.*)")]
    public void ThenIGotAResponse(int responseCode)
    {
        Assert.Equal(responseCode, (int)_response!.StatusCode);
    }
}